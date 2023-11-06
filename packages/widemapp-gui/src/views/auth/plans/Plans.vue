<template>
    <div>
        <div class="text-h5"> Configure new plan </div> 
        <div v-if="getProjectId == ''">Please select a project</div>
        <template v-if="getProjectId != ''">
            <v-stepper v-model="step">
                <v-stepper-header>
                <v-stepper-step
                    :complete="step > 1"
                    step="1"
                >
                    Step 1
                </v-stepper-step>

                <v-divider></v-divider>

                <v-stepper-step
                    :complete="step > 2"
                    step="2"
                >
                    Step 2
                </v-stepper-step>
                </v-stepper-header>

                <v-stepper-items>
                <v-stepper-content step="1">
                    <v-card
                    class="mb-12"
                    height="auto"
                    elevation="0"
                    >
                        <v-row>
                            <v-col cols="12" md="4">
                                <v-text-field
                                    v-model="payload.planName"
                                    label="Plan Name"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12" md="4">
                                <v-select
                                    multiple
                                    :items="generationTypes"
                                    v-model="payload.generationTypes"
                                    item-text="name"
                                    label="Select generation type"
                                    return-object
                                ></v-select>
                            </v-col>
                            <v-col cols="12" md="4">
                                <v-select
                                    :items="entities"
                                    v-model="payload.mainEntity"
                                    item-text="name"
                                    label="Select main entity"
                                    return-object
                                ></v-select>
                            </v-col>
                            <v-col cols="12" md="4">
                                <v-select
                                    multiple
                                    v-model="payload.operations"
                                    :items="operations"
                                    label="Select Operations"
                                ></v-select>
                            </v-col>
                            <v-col cols="12" md="4">
                                <v-autocomplete
                                    v-model="payload.template"
                                    :items="templates"
                                    chips
                                    small-chips
                                    item-text="name"
                                    label="Select Template"
                                    return-object
                            ></v-autocomplete>
                            </v-col>
                        </v-row>
                    </v-card>

                    <v-btn text disabled>
                    Previous
                    </v-btn>
                    <v-btn
                    color="primary"
                    @click="nextClick({
                        step: 2
                    })">
                    Next
                    </v-btn>

                    <v-btn text 
                    color="orange"
                    >
                    Draft
                    </v-btn>
                    <v-btn text color="default" @click="clearPlanConfiguration">
                    Clear
                    </v-btn>
                </v-stepper-content>

                <v-stepper-content step="2">
                    <v-card
                    height="auto"
                    elevation="0"
                    ></v-card>
                    
                    <v-data-table
                        :headers="operationsTableHeader"
                        :items="payload.apis"
                        item-key="id"
                        class="elevation-1"
                        show-expand
                        >
                        <template v-slot:[`item.name`]="props">
                            <v-edit-dialog
                                :return-value.sync="props.item.name"
                                large
                                persistent
                            >
                            {{ props.item.name }}
                            <template v-slot:input>
                                <v-text-field
                                    v-model="props.item.name"
                                    label="Edit Name"
                                    single-line
                                    counter
                                    autofocus
                                ></v-text-field>
                            </template>
                            </v-edit-dialog>
                        </template>
                        <template v-slot:[`item.method`]="props">
                            <v-edit-dialog
                                :return-value.sync="props.item.method"
                                large
                                persistent          
                            >
                            {{ props.item.method.name }}
                            <template v-slot:input>
                                <v-select
                                    v-model="props.item.method"
                                    :items="httpMethods"
                                    item-text="name"
                                    label="Select Operations"
                                    return-object
                                ></v-select>
                            </template>
                            </v-edit-dialog>
                        </template>
                        <template v-slot:[`item.endpoint`]="props">
                            <v-edit-dialog
                                :return-value.sync="props.item.endpoint"
                                large
                                persistent
                            >
                            {{ props.item.endpoint }}
                            <template v-slot:input>
                                <v-text-field
                                    v-model="props.item.endpoint"
                                    label="Edit"
                                    single-line
                                    counter
                                    autofocus
                                ></v-text-field>
                            </template>
                            </v-edit-dialog>
                        </template>
                        <template v-slot:[`item.entity`]="props">
                            <v-edit-dialog
                                :return-value.sync="props.item.entity"
                                large
                                persistent          
                            >
                            {{ props.item.entity.name }}
                            <template v-slot:input>
                                <v-select
                                    :items="entities"
                                    v-model="props.item.entity"
                                    item-text="name"
                                    label="Select Entity"
                                    return-object
                                ></v-select>
                            </template>
                            </v-edit-dialog>
                        </template>
                        <template v-slot:[`item.templateGroup`]="props">
                            <v-edit-dialog
                                :return-value.sync="props.item.templateGroup"
                                large
                                persistent          
                            >
                            {{ props.item.templateGroup.name }}
                            <template v-slot:input>
                                <v-autocomplete
                                v-model="props.item.templateGroup"
                                :items="templateGroups"
                                outlined
                                dense
                                chips
                                small-chips
                                item-text="name"
                                label="Select Template Group"
                                return-object
                            ></v-autocomplete>
                            </template>
                            </v-edit-dialog>
                        </template>
                        <template v-slot:[`item.actions`]="{ item }">
                        <v-icon
                            small
                            @click="duplicateApi(item)"
                        >
                            mdi-content-duplicate
                        </v-icon>
                        </template>
                        <template v-slot:expanded-item="{ headers, item }">
                            <td :colspan="headers.length">
                                <v-row>
                                    <v-col cols="12" md="6">
                                        <div class="text-subtitle-1">Requests </div>
                                        <v-divider></v-divider>
                                        <ComboboxRequests 
                                        :requestPropsItems="requestPropsItems" 
                                        :requestPropsModel="payload.apis.find(x => x.id == item.id).requestProps" 
                                        :api="item" 
                                        @updateRequestPropsItem="updateRequestPropsItem($event)"
                                        @updateRequestPropsModel="updateRequestPropsModel($event)"
                                        />
                                    </v-col>
                                    <v-col cols="12" md="6">
                                        <div class="text-subtitle-1">Responses</div>
                                        <v-divider></v-divider>
                                        <ComboboxResponses 
                                        :responsePropsItems="responsePropsItems" 
                                        :responsePropsModel="payload.apis.find(x => x.id == item.id).responseProps" 
                                        :api="item" 
                                        @updateResponsePropsItem="updateResponsePropsItem($event)"
                                        @updateResponsePropsModel="updateResponsePropsModel($event)"/>
                                    </v-col>
                                </v-row>
                            </td>
                        </template>
                    </v-data-table>

                    <v-divider class="mt-2"></v-divider>

                    <v-row class="mt-2">   
                        <v-col cols="12" md="3">
                            <v-autocomplete
                                v-model="payload.planGroups"
                                :items="templateGroups"
                                item-text="name"
                                outlined
                                dense
                                chips
                                small-chips
                                label="Plan Template groups"
                                multiple
                                return-object
                            ></v-autocomplete>
                        </v-col>
                    </v-row>
                    
                    <v-btn text
                    @click="nextClick({
                        step: 1
                    })">
                    Previous
                    </v-btn>
                    <v-btn
                    color="primary"
                    @click="finishPlan()">
                    Finish
                    </v-btn>
                    <v-btn text color="orange">
                    Draft
                    </v-btn>
                    <v-btn text color="default" @click="clearPlanConfiguration">
                    Clear
                    </v-btn>
                </v-stepper-content>
                </v-stepper-items>
            </v-stepper>
        </template>
    </div>
