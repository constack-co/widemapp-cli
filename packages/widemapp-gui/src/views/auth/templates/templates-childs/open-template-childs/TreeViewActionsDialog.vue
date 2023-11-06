<template>
    <v-row justify="center">
        <v-dialog
            v-model="dialogChild"
            persistent
            :max-width="propThreeViewData.action == 'ADD_NEW_FILE' ? '400px' : '300px'"
        >
            <v-card>
                <v-toolbar>
                <v-toolbar-title> {{ propThreeViewData ? propThreeViewData.label : ''}} </v-toolbar-title>
                    <v-spacer></v-spacer>
                    <v-btn icon @click="closeDialogChild()">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-toolbar>
                <v-card-actions class="mt-3">
                    <v-row dense>
                        <v-col :cols="propThreeViewData.action == 'ADD_NEW_FILE' ? '4' : '8'">
                        <v-text-field
                            v-model="inputValue"
                            label="Value"
                            dense
                            >
                        </v-text-field>
                        </v-col>
                        <v-col cols="4" v-if="propThreeViewData.action == 'ADD_NEW_FILE'" >
                        <v-select
                            dense
                            :items="['ADD_EDIT', 'EDIT']"
                            v-model="actionValue"
                            label="Select Action"
                        ></v-select>
                        </v-col>
                        <v-col cols="4">
                            <v-btn
                                color="green darken-1"
                                text
                                @click="emmitThreeViewDataName()"
                            >
                            Save
                        </v-btn>
                        </v-col>
                    </v-row>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script lang="ts">
import {Vue, Component, Prop, Watch} from "@/importBase";
import ActionsEnum from "@Common/enums/TreeEnum";
import { AddFileService, IAddFileRequest } from "@Services/templates/add-file.service";
import { AddGroupService, IAddGroupRequest } from "@Services/templates/add-group.service";
import { IUpdateFileRequest, UpdateFileService } from "@Services/templates/update-file.service";
import { IUpdateGroupRequest, UpdateGroupService } from "@Services/templates/update-group.service";

@Component
export default class TreeViewActionsDialog extends Vue{

    public addFileService: AddFileService = new AddFileService();
    public addGroupService: AddGroupService = new AddGroupService();
    public updateFileService: UpdateFileService = new UpdateFileService();
    public updateGroupService: UpdateGroupService = new UpdateGroupService();

    public async addFile(body: IAddFileRequest):Promise<string>{
        return await this.addFileService.SendAsync(body);
    }

    public async updateFile(body: IUpdateFileRequest):Promise<void>{
        return await this.updateFileService.SendAsync(body);
    }

    public async addGroup(body: IAddGroupRequest):Promise<string>{
        return await this.addGroupService.SendAsync(body);
    }

    public async updateGroup(body: IUpdateGroupRequest):Promise<void>{
        return await this.updateGroupService.SendAsync(body);
    }

    @Prop()
    dialog: boolean = false;

    @Prop({
        required: false,
        type: Object,
    })
    threeViewData;

    inputValue = '';
    actionValue = '';

    @Watch('threeViewData', {
        deep: true,
        immediate: false
    })
    onThreeViewDataChanged(value: any, oldValue: any) {
        if (value.action == ActionsEnum.RENAME) {
            this.inputValue = value.name
        }
        else {
            this.inputValue = '';
        }
    }

    get propThreeViewData() {
        return this.threeViewData;
    }

    async emmitThreeViewDataName() {
        if (this.propThreeViewData.action == ActionsEnum.ADD_NEW_FILE) {
            await this.addFile({
                name: this.inputValue,
                groupId: this.propThreeViewData.type == 'file' ? this.propThreeViewData.groupId : this.propThreeViewData.id,
                templateId:this.propThreeViewData.templateId,
                action: this.actionValue
            }).then((data) => {
                this.$emit('updateThreeViewData', { 
                    parentId: this.propThreeViewData.id,
                    id: data, 
                    name: this.inputValue,
                    type: 'file',
                    groupId: this.propThreeViewData.id,
                    templateId:this.propThreeViewData.templateId,
                    action: this.propThreeViewData.action,
                    isAddedFromFile: this.propThreeViewData.type == 'file' ? true : false,
                    children: []
                });
                this.$emit('updateDialog', false);
            })
        }
        else if (this.propThreeViewData.action == ActionsEnum.ADD_NEW_FOLDER) {
            await this.addGroup({
                name: this.inputValue,
                type: 'folder',
                groupId: this.propThreeViewData.type == 'file' ? this.propThreeViewData.groupId : this.propThreeViewData.id,
                templateId:this.propThreeViewData.templateId
            }).then((response: any) => {
                this.$emit('updateThreeViewData', { 
                    parentId: this.propThreeViewData.id,
                    id: response.id, 
                    name: this.inputValue,
                    type: 'folder',
                    groupId: this.propThreeViewData.id,
                    templateId:this.propThreeViewData.templateId,
                    action: this.propThreeViewData.action,
                    isAddedFromFile: this.propThreeViewData.type == 'file' ? true : false,
                    children: []
                });
                this.$emit('updateDialog', false);
            })
        }
        else if (this.propThreeViewData.action == ActionsEnum.RENAME && this.propThreeViewData.type == 'file') {
            await this.updateFile({
                id: this.propThreeViewData.id,
                name: this.inputValue
            }).then(() => {
                this.$emit('updateThreeViewData', { 
                    parentId: this.propThreeViewData.id,
                    name: this.inputValue,
                    action: this.propThreeViewData.action
                });
                this.$emit('updateDialog', false);
            })
        }
        else if (this.propThreeViewData.action == ActionsEnum.RENAME && 
        (this.propThreeViewData.type == 'folder' || this.propThreeViewData.type == 'group')
        ) {
            await this.updateGroup({
                id: this.propThreeViewData.id,
                name: this.inputValue
            }).then(() => {
                this.$emit('updateThreeViewData', { 
                    parentId: this.propThreeViewData.id,
                    name: this.inputValue,
                    action: this.propThreeViewData.action
                });
                this.$emit('updateDialog', false);
            })
        }
    }

    get dialogChild() {
        return this.dialog;
    }

    set dialogChild(value) {
        this.$emit('updateDialog', value);
    }
    
    closeDialogChild() {
        this.$emit('updateDialog', false);
    }
}

</script>