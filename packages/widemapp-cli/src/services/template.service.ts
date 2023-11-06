import { promises as fs } from 'node:fs';
import path, { join, normalize } from 'node:path';
import { AddFileService } from '../apis/templates/add-file.service';
import { AddGroupService } from '../apis/templates/add-group.service';
import { input, select } from '@inquirer/prompts';
import { AddTemplateService } from '../apis/templates/add-template.service';
import {
    GetTreeViewByGroupIdService,
    IGetTreeViewByGroupIdResponse
} from '../apis/templates/get-tree-view-by-group-id.api.service';
import { GetGroupsByNameApiResponse, GetGroupsByNameApiService } from '../apis/groups/get-groups-by-name-api.service';
import { ISaveFileRequest, SaveFileService } from '../apis/templates/save-file.service';

export class TemplateService {
    private addFileService: AddFileService = new AddFileService();
    private addGroupService: AddGroupService = new AddGroupService();
    private getTreeViewByGroupIdService: GetTreeViewByGroupIdService = new GetTreeViewByGroupIdService();
    private getGroupsByNameApiService: GetGroupsByNameApiService = new GetGroupsByNameApiService();
    private addTemplateService: AddTemplateService = new AddTemplateService();
    private saveFileService: SaveFileService = new SaveFileService();
    private STRUCTURE_FILE_NAME: string = 'structure.json';

    async handle() : Promise<void> {
        const outputPath = normalize(join(process.cwd(), this.STRUCTURE_FILE_NAME));

        let content: any = {};
        try {
            const contentStr = await fs.readFile(outputPath, 'utf-8');
            content = JSON.parse(contentStr);

            if (!content.templateId) {
                const response = await select({
                    message: 'Do you want to create a new template or use the existing one?',
                    choices: [{
                        name: 'Create New',
                        value: 'Create New',
                    },
                    {
                        name: 'Use Existing',
                        value: 'Use Existing',
                    }]
                });

                if (response === 'Create New') {
                    const templateName = await input({ message: 'Enter template name' });
                    const projectId = await input({ message: 'Enter project id' });
                    const addTemplateServiceRequest = {
                        name: templateName,
                        projectId: projectId
                    };
                    content.templateId = await this.addTemplateService.sendAsync(addTemplateServiceRequest);
                } else {
                    const templateId = await input({ message: 'Enter template id' });
                    content.templateId = templateId;

                    await fs.writeFile(outputPath, JSON.stringify(content, null, 2));
                }
            }
        } catch (error: any) {
            if (error.code === 'ENOENT') {
                const response = await select({
                    message: 'Do you want to create a new template or use the existing one?',
                    choices: [{
                        name: 'Create New',
                        value: 'Create New',
                    },
                    {
                        name: 'Use Existing',
                        value: 'Use Existing',
                    }]
                });

                if (response === 'Create New') {
                    const templateName = await input({ message: 'Enter template name' });
                    const projectId = await input({ message: 'Enter project id' });
                    const addTemplateServiceRequest = {
                        name: templateName,
                        projectId: projectId
                    };
                    content.templateId = await this.addTemplateService.sendAsync(addTemplateServiceRequest);
                } else {
                    const templateId = await input({ message: 'Enter template id' });
                    content.templateId = templateId;
                }

                await fs.writeFile(outputPath, JSON.stringify(content, null, 2));
            } else {
                console.error('An error occurred:', error.message);
            }
        }

        content = await this.checkFrontAndBackPaths(outputPath, content);

        for (const pathItem of content.paths) {
            const groups = await this.readRootFoldersAsync(pathItem.path);
            for (const group of groups) {
                const filepaths = await this.readFilesRecursivelyAsync(group, pathItem.path);
                const jsonStructure = this.createJSONStructure(filepaths, pathItem.type);
                const cloudTree = await this.checkDifferences(content, group, pathItem.type);
                const result: any = this.findDifference(cloudTree, jsonStructure, pathItem.type);

                // console.log('result:', JSON.stringify(result, null, 2));
                await this.synchronize(result, [result], pathItem.value, content.templateId, pathItem.path, null, 'group');
            }
        }
    }