</template>

<script lang="ts">
import {Vue, Component, reactive, Watch} from "@/importBase";
import snackBarMessage from "@Common/funcs/snackBarMessage";
import { GetEntitiesService } from "@Services/entities/get-entities.service";
import { GetTemplateGroupsService, IGetTemplateGroupsRequest } from "@Services/groups/get-template-groups.service";
import { GetMethodsService } from "@Services/methods/get-methods.service";
import { ApiTypeRequest, FinishPlanService, IFinishPlanRequest } from "@Services/plans/finish-plan.service";
import { GetGenerationTypesService } from "@Services/plans/get-generation-types.service";
import { GetAllTemplatesService } from "@Services/templates/get-all-templates.service";
import ComboboxRequests from "./ComboboxRequests.vue";
import ComboboxResponses from "./ComboboxResponses.vue";

@Component({
    components: {
        ComboboxRequests,
        ComboboxResponses
    }
})
export default class Plans extends Vue{  
    async mounted() {
        if (this.getProjectId == '') {
            snackBarMessage.Info('Please select a project');
        }
        else {
            await this.initialize();
        }
    }

    async initialize() {
        await this.fetchEntities();
        await this.fetchGenerationTypes();
        await this.fetchMethods();
        await this.fetchAllTempaltes();  
    }

