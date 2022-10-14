import { join, normalize } from 'path';
import { GetGroupsByNameApiResponse, GetGroupsByNameApiService } from '../apis/groups/get-groups-by-name-api.service';
import { GetPlansByIdApiRequest, GetPlansByIdApiResponse, GetPlansByIdApiService } from "../apis/plans/get-plans-by-id-api.service";
import { GetTreeViewByGroupIdService, IGetTreeViewByGroupIdResponse } from "../apis/templates/get-tree-view-by-group-id.api.service";
import { CompilerService } from "./compiler.servie";
import { FsService } from "./fs.service";

export class RunPlanService {
    private compilerService: CompilerService = new CompilerService();
    private getPlansByIdApiService: GetPlansByIdApiService = new GetPlansByIdApiService();
    private getTreeViewByGroupIdService: GetTreeViewByGroupIdService = new GetTreeViewByGroupIdService();
    private getGroupsByNameApiService: GetGroupsByNameApiService = new GetGroupsByNameApiService();
    private fsService: FsService = new FsService();
    private _frontPath?: string;
    private _backPath?: string;
    private planData: GetPlansByIdApiResponse | undefined;
    private treeViewsOfGroup: { groupId: string, treeView: IGetTreeViewByGroupIdResponse[] }[] = [];
    private childsOfGroups: {
        planApiId?: string, 
        groupId: string, 
        generationTypeValue: string, 
        files: any}[] = [];
    private tempFile: any = {};

    constructor(init: {frontPath?: string, backPath?: string}) {
        this._frontPath = init.frontPath;
        this._backPath = init.backPath;
    };

    async handle (data: {params: GetPlansByIdApiRequest}) : Promise<any> {
        await this.getPlansByIdApiService.RequestAsync(data.params).then((response: GetPlansByIdApiResponse) => {
            this.planData = response;
        });

        // throw error if generationtypes are front and back, but paths are missing -> to do

        for (const planApi of this.planData?.planApis ?? []) {
            const groupsToGenerate: GetGroupsByNameApiResponse[] = [];
            await this.getGroupsByNameApiService.RequestAsync({
                groupName: planApi.groupName, 
                templateId: this.planData?.template.id ?? undefined
            }).then((response: GetGroupsByNameApiResponse[]) => {
                for (const item of response) {
                    for (const generationType of this.planData?.generationTypes ?? []) {
                        if (generationType.value == item.generationType.value) {
                            groupsToGenerate.push(item);
                        }
                    } 
                }
            })
            
            for (const groupToGenerate of groupsToGenerate) {
                await this.getTreeViewByGroupIdService.RequestAsync({groupId: groupToGenerate.id}).then((response: IGetTreeViewByGroupIdResponse[]) => {
                    this.treeViewsOfGroup.push({
                        groupId: groupToGenerate.id,
                        treeView: response
                    });
                    this.exportChildsOfGroup({
                        groupId: groupToGenerate.id,
                        childrens: response[0].children,
                        generationTypeValue: groupToGenerate.generationType.value,
                        planApiId: planApi.id});
                });
            }
        }

        for (const planGroupName of this.planData?.planGroupNames ?? []) {
            const planGroupsToGenerate: GetGroupsByNameApiResponse[] = [];
            await this.getGroupsByNameApiService.RequestAsync({
                groupName: planGroupName, 
                templateId: this.planData?.template.id ?? undefined
            }).then((response: GetGroupsByNameApiResponse[]) => {
                for (const item of response) {
                    for (const generationType of this.planData?.generationTypes ?? []) {
                        if (generationType.value == item.generationType.value) {
                            planGroupsToGenerate.push(item);
                        }
                    } 
                }
            })

            for (const planGroupToGenerate of planGroupsToGenerate) {
                await this.getTreeViewByGroupIdService.RequestAsync({groupId: planGroupToGenerate.id}).then((response: IGetTreeViewByGroupIdResponse[]) => {
                    this.treeViewsOfGroup.push({
                        groupId: planGroupToGenerate.id,
                        treeView: response
                    });
                    this.exportChildsOfGroup({
                        groupId:  planGroupToGenerate.id,
                        childrens: response[0].children,
                        generationTypeValue: planGroupToGenerate.generationType.value
                    });
                });
            }
        }

        for (const childsOfGroup of this.childsOfGroups) {
            for (const file of childsOfGroup.files) {

                let input: any = this.planData;
                if (childsOfGroup.planApiId) {
                    input.planApi = input.planApis.filter((x: any) => x.id == childsOfGroup.planApiId)[0];
                }
                
                file.name = this.compilerService.compile(input, file.name);
                file.path = this.compilerService.compile(input, file.path);
                file.contentAdd = this.compilerService.compile(input, file.contentAdd ?? "");

                for (const fileEdit of file.fileEdits) {
                    fileEdit.content = this.compilerService.compile(input, fileEdit.content ?? "");
                }

                let projectPath = "";
                if (childsOfGroup.generationTypeValue == 'FRONT_END') {
                    projectPath = this._frontPath ?? "";
                }
                else if (childsOfGroup.generationTypeValue == 'BACK_END') {
                    projectPath = this._backPath ?? "";
                }
                if (file.action == 'ADD_EDIT') {
                    await this.fsService.addOrEditFile({
                        path: normalize(join(projectPath, file.path)),
                        fileName: file.name,
                        contentAdd: file.contentAdd ?? "",
                        fileEdits: file.fileEdits,
                    })
                }
                else if (file.action == 'EDIT') {
                    await this.fsService.editFile({
                        path: normalize(join(projectPath, file.path)),
                        fileName: file.name,
                        fileEdits: file.fileEdits,
                    })
                }
            }
        }
    }