    async checkFrontAndBackPaths(outputPath: string, content: any): Promise<any> {
        if (!content?.paths) {
            const frontEndPath = await input({ message: 'Enter path for frontend' });
            const backEndPath = await input({ message: 'Enter path for backend' });
            const paths: any = [
                {
                    type: 'FRONT_END',
                    value: '5e532578-f07c-4ba3-9ac6-c4ef664b2085',
                    path: frontEndPath
                }, {
                    type: 'BACK_END',
                    value: '3898f5b4-8850-495c-a527-f9e87a354dab',
                    path: backEndPath
                }
            ];
            content.paths = paths;
            await fs.writeFile(outputPath, JSON.stringify(content, null, 2));
            return content;
        }

        return content;
    }

    async readFilesRecursivelyAsync(dir: string, cwd: string = '.'): Promise<any[]> {
        let results: any[] = [];

        const absoluteDir = path.join(cwd, dir);
        const items = await fs.readdir(absoluteDir, { withFileTypes: true });

        for (const dirent of items) {
            const filePath = path.join(dir, dirent.name);

            if (dirent.isDirectory()) {
                results = [...results, ...(await this.readFilesRecursivelyAsync(filePath, cwd))];
            } else {
                results.push(filePath);
            }
        }

        return results;
    }

    async readRootFoldersAsync(dir: string): Promise<string[]> {
        const folders: string[] = [];

        const list = await fs.readdir(dir);
        for (const item of list) {
            const itemPath = path.join(dir, item);
            const stat = await fs.stat(itemPath);

            if (stat && stat.isDirectory()) {
                folders.push(item);
            }
        }

        return folders;
    }

    createJSONStructure(paths: string[], groupType: string): any {
        const root: any = {
            name: '',
            type: 'content',
            children: []
        };

        for (const p of paths) {
            const parts = p.split(path.sep);
            let currentLevel = root.children;

            for (const [index, part] of parts.entries()) {
                const existingPath = currentLevel.find((child: any) => child.name === part);

                if (!existingPath) {
                    if (index === parts.length - 1) {
                        currentLevel.push({
                            name: part,
                            type: 'file'
                        });
                    } else {
                        const newPart = {
                            name: part,
                            groupType: groupType,
                            type: 'folder',
                            children: []
                        };
                        currentLevel.push(newPart);
                        currentLevel = newPart.children;
                    }
                } else {
                    currentLevel = existingPath.children;
                }
            }
        }

        return root.children[0];
    }

    async checkDifferences(content: any, groupName: string, type: string) : Promise<any> {
        return await this.getGroupsByNameApiService.requestAsync({
            groupName: groupName,
            templateId: content.templateId
        }).then(async (response: GetGroupsByNameApiResponse[]) => {
            for (const item of response) {
                if (type === item.generationType.value) {
                    return await this.getTreeViewByGroupIdService.requestAsync({ groupId: item.id }).then((response: IGetTreeViewByGroupIdResponse[]) => {
                        return response[0];
                    });
                }
            }
        });
    }

