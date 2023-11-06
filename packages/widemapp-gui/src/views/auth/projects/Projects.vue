<template>
    <div>
        <div class="text-h5"> Manage Projects </div> 

        <v-data-table
            :headers="headers"
            :items="projects"
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
        </template>
        <template v-slot:no-data>
            <v-btn
                color="primary"
                @click="getAllProjects"
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
import { AddProjectService } from "@Services/projects/add-project.service";
import { GetProjectsService } from "@Services/projects/get-projects.service";

@Component
export default class Projects extends Vue{

    async mounted() {
        await this.getAllProjects();
    }
    
    public projects: any =  [];

    public getProjectsService: GetProjectsService = new GetProjectsService();
    public addProjectService: AddProjectService = new AddProjectService();

    public async getAllProjects():Promise<void>{
        await this.getProjectsService.RequestAsync().then((data) => {
            this.projects = data;
        });
    }

    public dialog = false;
    public dialogDelete = false;
    public dialogOpen = false;

    public headers = [
        {
            text: 'Project name',
            align: 'start',
            sortable: false,
            value: 'name',
        },
        { text: 'Actions', value: 'actions', sortable: false }
    ];

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
        this.editedIndex = this.projects.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
        snackBarMessage.Info('Edit action will be completed soon')

    };

    deleteItem (item) {
        this.editedIndex = this.projects.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
        snackBarMessage.Info('Delete action will be completed soon')

    };

    deleteItemConfirm () {
        this.projects.splice(this.editedIndex, 1)
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
                Object.assign(this.projects[this.editedIndex], this.editedItem)
            } else {
            await this.addProjectService.SendAsync({
                name: this.editedItem.name
            }).then((response) => {
                let projects = this.$store.getters.getProjects
                projects.push({
                    id: response,
                    name: this.editedItem.name
                })
                this.$store.dispatch('setProjects', projects)
                this.projects.push(this.editedItem)
            })
            }
        this.close()
    };
}
</script>
