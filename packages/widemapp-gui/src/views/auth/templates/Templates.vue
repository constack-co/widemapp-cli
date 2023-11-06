<template>
  <div>
    <div class="text-h5"> Manage Templates </div> 

    <div v-if="getProjectId == ''">Please select a project</div>
    <v-data-table
      v-if="getProjectId != ''"
      :headers="headers"
      :items="templates"
      sort-by="calories"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-toolbar
          flat
        >
          <v-dialog
            v-model="dialog"
            max-width="500px"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                color="primary"
                dark
                class="mb-2"
                v-bind="attrs"
                v-on="on"
              >
                New Item
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="text-h5">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                    >
                      <v-text-field
                        v-model="editedItem.name"
                        label="Template Name"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  color="blue darken-1"
                  text
                  @click="close"
                >
                  Cancel
                </v-btn>
                <v-btn
                  color="blue darken-1"
                  text
                  @click="save"
                >
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>

          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeDelete">Cancel</v-btn>
                <v-btn color="blue darken-1" text @click="deleteItemConfirm">OK</v-btn>
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
          
          <!-- OPEN TEMPLATE DIALOG -->
          <v-dialog v-model="dialogOpen" persistent>
            <v-toolbar
              dark
              color="primary"
            >
              <v-btn
                icon
                dark
                @click="dialogOpen = false"
              >
                <v-icon>mdi-close</v-icon>
              </v-btn>
              <v-toolbar-title>Settings</v-toolbar-title>
              <v-spacer></v-spacer>
              <v-toolbar-items>
                <v-btn
                  dark
                  text
                  @click="dialogOpen = false"
                >
                  Save
                </v-btn>
              </v-toolbar-items>
            </v-toolbar>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon
          medium
          class="mr-2"
          @click="editItem(item)"
        >
          mdi-pencil
        </v-icon>
        <v-icon
          medium
          class="mr-2"
          @click="deleteItem(item)"
        >
          mdi-delete
        </v-icon>
        <v-icon
          medium
          @click="openTemplate(item)"
        >
          mdi-magnify-expand
        </v-icon>
      </template>
      <template v-slot:no-data>
        <v-btn
          color="primary"
          @click="getAllTempaltes"
        >
          Load Data
        </v-btn>
      </template>
    </v-data-table>
  </div> 
</template>

<script lang="ts">
import {Vue, Component, Watch} from "@/importBase";
import RouteNameEnum from "@Common/enums/routeNameEnum";
import snackBarMessage from "@Common/funcs/snackBarMessage";
import { AddTemplateService } from "@Services/templates/add-template.service";
import { GetAllTemplatesService } from "@Services/templates/get-all-templates.service";

@Component
export default class TemplatesTable extends Vue{

    async mounted() {
      if (this.getProjectId == '') {
        snackBarMessage.Info('Please select a project');
      }
      else {
        await this.getAllTempaltes();
      }
    }

    get getProjectId() {
        return this.$store.getters.getProjectId;
    }

    @Watch('getProjectId')
    async onProjectIdChanged(value: boolean, oldValue: boolean) {
        await this.getAllTempaltes();
    }
    
    public getAllTemplatesService: GetAllTemplatesService = new GetAllTemplatesService();
    public addTemplateService: AddTemplateService = new AddTemplateService();

    public async getAllTempaltes():Promise<void>{
      await this.getAllTemplatesService.RequestAsync({
        projectId: this.getProjectId
      }).then((data) => {
        this.templates = data;
      });
    }

    public dialog = false;
    public dialogDelete = false;
    public dialogOpen = false;

    public headers = [
        {
          text: 'Template name',
          align: 'start',
          sortable: false,
          value: 'name',
        },
        { text: 'Actions', value: 'actions', sortable: false }
      ];
      
    public templates: any =  [];
    public editedIndex = -1;
    public editedItem = {
        name: ''
    };

    public defaultItem = {
        name: ''
    };

    @Watch('dialog')
    onDialogChanged(value: boolean, oldValue: boolean) {
        this.dialog = value
    }

    @Watch('dialogDelete')
    onDialogDeleteChanged(value: boolean, oldValue: boolean) {
        this.dialogDelete = value
    }

    get formTitle() {
        return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
    }


    editItem (item) {
      this.editedIndex = this.templates.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true;
      snackBarMessage.Info('Edit action will be completed soon')

    };

    deleteItem (item) {
      this.editedIndex = this.templates.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true;
      snackBarMessage.Info('Delete action will be completed soon')

    };

    deleteItemConfirm () {
      this.templates.splice(this.editedIndex, 1)
      this.closeDelete()
    };

    close () {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    };

    closeDelete () {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    };

    async save () {
      if (this.editedIndex > -1) {
        Object.assign(this.templates[this.editedIndex], this.editedItem)
      } else {
        await this.addTemplateService.SendAsync({
          name: this.editedItem.name,
          projectId: this.getProjectId
        }).then((response) => {
          this.templates.push({
            id: response,
            name: this.editedItem.name
          })
        })
      }
      this.close()
    };

    openTemplate(item) {
      this.$router.push({name: RouteNameEnum.TemplatesDetails, query: item});
    }
}
</script>
