<template>
  <div>
    <div class="text-h5"> Manage Entities </div> 
    <div v-if="getProjectId == ''">Please select a project</div>
    <div id="entities-screen" v-if="getProjectId != ''">
      <add-entity 
      :dialog="addEntityDialog" 
      @updateDialog="updateDialog($event)" 
      @entityWasAdded="addNewEntity($event)" />
      
      <div class="viewport" @contextmenu.prevent="$refs.menu.open">
        <screen ref="screen" height="70vh">
          <edge v-for="(edge, edgeIndex) in graph.edges"
            :data="edge"
            :nodes="graph.nodes"
            :key="edge.id+':'+edgeIndex"
          ></edge>
          <node :data="node" ref="node" v-for="(node, nodeIndex) in graph.nodes" :key="node.id+':'+nodeIndex" class="overflow-visible customNodeStyle">
            <v-sheet
            width="350px"
            elevation="10"
            >
            <v-card
              class="mx-auto"
            >
            <v-toolbar
            flat
            color="primary accent-4"
            dark
          >
            <v-toolbar-title>{{ renderEntity(node).name }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="comingSoon()">
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </v-toolbar>
              <v-list>
                <v-list-item-group>
                  <v-row class="text-caption">
                    <v-col cols="6">
                      <span class="ml-2">
                        Property Name
                      </span>
                      
                    </v-col>
                    <v-col>
                      <span class="ml-2">
                        Type
                      </span>
                    </v-col>
                    <v-col>
                      Is Nullable
                    </v-col>
                  </v-row>
                  <v-list-item
                  :ripple="false"
                    v-for="(property, propertyIndex) in renderEntity(node).properties"
                    :key="property.id+':'+propertyIndex"
                  >
                    <v-list-item-action class="inputs-dots mr-0">
                        <div v-for="(input, inputIndex) in node.inputs.filter(x => x === 'i'+property.id)" :key="node.id+':'+input+':'+inputIndex">
                          <port ref="port"
                            :id="node.id+':'+input"
                            :edgesTo="getInputEdges(node, input)">
                              <div class="port-inner-inputs"
                                @mousedown.prevent.stop="evt => startConnect(node, { input }, evt)"
                                @mouseup.prevent.stop="createConnect(node, { input })"
                                :class="getInputEdges(node, input).length && 'connected'">
                              </div>
                            </port>
                        </div>
                    </v-list-item-action>
                    <v-list-item-content class="pb-0 pt-0">
                      <v-list-item-title>
                        <v-row>
                          <v-col cols="6">
                            <v-text-field
                            v-model="property.name"
                            name="name"
                            @change="updateProperty(property.id, property.name, 'Name')"
                            id="id"
                          ></v-text-field>
                          </v-col>
                          <v-col cols="4">
                            <v-autocomplete
                                class="mt-2"
                                v-model="property.type"
                                :items="propertyTypes"
                                outlined
                                dense
                                item-text="name"
                                @change="updateProperty(property.id, property.type, 'PropertyTypeId')"
                                return-object
                            ></v-autocomplete>
                          </v-col>
                          <v-col cols="2">
                            <v-checkbox
                              full-width
                              v-model="property.isNullable"
                              @change="updateProperty(property.id, property.isNullable, 'IsNullable')"
                            ></v-checkbox>
                          </v-col>
                        </v-row>
                      </v-list-item-title>
                    </v-list-item-content>

                    <v-list-item-action class="outputs-dots ml-0">
                      <div v-for="(output, outputIndex) in node.outputs.filter(x => x == 'o'+property.id)" :key="node.id+':'+output+':'+outputIndex">
                        <port ref="port"
                          :id="node.id+':'+output"
                          :edgesFrom="getOutputEdges(node, output)">
                            <div class="port-inner-outputs"
                              @mousedown.prevent.stop="evt => startConnect(node, { output }, evt)"
                              @mouseup.prevent.stop="createConnect(node, { output })"
                              :class="getOutputEdges(node, output).length && 'connected'">
                            </div>
                        </port>
                      </div>
                    </v-list-item-action>
                  </v-list-item>
                  <v-list-item>
                    <v-list-item-content class="pb-0">
                        <v-row>
                          <v-col cols="8">
                            <v-text-field
                            v-model="newPropertyInput[renderEntity(node).id]"
                            dense
                            label="New property"
                            required
                          ></v-text-field> 
                          </v-col>
                          <v-col cols="4">

                          <v-btn
                            dense
                            class="green pa-0 ma-0"
                            @click="addNewProperty(renderEntity(node).id)"
                          >
                            <v-icon>mdi-check</v-icon>
                          </v-btn>
                        </v-col>
                        </v-row>
                    </v-list-item-content>
                  </v-list-item>
                </v-list-item-group>
              </v-list>
            </v-card>
            </v-sheet>
          </node>
        </screen>
      </div>
      <vue-context ref="menu">
          <li>
              <a @click.prevent="addEntityDialog = true">Add new entity</a>
          </li>
      </vue-context>
    </div>
  </div>  
</template>

<script>
import Screen from 'vnodes/src/components/Screen.vue';
import Node from 'vnodes/src/components/Node.vue';
import Edge from 'vnodes/src/components/Edge.vue';
import Port from 'vnodes/src/components/Port.vue';
import graph from 'vnodes/src/graph';
import AddEntity from './AddEntity.vue';
import VueContext from 'vue-context';
import { AddRelationService } from '../../../services/entities/add-relation.service';
import { GetEntitiesService } from '../../../services/entities/get-entities.service';
import { GetEntityRelationsService } from '../../../services/entities/get-entity-relations.service';
import { DeleteEntityRelationService } from '../../../services/entities/delete-entity-relation.service';
import { AddPropertyService } from '../../../services/entities/add-property.service';
import { UpdatePropertyService } from '../../../services/entities/update-property.service';
import { GetPropertyTypesService } from '../../../services/entities/get-property-types.service';
import snackBarMessage from "@Common/funcs/snackBarMessage";


export default {
  components: {
    Screen,
    Node,
    Edge,
    Port,
    AddEntity,
    VueContext
  },
  data() {
    return {
      addRelationService: new AddRelationService(),
      getEntitiesService: new GetEntitiesService(),
      getEntityRelationsService: new GetEntityRelationsService(),
      deleteEntityRelationService: new DeleteEntityRelationService(),
      addPropertyService: new AddPropertyService(),
      updatePropertyService: new UpdatePropertyService(),
      getPropertyTypesService: new GetPropertyTypesService(),
      addRelationModel: {
        entityFromId: null,
        entityToId: null,
        propertyFromId: null,
        propertyToId: null
      },
      deleteRelationModel: {
        entityFromId: null,
        entityToId: null,
        propertyFromId: null,
        propertyToId: null
      },
      addEntityDialog: false,
      graph: new graph(),
      connecting: null, // { node: {}, input: str, output: str }
      mousePrev: { x: 0, y: 0 },
      zoom: 1,
      entities: [],
      relations: [],
      newPropertyInput: {},
      projectId: '',
      propertyTypes: []
    }
  },

  computed: {
    activeEdge: vm => vm.graph.edges.find(e => e.active),
    getProjectId () {
      return this.$store.getters.getProjectId;
    }
  },

  watch: {
    async getProjectId(value) {
      this.projectId = value;
      await this.initialize();
    }
  },

  async mounted () {
    if (this.getProjectId == '') {
      snackBarMessage.Info('Please select a project');
    }
    else {
      this.projectId = this.getProjectId;
      await this.initialize();
    }
  }
  ,
  beforeDestroy () {
    document.removeEventListener('mouseup', this.cancelConnect)
    document.removeEventListener('mousemove', this.onmousemove)
  },
  
  methods: {
    comingSoon() {
      snackBarMessage.Info('Available soon')
    },

    clearData() {
      this.entities = [];
      this.relations = [];
      this.graph = new graph();
    },

    async initialize() {
      this.clearData();

      await this.fetchEntities(this.projectId);
    
      await this.fetchEntityRelations();

      await this.fetchPropertyTypes();
      
      for (const entity of this.entities) {
        this.graph.createNode({
            id: entity.id,
            inputs: entity.inputs,
            outputs: entity.outputs,
          });
        }

      for (const relation of this.relations) {
        this.graph.createEdge({
          from: relation.from,
          to: relation.to,
          fromPort: relation.fromPort,
          toPort: relation.toPort,
          active: relation.active,
          type: relation.type
        })
      }

      this.$nextTick(() => {
        this.graph.graphNodes({ spacing: 150 })
        this.$refs.screen.zoomNodes(this.graph.nodes, { scale: 0 })
        this.graph.edges.forEach(edge => {
            this.$set(edge.fromAnchor, 'snap', "anchor")
            this.$set(edge.toAnchor, 'snap', "anchor")
          })
      })
      document.addEventListener('mouseup', this.cancelConnect)
      document.addEventListener('mousemove', this.onmousemove)
    },

    addNewEntity(event) {
      this.entities.push(event);
      this.graph.createNode({
        id: event.id,
        inputs: event.inputs,
        outputs: event.outputs
      });
    },

    async addNewProperty(entityId) {
      const body = {
        name: this.newPropertyInput[entityId],
        isNullable: false, // temp
        propertyTypeId: "f8faca99-eaf9-4380-a044-c752caaff3a2", // temp
        entityId: entityId
      }

      this.newPropertyInput = {}
      
      await this.addPropertyService.SendAsync(body).then((data) => {
        const entity = this.entities.find(x => x.id == entityId);
        entity.properties.push({
          id: data.id,
          isNullable: false,
          name: body.name,
          type: {name: "string", value: "string"} // temp
        })
        entity.inputs.push(`i${data.id}`)
        entity.outputs.push(`o${data.id}`)
      })
    },

    async updateProperty(propertyId, value, attributeToUpdate) {
      const body = {
        propertyId: propertyId,
        attributeToUpdate: attributeToUpdate,
        name: attributeToUpdate == 'Name' ? value : null,
        isNullable: attributeToUpdate == 'IsNullable' ? value : null,
        propertyTypeId: attributeToUpdate == 'PropertyTypeId' ? value?.id : null
      };
      await this.updatePropertyService.SendAsync(body).then(()=> {
        snackBarMessage.Success('Updated')
      })
    },

    async addRelation() {
      if (this.addRelationModel.propertyFromId !== null && this.addRelationModel.propertyFromId.startsWith("o")) {
        this.addRelationModel.propertyFromId = this.addRelationModel.propertyFromId.substring(1);
      }
      if (this.addRelationModel.propertyToId !== null && this.addRelationModel.propertyToId.startsWith("i")) {
        this.addRelationModel.propertyToId = this.addRelationModel.propertyToId.substring(1);
      }
      
      await this.addRelationService.SendAsync(this.addRelationModel).then(() => {
        this.addRelationModel = {
          entityFromId: null,
          entityToId: null,
          propertyFromId: null,
          propertyToId: null
        }
      });
    },

    async removeRelation() {
      if (this.deleteRelationModel.propertyFromId !== null && this.deleteRelationModel.propertyFromId.startsWith("o")) {
        this.deleteRelationModel.propertyFromId = this.deleteRelationModel.propertyFromId.substring(1);
      }
      if (this.deleteRelationModel.propertyToId !== null && this.deleteRelationModel.propertyToId.startsWith("i")) {
        this.deleteRelationModel.propertyToId = this.deleteRelationModel.propertyToId.substring(1);
      }

      if (this.deleteRelationModel.entityFromId != null && 
      this.deleteRelationModel.entityToId == null &&
      this.deleteRelationModel.propertyFromId == null &&
      this.deleteRelationModel.propertyToId == null) return;
      
      await this.deleteEntityRelationService.RequestAsync(this.deleteRelationModel).then(() => {
        this.deleteRelationModel = {
          entityFromId: null,
          entityToId: null,
          propertyFromId: null,
          propertyToId: null
        }
      });
    },

    async fetchEntities(projectId) {
      await this.getEntitiesService.RequestAsync({projectId: projectId}).then((response) => {
        this.entities = response;

        for (const item of response) {
          this.newPropertyInput[item.id] = ''
        }
      });
    },

    async fetchPropertyTypes() {
      await this.getPropertyTypesService.RequestAsync().then((response) => {
        this.propertyTypes = response;
      });
    },

    async fetchEntityRelations() {
      await this.getEntityRelationsService.RequestAsync().then((response) => {
        for (const item of response) {
          this.relations.push({
            from: item.from,
            to: item.to,
            fromPort: item.fromPort,
            toPort: item.toPort,
            active: false,
            type: 'hsmooth'
          })
        }
      });
    },

    updateDialog(event) {
      this.addEntityDialog = event;
    },

    renderEntity(node) {
      return this.entities.find(x => x.id == node.id);
    },

    startConnect (node, {input, output }, evt) {
      if (typeof input === 'undefined' && typeof output !== 'undefined') {
        this.addRelationModel.entityFromId = node.id;
        this.addRelationModel.propertyFromId = output;
      }
      else if (typeof input !== 'undefined' && typeof output === 'undefined') {
        this.addRelationModel.entityToId = node.id;
        this.addRelationModel.propertyToId = input;

        this.deleteRelationModel.entityToId = node.id;
        this.deleteRelationModel.propertyToId = input;
      } 

      if (this.connecting) return;
      const port = this.$refs.port.find(p => p.$attrs.id === `${node.id}:${input || output}`)
      const edge = input && this.getInputEdges(node, input).reverse()[0]
      if (edge) { // edit exiting edge
        edge.active = true
        this.connecting = {
          node: this.graph.nodes.find(n => input ? edge.from === n.id : edge.to === n.id),
          input: output,
          output: input
        }
      } else { // new edge
        this.graph.createEdge({
          from: node.id,
          to: node.id,
          fromPort: input || output,
          toPort: input || output,
          fromAnchor: { ...port.offset },
          toAnchor: { ...port.offset },
          active: true,
          type: 'hsmooth'
        })
        this.connecting = {
          node, input, output
        }
      }

      this.mousePrev = { x: evt.clientX, y: evt.clientY }
      this.zoom = this.$refs.screen.panzoom.getZoom()
    },
    async createConnect (node, {input, output}) {
      if (typeof input === 'undefined' && typeof output !== 'undefined') {
        this.addRelationModel.entityFromId = node.id;
        this.addRelationModel.propertyFromId = output;

        this.deleteRelationModel.entityFromId = node.id;
        this.deleteRelationModel.propertyFromId = output;
      }
      else if (typeof input !== 'undefined' && typeof output === 'undefined') {
        this.addRelationModel.entityToId = node.id;
        this.addRelationModel.propertyToId = input;

      }
      if (!this.connecting) return
      if (this.isValidConnection({node, input, output}, this.connecting)) {
        if (input) {
          this.activeEdge.to = node.id
          this.activeEdge.toPort = input
        } else if (output) {
          this.activeEdge.from = node.id
          this.activeEdge.fromPort = output
        }
        await this.addRelation();
        this.stopConnect()
      } else {
        await this.cancelConnect()
      }
    },
    async cancelConnect () {
      if (!this.connecting) return
      this.graph.removeEdge(this.activeEdge)
      
      this.deleteRelationModel.entityFromId = this.connecting.node.id;
      await this.removeRelation();
      this.stopConnect()
    },
    stopConnect () {
      if (this.activeEdge) {
        this.activeEdge.active = false
      }
      this.$nextTick(() => {
        this.connecting = null
      })
    },
    isValidConnection (conA, conB) {
      return (conA && conB && conA.node && conB.node)
       && (conA.node !== conB.node)
       && (conA.input && conB.output || conB.input && conA.output)
    },
    // edges that go to this input
    getInputEdges (node, input) {
      return this.graph.edges
        .filter(e => e.to === node.id && e.toPort === input)
    },
    // edges that start from this output
    getOutputEdges (node, output) {
      return this.graph.edges
        .filter(e => e.from === node.id && e.fromPort === output)
    },
    onmousemove (e) {
      if (this.connecting) {
        const offset =  {
          x: (e.clientX - this.mousePrev.x) / this.zoom,
          y: (e.clientY - this.mousePrev.y) / this.zoom
        }

        const anchor = this.connecting.input
          ? this.activeEdge.fromAnchor
          : this.activeEdge?.toAnchor

        if (typeof anchor !== "undefined") {
          anchor.x += offset.x
          anchor.y += offset.y
        }
      
        this.mousePrev = { x: e.clientX, y: e.clientY }
      }
    }
  }
}
</script>

<style scoped>

.customNodeStyle {
  opacity: 0.8 !important;
}

.inputs-dots {
  margin-left: -30px;
}

.outputs-dots {
  margin-right: -35px;
}

.port-inner-inputs {
  width: 20px;
  height: 20px;
  border-radius: 10px;
  background-color: green;
  display: inline-block;
  cursor: pointer;
}

.port-inner-outputs {
  width: 20px;
  height: 20px;
  border-radius: 10px;
  background-color: blue;
  display: inline-block;
  cursor: pointer;
}


.port-inner-inputs:hover, .port-inner-outputs:hover {
  background-color: yellow;
}

.port-inner-inputs.connected, .port-inner-outputs.connected {
  background-color: red;
}

</style>