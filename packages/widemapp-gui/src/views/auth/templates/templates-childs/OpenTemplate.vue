<template>
  <div>
    <ResizableBox :option="option">
      <template #left>
        <v-card
            class="pa-2 mr-4"
            outlined
            tile
          >
          <v-row class="mt-2">
            <v-col>
              <v-text-field
                v-model="newGroupInput.value"
                dense
                label="Group name"
                placeholder="New group"
                >
              </v-text-field>
            </v-col>
            <v-col>
              <v-select
                  dense
                  :items="generationTypes"
                  v-model="generationType"
                  item-text="name"
                  label="Select generation type"
                  return-object
              ></v-select>
            </v-col>
            <v-col>
              <v-btn
                depressed
                color="primary"
                @click="createNewGroup()"
              >
                Add
              </v-btn>
            </v-col>
          </v-row>
          <v-divider></v-divider>
              <template>
                  <v-treeview
                      v-model="tree"
                      :open="initiallyOpen"
                      :items="items"
                      activatable
                      item-key="id"
                      @update:active="changeSelectedFile($event)"
                  >
                      <template v-slot:prepend="{ item, open }">
                        <v-icon v-if="item.type == 'group'">
                            {{ open ? 'mdi-sort-variant-lock-open' : 'mdi-format-list-group' }}
                        </v-icon>
                        <v-icon v-else-if="item.type == 'folder'">
                            {{ open ? 'mdi-folder-open' : 'mdi-folder' }}
                        </v-icon>
                        <v-icon v-else>
                            {{ filesIcons[item.name.split('.').pop()] }}
                        </v-icon>
                      </template>
  
                      <template v-slot:append="{ item }">
                          <v-icon v-if="selectedItem.id == item.id" class="mx-2"
                            @click="rename(item)">mdi-rename-box</v-icon>
  
                          <v-icon v-if="selectedItem.id == item.id" class="mx-2"
                            @click="addFile(item)">mdi-file-plus</v-icon>
  
                          <v-icon v-if="selectedItem.id == item.id" class="mx-2"
                            @click="addFolder(item)">mdi-folder-plus</v-icon>

                          <v-icon v-if="selectedItem.id == item.id" class="mx-2"
                            @click="deleteFileOrFolder(item)">mdi-trash-can</v-icon>

                          <span>{{ getGenerationTypeName(item.generationTypeId) }}</span>
                      </template>
                  </v-treeview>
              </template>
          </v-card>
      </template>
      <template #center>
        <v-card
            class="mx-4"
            outlined
            tile
            :disabled="selectedItem.type != 'file'"
          >
          <v-toolbar
            flat
            color="primary"
            dark
          >
            <v-toolbar-title>Code Editor
            
            <v-btn 
            color="success" 
            @click="saveFile()">
              <v-icon>mdi-check</v-icon>
              Save</v-btn>
            </v-toolbar-title>
          </v-toolbar>
          <v-tabs>
            <v-tab v-show="selectedItem.action == 'ADD_EDIT'">
              <v-icon left>
                mdi-note-plus-outline
              </v-icon>
              When Add
            </v-tab>
            <v-tab>
              <v-icon left>
                mdi-note-edit
              </v-icon>
              When Edit
            </v-tab>
            <v-spacer></v-spacer>
            <v-tab>
              <v-icon left>
                mdi-help-rhombus
              </v-icon>
              Examples & Helpers
            </v-tab>
            <v-tab-item>
              <v-card flat>
                <CodeEditor v-model="selectedItem.contentAdd" 
                  :language_selector="false"
                  spellcheck="false"
                  :display_language="false"
                  :wrap_code="true"
                  width="auto"
                ></CodeEditor>
              </v-card>
            </v-tab-item>
            <v-tab-item>
              <v-card flat>
                <v-btn class="ma-2" @click="newEditContent(selectedItem.id)" color="info">Add new edit content handler</v-btn>
                <v-divider></v-divider>
                <div class="ma-2" v-for="(fileEdit, index) in selectedItem.fileEdits" :key="index">
                  <v-divider></v-divider>
                  <v-text-field
                    class="mb-2"
                    hide-details
                    v-model="fileEdit.placeholder"
                    name="placeholder"
                    label="Placeholder"
                    id="placeholder"
                  ></v-text-field>
                  <CodeEditor 
                  class="mb-5 mt-0"
                  v-model="fileEdit.content" 
                    :language_selector="false"
                    spellcheck="false"
                    :display_language="false"
                    :wrap_code="true"
                    width="auto"
                  ></CodeEditor>
                </div>
                
              </v-card>
            </v-tab-item>
            <v-tab-item>
              <v-card flat>
                <ExamplesHelpers />
              </v-card>
            </v-tab-item>
          </v-tabs>
          </v-card>
      </template>
      <template #right>
        <div class="mx-4">
          <h2>PAYLOAD</h2>
          <v-divider class="mt-2"></v-divider>
          <vue-json-pretty
            
            :data="state.data"
            :deep="state.deep"
            :deep-collapse-children="state.deepCollapseChildren"
            :show-double-quotes="state.showDoubleQuotes"
            :show-length="state.showLength"
            :show-line="state.showLine"
            :show-line-number="state.showLineNumber"
            :collapsed-on-click-brackets="state.collapsedOnClickBrackets"
            :show-icon="state.showIcon"
            style="position: relative"
            >
          </vue-json-pretty>
        </div>
      </template>
    </ResizableBox>
    <TreeViewActionsDialog 
      :dialog="threeViewDialog" 
      :threeViewData="threeViewData" 
      @updateDialog="updateDialog($event)"
      @updateThreeViewData="emmitThreeViewData($event)"
    />
    <v-dialog v-model="deleteFileOrFolderDialog" max-width="500px">
      <v-card>
        <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="deleteFileOrFolderDialog = false">Cancel</v-btn>
          <v-btn color="blue darken-1" text @click="deleteFileOrFolderRequest">OK</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import {Vue, Component, reactive, ref} from "@/importBase";