    // eslint-disable-next-line max-params
    async synchronize(root: any[], items: any[], generationTypeId: string | null, templateId: string, typePath: string, parentGroupId: string | null = null, type: string = 'folder'): Promise<void> {
        for (const item of items) {
            if (!item) continue;
            let groupId = item?.groupId || null;  // Start with item's groupId if it exists

            if (item?.type === 'folder' || item?.type === 'group') {
                if (!groupId) {
                    const newNode = {
                        name: item?.name,
                        type: type,
                        groupId: parentGroupId,
                        generationTypeId: generationTypeId,
                        templateId: templateId,
                        children: item?.children || []
                    };

                    if (item?.id) {
                        groupId = item?.id || null;

                        item.groupId = parentGroupId !== null ? groupId : null;
                        item.templateId = newNode.templateId;
                        item.generationTypeId = newNode.generationTypeId;
                        item.id = groupId;
                    } else {
                        const groupIdResponse = await this.addGroupService.sendAsync(newNode).catch(error => console.log(error.response.data));
                        groupId = groupIdResponse?.id || null;
                        item.groupId = parentGroupId !== null ? groupId : null;
                        item.templateId = newNode.templateId;
                        item.generationTypeId = newNode.generationTypeId;
                        item.id = groupId;
                    }
                } else {
                    const itemPath = this.findPathForNode(root, item?.id);
                    if (itemPath) {
                        const localPath = path.join(typePath, ...itemPath);
                        try {
                            await fs.access(localPath); // Check if path exists
                        } catch {
                            if (item.type === 'folder' || item.type === 'group') {
                                await fs.mkdir(localPath, { recursive: true });
                            } else {
                                await fs.writeFile(localPath, '');
                            }
                        }
                    }
                }

                if (item.children) {
                    await this.synchronize(root, item.children, null, templateId, typePath, item?.id ? item.id : groupId);
                }
            } else {
                // eslint-disable-next-line no-lonely-if
                if (!groupId) {
                    const newNode = {
                        name: item?.name,
                        type: 'file',
                        groupId: parentGroupId,
                        templateId: templateId,
                        action: 'ADD_EDIT'
                    };

                    if (item?.id) {
                        item.id = item?.id || null;
                        item.groupId = newNode.groupId;
                        item.templateId = newNode.templateId;
                        item.action = newNode.action;
                    } else {
                        const fileIdResponse = await this.addFileService.sendAsync(newNode).catch(error => console.log(error.response.data));
                        item.id = fileIdResponse || null;
                        item.groupId = newNode.groupId;
                        item.templateId = newNode.templateId;
                        item.action = newNode.action;

                        const itemPath = this.findPathForNode(root, item?.id);
                        if (itemPath) {
                            const localPath = path.join(typePath, ...itemPath);
                            const fileContent = await fs.readFile(localPath, 'utf-8');
                            const saveFileRequest: ISaveFileRequest = {
                                id: item.id,
                                contentAdd: fileContent
                            }
                            await this.saveFileService.sendAsync(saveFileRequest).catch(error => console.log(error));
                        }
                    }
                } else {
                    const itemPath = this.findPathForNode(root, item?.id);
                    if (itemPath) {
                        const localPath = path.join(typePath, ...itemPath);
                        try {
                            await fs.access(localPath);
                        } catch {
                            if (item.type === 'folder' || item.type === 'group') {
                                await fs.mkdir(localPath, { recursive: true });
                            } else {
                                await fs.writeFile(localPath, '');
                            }
                        }
                    }
                }
            }
        }
    }

    findPathForNode(root: any, targetId: string, currentPath: string[] = []): string[] | null {
        if (root.id === targetId) {
            return [...currentPath, root.name];
        }

        for (const child of root.children || []) {
            const pathForChild = this.findPathForNode(child, targetId, [...currentPath, root.name]);
            if (pathForChild) {
                return pathForChild;
            }
        }

        return null;
    }

    public findDifference(tree1: TreeNode, tree2: TreeNode, type: string): TreeNode {

        if (!tree1 && !tree2) {
            return { name: '', type: 'content', children: [] };
        }
    
        if (!tree1) {
            return { ...tree2! }; 
        }
    
        if (!tree2) {
            return { ...tree1 };
        }
    

        const resultTree: TreeNode = { ...tree1, children: [] };
    
        function nodeExistsInTree(node: TreeNode, tree: TreeNode): boolean {
            if (!tree.children) return false;
            return !!tree.children.find((child: any) => child.name === node.name && (child.groupType === type || child.type === node.type));
        }
    
        function recursiveDiff(node1: TreeNode, node2: TreeNode, resultNode: TreeNode): void {    
            for (const child1 of node1.children || []) {
                const matchingChild2 = (node2.children || []).find((child: any) => child.name === child1.name && (child.groupType === type || child.type === child1.type));
                if (!matchingChild2) {
                    resultNode.children = resultNode.children || [];
                    resultNode.children.push({ ...child1 });
                } else {
                    const newChild: TreeNode = { ...child1, children: [] };
                    recursiveDiff(child1, matchingChild2, newChild);
                    if (newChild.children && newChild.children.length > 0) {
                        resultNode.children = resultNode.children || [];
                        resultNode.children.push(newChild);
                    }
                }
            }
    
            for (const child2 of node2.children || []) {
                if (!nodeExistsInTree(child2, node1)) {
                    resultNode.children = resultNode.children || [];
                    resultNode.children.push({ ...child2 });
                }
            }
        }
    
        recursiveDiff(tree1, tree2, resultTree);
    
        return resultTree;
    }
    
}

interface TreeNode {
    [key: string]: any;
}