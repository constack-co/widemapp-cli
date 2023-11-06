<template>
  <div>
    <v-container>
      <v-form ref="submitSecurityForm" @submit.prevent="submitForm">
        <v-sheet>
          <v-row>
            <v-col cols="12">
              <v-row justify="center">
                <v-col cols="4">
                  <v-text-field
                    :append-icon="
                      viewModel.showCurrentPassword ? 'mdi-eye' : 'mdi-eye-off'
                    "
                    :type="viewModel.showCurrentPassword ? 'text' : 'password'"
                    @click:append="
                      viewModel.showCurrentPassword =
                        !viewModel.showCurrentPassword
                    "
                    label="Current Password"
                    v-model="passwordModel.value.currentPassword"
                    :rules="rules.passwordRule.value"
                    :counter="rules.simpleInputCounter.value"
                  />
                </v-col>
              </v-row>
            </v-col>
            <v-col cols="12">
              <v-row justify="center">
                <v-col cols="4">
                  <v-text-field
                    :append-icon="
                      viewModel.showNewPassword ? 'mdi-eye' : 'mdi-eye-off'
                    "
                    :type="viewModel.showNewPassword ? 'text' : 'password'"
                    @click:append="
                      viewModel.showNewPassword = !viewModel.showNewPassword
                    "
                    v-model="passwordModel.value.newPassword"
                    label="Password"
                    :rules="rules.passwordRule.value"
                    autocomplete="password"
                    @keyup="viewModel.confirmPassword = ''"
                    :counter="rules.simpleInputCounter.value"
                  />
                </v-col>
              </v-row>
            </v-col>
            <v-col cols="12">
              <v-row justify="center">
                <v-col cols="4">
                  <v-text-field
                    :rules="[
                      (value) =>
                        value === passwordModel.value.newPassword ||
                        'The password you entered don\'t match',
                    ]"
                    :append-icon="
                      viewModel.showConfirmPassword ? 'mdi-eye' : 'mdi-eye-off'
                    "
                    :type="viewModel.showConfirmPassword ? 'text' : 'password'"
                    @click:append="
                      viewModel.showConfirmPassword =
                        !viewModel.showConfirmPassword
                    "
                    v-model="viewModel.confirmPassword"
                    label="Confirm Password"
                    autocomplete="confirmPassword"
                  />
                </v-col>
              </v-row>
            </v-col>
            <v-col cols="12">
              <v-container>
                <v-row>
                  <v-col cols="12" class="text-right">
                    <v-btn
                      type="submit"
                      class="mx-1"
                      color="green"
                      outlined
                      :loading="editUserPasswordService.Loader.value"
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
<script lang="ts" src="./EditAccountSecurityComponent" />