    private exportChildsOfGroup(input: { 
        groupId: string, 
        childrens: IGetTreeViewByGroupIdResponse[], 
        planApiId?: string,
        generationTypeValue: string
    }) {
        for (const itemChild of input.childrens) {
            if (itemChild.type == 'file') {
                let file: any = {};

                file.id = itemChild.id;
                file.name = itemChild.name;
                file.type = itemChild.type;
                file.contentAdd = itemChild.contentAdd;
                file.fileEdits = itemChild.fileEdits;
                file.action = itemChild.action;
                file.groupId = itemChild.groupId;
                file.templateId = itemChild.templateId;
                file.path = this.findPath(itemChild.id, this.treeViewsOfGroup.find(x => x.groupId == input.groupId)?.treeView ?? []);

                const isGroupId = this.childsOfGroups?.find((x) => x.groupId == input.groupId && x.planApiId == input.planApiId);
                if (typeof isGroupId === 'undefined') {
                    this.childsOfGroups.push({
                        planApiId: input.planApiId,
                        generationTypeValue: input.generationTypeValue,
                        groupId: input.groupId,
                        files: [file]
                    });
                }
                else {
                    isGroupId.files.push(file);
                }
            }
            if (itemChild.children && itemChild.children.length > 0) {
                this.exportChildsOfGroup({
                    groupId: input.groupId,
                    childrens: itemChild.children,
                    planApiId: input.planApiId,
                    generationTypeValue: input.generationTypeValue});
            }
        }
    }

    private findPath(fileId: string, treeView: IGetTreeViewByGroupIdResponse[]) : any {
        let path = "";
        this.findItemById(treeView, fileId);
        let nextParent = this.tempFile.groupId;
        let isParent = false;
        while (!isParent) {
            this.findItemById(treeView, nextParent);
            if (this.tempFile && (typeof this.tempFile.groupId === 'undefined' || this.tempFile.groupId == null)) {
                isParent = true;
            }
            else {
                path = `/${this.tempFile.name}${path}`
                nextParent = this.tempFile.groupId;
            }
        }
        return path;
    } 

    private findItemById(items: any, id: string) : any {
        for (let itemChild of items) {
            if (itemChild.id == id) {
                this.tempFile = itemChild;
            }
            else if (itemChild.children && itemChild.children.length > 0) {
                this.findItemById(itemChild.children, id);
            }
        }
    }
}