import ActionsEnum from "@Common/enums/TreeEnum";
import { AddGroupService, IAddGroupRequest, IAddGroupResponse } from "@Services/templates/add-group.service";
import { GetFileService, IGetFileRequest, IGetFileResponse } from "@Services/templates/get-file.service";
import { GetTreeViewService, IGetTreeViewResponse } from "@Services/templates/get-tree-view.service";
import { ISaveFileRequest, SaveFileService } from "@Services/templates/save-file.service";
import CodeEditor from 'simple-code-editor';
import TreeViewActionsDialog from "./open-template-childs/TreeViewActionsDialog.vue";
import ResizableBox from '../../../../common/components/ResizableBox.vue';
import VueJsonPretty from 'vue-json-pretty';
import 'vue-json-pretty/lib/styles.css';
import { GetGenerationTypesService } from "@Services/plans/get-generation-types.service";
import { DeleteFileOrGroupService } from "@Services/templates/delete-file-or-group.service";
import ExamplesHelpers from "./open-template-childs/ExamplesHelpers.vue";

@Component({
  components: {
    CodeEditor,
    TreeViewActionsDialog,
    ResizableBox,
    VueJsonPretty,
    ExamplesHelpers
  }
})
export default class OpenTemplate extends Vue{
  async mounted() {
    await this.getTreeView();
    await this.fetchGenerationTypes();
  }

  public deleteFileOrFolderDialog = false;

  public payload = reactive({
  "planId": "string",
  "planName": "string",
  "mainEntity": {
    "id": "string",
    "name": "string",
    "relations": [
      {
        "from": "string",
        "to": "string",
        "fromPort": "string",
        "toPort": "string"
      }
    ],
    "properties": [
      {
        "id": "string",
        "name": "string",
        "type": {
          "name": "string",
          "value": "string"
        },
        "isNullable": "boolean"
      }
    ]
  },
  "planGroups": [
    {
      "id": "string",
      "name": "string",
      "type": "string"
    }
  ],
  "planApis": [
    {
      "id": "string",
      "name": "string",
      "endpoint": "string",
      "method": {
        "id": "string",
        "name": "string"
      },
      "group": {
        "id": "string",
        "name": "string",
        "type": "string"
      },
      "entity": {
        "id": "string",
        "name": "string",
        "relations": [
          {
            "from": "string",
            "to": "string",
            "fromPort": "string",
            "toPort": "string"
          }
        ],
        "properties": [
          {
            "id": "string",
            "name": "string",
            "type": {
              "name": "string",
              "value": "string"
            },
            "isNullable": "boolean"
          }
        ]
      },
      "apiRequests": [
        {
          "id": "string",
          "name": "string",
          "isNullable": "boolean"
        },
      ],
      "apiResponses": [
        {
          "id": "string",
          "name": "string",
          "isNullable": "boolean"
        }
      ]
    }
  ],
  "planApi": {
    "id": "string",
    "name": "string",
    "endpoint": "string",
    "method": {
      "id": "string",
      "name": "string"
    },
    "group": {
      "id": "string",
      "name": "string",
      "type": "string"
    },
    "entity": {
      "id": "string",
      "name": "string",
      "relations": [
        {
          "from": "string",
          "to": "string",
          "fromPort": "string",
          "toPort": "string"
        }
      ],
      "properties": [
        {
          "id": "string",
          "name": "string",
          "type": {
            "name": "string",
            "value": "string"
          },
          "isNullable": "boolean"
        }
      ]
    },
    "apiRequests": [
      {
        "id": "string",
        "name": "string",
        "isNullable": "boolean"
      },
    ],
    "apiResponses": [
      {
        "id": "string",
        "name": "string",
        "isNullable": "boolean"
      }
    ]
  },
  "generationTypes": [
    {
      "id": "string",
      "name": "string",
      "value": "string"
    }
  ]
})

