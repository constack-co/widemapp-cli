<template>
  <div>
    <v-container>
      <v-form ref="submitEditForm" @submit.prevent="submitForm">
        <v-sheet>
          <v-row>
            <v-col cols="12">
              <v-row justify="center">
                <v-col cols="5">
                  <v-select
                    v-model="model.roleId"
                    :items="getRolesService.Response.value"
                    label="User Role"
                    dense
                    outlined
                    item-text="name"
                    item-value="id"
                  ></v-select>
                </v-col>
              </v-row>
            </v-col>
          </v-row>
          <v-col cols="12">
              <v-container>
                <v-row>
                  <v-col cols="12" class="text-right">
                    <v-btn
                      :disabled="isDisabled"
                      type="submit"
                      class="mx-1"
                      color="green"
                      outlined
                      :loading="getRolesService.Loader.value"
                    >
                      Submit
                    </v-btn>
                  </v-col>
                </v-row>
              </v-container>
            </v-col>
        </v-sheet>
      </v-form>
    </v-container>
  </div>
</template>
<script lang="ts" src="./EditAccountRoleComponent" />