    get getProjectId() {
        return this.$store.getters.getProjectId;
    }

    @Watch('getProjectId')
    async onProjectIdChanged(value: boolean, oldValue: boolean) {
        await this.initialize();
    }

    getEntitiesService = new GetEntitiesService();
    getGenerationTypesService = new GetGenerationTypesService();
    getMethodsService = new GetMethodsService();
    getAllTemplatesService = new GetAllTemplatesService();
    finishPlanService = new FinishPlanService();
    getTemplateGroupsService = new GetTemplateGroupsService();

    public step = 1;

    public payload: any = reactive({
        planName: '',
        generationTypes: [],
        mainEntity: {
            id: '',
            name: ''
        },
        operations: [],
        apis: [],
        planGroups: [],
        template: {}
    });

    public generationTypes: any = [];
    
    public entities: any = [];

    public operations = ['GET ALL', 'GET BY', 'ADD', 'UPDATE', 'DELETE'];

    public httpMethods: any = [];

    public templates: any = [];

    public templateGroups: any = [];

    public operationsTableHeader = [
        {
            text: 'Name',
            value: 'name',
        },
        {
            text: 'Method',
            value: 'method',
        },
        {
            text: 'Endpoint',
            value: 'endpoint',
        },
        { 
            text: 'Entity', 
            value: 'entity' 
        },
        { 
            text: 'Template Group', 
            value: 'templateGroup' 
        },
        { text: 'Actions', value: 'actions', sortable: false },
        { text: '', value: 'data-table-expand' }
    ];

    public requestPropsItems: any = [
        { header: 'Select an option or create one' }
    ]

    public responsePropsItems: any = [
        { header: 'Select an option or create one' }
    ]

    public operationsDefaultTemplate = (entity?: any) => [
        {
            selector: 'GET ALL',
            endpoint: `api/${entity}`, 
            method: {
                id: "ec0f4dff-3d31-443e-bbfd-244751b94e3c", 
                name: "GET"
            },
        },
        {
            selector: 'GET BY',
            endpoint: `api/${entity}`, 
            method: {
                id: "ec0f4dff-3d31-443e-bbfd-244751b94e3c", 
                name: "GET"
            },
        },
        {
            selector: 'ADD',
            endpoint: `api/${entity}`, 
            method: {
                id: "bbe4915f-76d5-453f-bf75-0961353ed714", 
                name: "POST"
            },
        },
        {
            selector: 'UPDATE',
            endpoint: `api/${entity}`, 
            method: {
                id: "45895712-87d0-48ad-94d5-54771bd90f81", 
                name: "PUT"
            },
        },
        {
            selector: 'DELETE',
            endpoint: `api/${entity}`, 
            method: {
                id: "54bf848a-4653-4905-9a7c-6c9af0fc3f17", 
                name: "DELETE"
            },
        },
    ];

    async fetchEntities() {
        await this.getEntitiesService.RequestAsync({
            projectId: this.getProjectId
        }).then((response) => {
            this.entities = response;
        });
    }

    async fetchGenerationTypes() {
        await this.getGenerationTypesService.RequestAsync().then((response) => {
            this.generationTypes = response
        });
    }

    async fetchMethods() {
        await this.getMethodsService.RequestAsync().then((response) => {
            this.httpMethods = response
        });
    }

    async fetchAllTempaltes():Promise<void>{
        await this.getAllTemplatesService.RequestAsync({
            projectId: this.getProjectId
        }).then((response) => {
            this.templates = response;
        });
    }

    async fetchTemplateGroups(data: IGetTemplateGroupsRequest):Promise<void>{
        await this.getTemplateGroupsService.RequestAsync(data).then((response) => {
            this.templateGroups = response;
        });
    }

    uuidv4() {
        return ([1e7].toString()+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, (c: any) =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        );
    }