  public state = reactive({
      val: JSON.stringify(this.payload),
      data: this.payload,
      showLength: false,
      showLine: true,
      showLineNumber: false,
      showDoubleQuotes: true,
      collapsedOnClickBrackets: true,
      useCustomLinkFormatter: false,
      deep: 2,
      deepCollapseChildren: false,
      showIcon: false,
    });

  public template = reactive({
    id: this.$route.query.id.toString(),
    name: this.$route.query.name.toString()
  });

  public getGenerationTypeName(id: string) {
    if (id == "5e532578-f07c-4ba3-9ac6-c4ef664b2085") {
      return "FRONT_END";
    }
    else if (id == "3898f5b4-8850-495c-a527-f9e87a354dab") {
      return "BACK_END";
    }
  }

  public option = {
    left: {
      size: 1,
      buttons: [{ direction: 'right' }]
    },
    center: {
      size: 2,
      buttons: [{
        direction: 'left'
      }, {
        direction: 'right'
      }]
    },
    right: {
      size: 1,
      buttons: [{ direction: 'left' }]
    }
  }

  public newGroupInput = ref('');

  public getTreeViewService: GetTreeViewService = new GetTreeViewService();
  public addGroupService: AddGroupService = new AddGroupService();
  public getTemplateFileService: GetFileService = new GetFileService();
  public saveFileService: SaveFileService = new SaveFileService();
  public getGenerationTypesService: GetGenerationTypesService = new GetGenerationTypesService();
  public deleteFileOrGroupService: DeleteFileOrGroupService = new DeleteFileOrGroupService();

  public async saveFileRequest(body: ISaveFileRequest):Promise<any>{
    return await this.saveFileService.SendAsync(body);
  }

  public async getFile(query: IGetFileRequest):Promise<IGetFileResponse>{
    return await this.getTemplateFileService.RequestAsync(query);
  }

  public selectedItem = reactive({
    id: '',
    name: '',
    action: '',
    contentAdd: '',
    fileEdits: [] as any,
    groupId: '',
    templateId: '',
    type: ''
  });

  public threeViewDialog = false;
  public threeViewData = reactive({
    id: '',
    label: '',
    name: '',
    type: '',
    action: '',
    groupId: null,
    templateId: ''
  });

  public initiallyOpen = ['public'];
  public filesIcons = {
      html: 'mdi-language-html5',
      js: 'mdi-nodejs',
      ts: 'mdi-language-typescript',
      json: 'mdi-code-json',
      md: 'mdi-language-markdown',
      pdf: 'mdi-file-pdf',
      png: 'mdi-file-image',
      ico: 'mdi-file-image',
      txt: 'mdi-file-document-outline',
      xls: 'mdi-file-excel',
      cs: 'mdi-dot-net'
    };

  public tree = [];
  public items = reactive([] as IGetTreeViewResponse[]);

  public generationTypes: any = [];
  public generationType: any = reactive({});

  public async getTreeView():Promise<void>{
    await this.getTreeViewService.RequestAsync({templateId: this.template.id}).then((data) => {
      this.items = data;
    });
  }

  async fetchGenerationTypes() {
      await this.getGenerationTypesService.RequestAsync().then((response) => {
          this.generationTypes = response
      });
  }

  public async changeSelectedFile(event) {
    const eventId = event[0]?.toString();
    this.selectedItem.id = eventId;
    this.selectedItem.name = '';
    this.selectedItem.action = '';
    this.selectedItem.contentAdd = '';
    this.selectedItem.fileEdits = [];
    this.selectedItem.groupId = '';
    this.selectedItem.templateId = '';
    if (event.length > 0) {
      this.findItemTypeById(this.items, eventId);
      if (this.selectedItem.type == 'file') {
        await this.getFile({id: eventId}).then((data) => {
          this.selectedItem.id = data.id;
          this.selectedItem.name = data.name;
          this.selectedItem.action = data.action;
          this.selectedItem.contentAdd = data.contentAdd;
          this.selectedItem.fileEdits = data.fileEdits as any;
          this.selectedItem.groupId = data.groupId;
          this.selectedItem.templateId = data.templateId;
        })
      }
    }
  }

