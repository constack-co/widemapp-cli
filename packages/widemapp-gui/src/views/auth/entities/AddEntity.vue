<template>
    <v-dialog
      v-model="dialogChild"
      persistent
      max-width="290"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          color="primary mb-2"
          dark
          v-bind="attrs"
          v-on="on"
          :disabled="ifNotProjectId()"
        >
          Add new entity
        </v-btn>
      </template>
      <v-card>
        <v-card-title class="text-h5">
          Add new entity
        </v-card-title>
        <v-card-text>
          <v-text-field
            dense
            v-model="model.name"
            name="name"
            label="Name"
          ></v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click="closeDialogChild()"
          >
            Close
          </v-btn>
          <v-btn
            color="green darken-1"
            text
            @click="addEntity()"
          >
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script lang="ts">
import {Vue, Component, Prop} from "@/importBase";
import { AddEntityService, IAddEntityRequest } from "@Services/entities/add-entity.service";
import { reactive } from "@vue/composition-api";

@Component
export default class AddEntity extends Vue{

    public addEntityService: AddEntityService = new AddEntityService();

    public model: IAddEntityRequest = reactive({
      name: '',
      projectId: ''
    });

    public ifNotProjectId () {
      return this.$store.getters.getProjectId === "";
    }

    public async addEntity() {
      this.model.projectId = this.$store.getters.getProjectId;
      await this.addEntityService.SendAsync(this.model).then((data) => {
        this.$emit('entityWasAdded', {
          id: data.id,
          inputs: [],
          name: this.model.name,
          outputs: [],
          properties: []
        })
        this.closeDialogChild();
        this.model.name = '';
      });
    }

    @Prop()
    dialog: boolean = false;

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