    async nextClick(event: {step: number}) {
        this.step = event.step;

        if (event.step == 2 && this.payload.operations) {
            for (const operation of this.payload.operations) {
                const operationSelected = this.operationsDefaultTemplate(this.payload.mainEntity.name).find(x => x.selector == operation);
                if (operationSelected && !this.payload.apis.some(x => x.selector == operation)) {
                    this.payload.apis.push({
                        id: this.uuidv4(),
                        selector: operationSelected.selector,
                        name: '',
                        method: operationSelected.method,
                        endpoint: operationSelected.endpoint,
                        entity: this.payload.mainEntity,
                        templateGroup: {},
                        requestProps: [],
                        responseProps: []
                    })
                }
            }

            for (const property of this.payload.mainEntity.properties) {
                this.requestPropsItems.push({
                    text: `${property.name}|${property.type.name}|${property.isNullable}`,
                    color: 'default lighten-3',
                })

                this.responsePropsItems.push({
                    text: `${property.name}|${property.type.name}|${property.isNullable}`,
                    color: 'default lighten-3',
                })
            }
        }

        await this.fetchTemplateGroups({templateId: this.payload.template.id});
    }

    updateRequestPropsItem(event) {
        this.requestPropsItems.push(event.prop);
    }

    updateResponsePropsItem(event) {
        this.responsePropsItems.push(event.prop);
    }

    updateRequestPropsModel(event) {
        if (event.action == 'ADD') {
            const requestPropExist = this.payload.apis.find(x => x.id == event.item.id).requestProps?.some(x => x.text == event.prop.text);
            if (!requestPropExist) {
                this.payload.apis.find(x => x.id == event.item.id).requestProps.push(event.prop)
            }
        }
        else if (event.action == 'REMOVE') {
            const requestProps = this.payload.apis.find(x => x.id == event.item.id).requestProps;
            const index = requestProps.indexOf(requestProps?.find(x => x.text == event.prop.text))
            requestProps.splice(index, 1);
        }
    }

    updateResponsePropsModel(event) {
        if (event.action == 'ADD') {
            const responsePropExist = this.payload.apis.find(x => x.id == event.item.id).responseProps?.some(x => x.text == event.prop.text);
            if (!responsePropExist) {
                this.payload.apis.find(x => x.id == event.item.id).responseProps.push(event.prop)
            }
        }
        else if (event.action == 'REMOVE') {
            const responseProps = this.payload.apis.find(x => x.id == event.item.id).responseProps;
            const index = responseProps.indexOf(responseProps?.find(x => x.text == event.prop.text))
            responseProps.splice(index, 1);
        }
    }

    duplicateApi(item) {
        const itemToAdd = {
            id: this.uuidv4(),
            selector: item.selector,
            name: item.name,
            endpoint: item.endpoint,
            entity: item.entity,
            method: item.method,
            requestProps: [],
            responseProps: [],
            templateGroup: item.templateGroup
        };
        this.payload.apis.push(itemToAdd);
    }

    clearPlanConfiguration() {
        this.payload = {
            planName: '',
            generationTypes: [],
            mainEntity: {
                id: '',
                name: ''
            },
            operations: [],
            apis: [],
            planGroups: [],
            template: {}
        };
    }

    async finishPlan() {
        const apis: ApiTypeRequest[] = [];
        this.payload.apis.forEach(item => {
            const apiRequests: any = [];
            item.requestProps.forEach(requestProp => {
                apiRequests.push({
                    name: requestProp.text.split('|')[0],
                    type: requestProp.text.split('|')[1],
                    isNullable: requestProp.text.split('|')[2] === 'true'
                })
            })

            const apiResponses: any = [];
            item.responseProps.forEach(responseProp => {
                apiResponses.push({
                    name: responseProp.text.split('|')[0],
                    type: responseProp.text.split('|')[1],
                    isNullable: responseProp.text.split('|')[2] === 'true'
                })
            })

            apis.push({
                id: item.id,
                name: item.name,
                methodId: item.method.id,
                endpoint: item.endpoint,
                groupName: item.templateGroup.name,
                entityId: item.entity.id,
                apiRequests: apiRequests,
                apiResponses: apiResponses
            })
        })

        const generateTypeIds: string[] = [];
        this.payload.generationTypes.forEach(item => {
            generateTypeIds.push(item.id)
        })

        const planGroupNames: string[] = [];
        this.payload.planGroups.forEach(item => {
            planGroupNames.push(item.name)
        })

        const body: IFinishPlanRequest = {
            planName: this.payload.planName,
            mainEntityId: this.payload.mainEntity.id,
            templateId: this.payload.template.id,
            generateTypeIds: generateTypeIds,
            planGroupNames: planGroupNames,
            apis: apis
        };
        await this.finishPlanService.SendAsync(body).then((data) => {
            // console.log(data)
            // this.clearPlanConfiguration()
        });
        
        // save

        // clear

        // go to step 1
    }
}
</script>