  public rename(item) {
    this.threeViewData = {
      label: 'Rename',
      action: ActionsEnum.RENAME,
      id: item.id,
      name: item.name,
      type: item.type,
      groupId: item.groupId,
      templateId: item.templateId
    }
  
    this.threeViewDialog = true;
  }

  public addFile(item) {
    this.threeViewData = {
      label: 'Add new file',
      action: ActionsEnum.ADD_NEW_FILE,
      id: item.id,
      name: item.name,
      type: item.type,
      groupId: item.groupId,
      templateId: item.templateId
    }

    this.threeViewDialog = true;
  }

  public addFolder(item) {
    this.threeViewData = {
      label: 'Add new folder',
      action: ActionsEnum.ADD_NEW_FOLDER,
      id: item.id,
      name: item.name,
      type: item.type,
      groupId: item.groupId,
      templateId: item.templateId
    }

    this.threeViewDialog = true;
  }

  updateDialog(event) {
    this.threeViewDialog = event;
  }

  emmitThreeViewData(event) {
    this.updateItems(this.items, event);
  }

  findItemTypeById(items: any, id: string) {
    for (const itemChild of items) {
      if (itemChild.id == id) {
        this.selectedItem.type = itemChild.type;
      }
      else if (itemChild.children && itemChild.children.length > 0) {
        this.findItemTypeById(itemChild.children, id);
      }
    }
  }

  updateItems(items: any, event: any) {
    for (let itemChild of items) {
      if (itemChild.id == event.parentId) {
        if (event.action == ActionsEnum.RENAME) {
          itemChild.name = event.name;
        }
        else if (event.action == ActionsEnum.ADD_NEW_FILE) {
          const placeToPush = itemChild.children && !event.isAddedFromFile ? itemChild.children : items;
          placeToPush.push({
            id: event.id,
            name: event.name,
            type: event.type,
            groupId: event.groupId,
            templateId: event.templateId,
            children: event.children
          });
        }
        else if (event.action == ActionsEnum.ADD_NEW_FOLDER) {
          const placeToPush = itemChild.children && !event.isAddedFromFile ? itemChild.children : items;
          placeToPush.push({
            id: event.id,
            name: event.name,
            type: 'folder',
            groupId: event.groupId,
            templateId: event.templateId,
            children: event.children
          });
        }
        break;
      }
      else if (itemChild.children && itemChild.children.length > 0) {
        this.updateItems(itemChild.children, event);
      }
    }
  }

  async createNewGroup() {
    await this.addGroupService.SendAsync({
        name: this.newGroupInput.value,
        type: 'group',
        templateId: this.template.id,
        generationTypeId: this.generationType.id
    }).then((response: IAddGroupResponse) => {
        this.items.push({
          id: response.id,
          name: this.newGroupInput.value,
          type: 'group',
          generationTypeId: this.generationType.id,
          templateId: this.template.id,
          children: []
        })
    })
  }

  async saveFile() {

    const body = {
      id: this.selectedItem.id,
      contentAdd: this.selectedItem.contentAdd,
      fileEdits: this.selectedItem.fileEdits
    }
    await this.saveFileRequest(body);
  }

  public fileOrFolderToDeleteItem: any = reactive({});

  deleteItemTypeById(items: any, id: string) {
    for (const itemChild of items) {
      if (itemChild.id == id) {
        const indexOfItem = items.indexOf(itemChild);
        items.splice(indexOfItem, 1);
      }
      else if (itemChild.children && itemChild.children.length > 0) {
        this.deleteItemTypeById(itemChild.children, id);
      }
    }
  }

  async deleteFileOrFolder(item) {
    this.fileOrFolderToDeleteItem = item;
    this.deleteFileOrFolderDialog = true;
  }

  async deleteFileOrFolderRequest() {
    await this.deleteFileOrGroupService.RequestAsync({
      id: this.fileOrFolderToDeleteItem.id,
      type: this.fileOrFolderToDeleteItem.type
    }).then(() => { 
      this.deleteItemTypeById(this.items, this.fileOrFolderToDeleteItem.id);
      this.fileOrFolderToDeleteItem = reactive({});
      this.deleteFileOrFolderDialog = false;

    })
  }

  newEditContent(fileId: string) {
    this.selectedItem.fileEdits.push({
      id: null,
      placeholder: '',
      content: '',
      fileId: fileId
    })
  }
}
</script>