<template>
  <div>
    <v-container>
      <v-form ref="submitEditForm" @submit.prevent="submitForm">
        <v-sheet>
          <v-row>
            <v-col cols="12">
              <v-row justify="center">
                <v-col cols="3">
                  <v-text-field
                    label="Username"
                    v-model="editModel.value.userName"
                    :rules="rules.simpleInputRule.value"
                    :counter="rules.simpleInputCounter.value"
                  />
                </v-col>
                <v-col cols="3">
                  <v-text-field
                    label="Email"
                    v-model="editModel.value.email"
                    :rules="rules.emailRule.value"
                    :counter="rules.simpleInputCounter.value"
                  />
                </v-col>
              </v-row>
            </v-col>
            <v-col cols="12">
              <v-row justify="center">
                <v-col cols="3">
                  <v-text-field
                    label="First Name"
                    v-model="editModel.value.firstName"
                    :rules="rules.simpleInputRule.value"
                    :counter="rules.simpleInputCounter.value"
                  />
                </v-col>
                <v-col cols="3">
                  <v-text-field
                    label="Last Name"
                    v-model="editModel.value.lastName"
                    :rules="rules.simpleInputRule.value"
                    :counter="rules.simpleInputCounter.value"
                  />
                </v-col>
              </v-row>
            </v-col>
            <v-col cols="12">
              <v-container>
                <v-row>
                  <v-col cols="12" class="text-right">
                    <v-btn class="mx-1" @click="resetForm" outlined>
                      Reset
                    </v-btn>
                    <v-btn
                      :loading="editUserService.Loader.value"
                      type="submit"
                      class="mx-1"
                      :disabled="isFormUpdated"
                      color="green"
                      outlined
                    >
                      Submit
                    </v-btn>
                  </v-col>
                </v-row>
              </v-container>
            </v-col>
          </v-row>
        </v-sheet>
      </v-form>
    </v-container>
  </div>
</template>
<script lang="ts" src="./EditAccountInfoComponent">