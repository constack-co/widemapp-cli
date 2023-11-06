<template>
  <div>
    <v-app id="inspire">
      <v-main>
        <v-container class="fill-height" fluid>
          <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="4">
              <v-card class="elevation-12">
                <v-toolbar color="primary" dark flat>
                  <v-toolbar-title>Reset Password</v-toolbar-title>
                  <v-spacer />
                </v-toolbar>
                <v-card-text>
                  <v-form @submit.prevent="resetPassword" ref="resetPassword">
                    <v-row>
                      <v-col cols="12">
                        <v-text-field
                          readonly
                          label="Email"
                          name="Email"
                          v-model="model.email"
                          prepend-icon="person"
                          type="text"
                          :rules="rules.simpleInputRule.value"
                        />
                      </v-col>
                      <v-col cols="12">
                        <v-text-field
                          prepend-icon="lock"
                          :append-icon="
                            viewModel.showPassword ? 'mdi-eye' : 'mdi-eye-off'
                          "
                          :type="viewModel.showPassword ? 'text' : 'password'"
                          @click:append="viewModel.showPassword = !viewModel.showPassword"
                          v-model="model.password"
                          label="Password"
                          :rules="rules.passwordRule.value"
                          autocomplete="password"
                          @keyup="viewModel.confirmPassword = ''"
                          :counter="rules.simpleInputCounter.value"
                        />
                      </v-col>
                      <v-col cols="12">
                        <v-text-field
                        prepend-icon="lock"
                          :rules="[
                            (value) =>
                              value === model.password ||
                              'The password you entered don\'t match',
                          ]"
                          :append-icon="
                            viewModel.showConfirmPassword ? 'mdi-eye' : 'mdi-eye-off'
                          "
                          :type="viewModel.showConfirmPassword ? 'text' : 'password'"
                          @click:append="
                            viewModel.showConfirmPassword = !viewModel.showConfirmPassword
                          "
                          v-model="viewModel.confirmPassword"
                          label="Confirm Password"
                          autocomplete="confirmPassword"
                        />
                      </v-col>
                    </v-row>

                    <div class="col-md-12">
                      <a class="float-left mt-2" @click="pushToLogin">Login</a>
                      <v-btn
                        type="submit"
                        class="float-right"
                        color="primary"
                        :loading="resetPasswordService.Loader.value"
                        >Submit</v-btn
                      >
                    </div>
                  </v-form>
                </v-card-text>
                <v-card-actions>
                  <v-spacer />
                </v-card-actions>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </v-app>
  </div>
</template>
<script lang="ts">
import {
  ResetPasswordService,
  IResetPasswordRequest,
} from "@Services/account/reset-password.service";
import { Vue, Component, reactive, ref } from "@/importBase";
import Rules from "@/common/funcs/rules";
import RouteNameEnum from "@/common/enums/routeNameEnum";

@Component
export default class ResetPassword extends Vue {
  public resetPasswordService: ResetPasswordService = new ResetPasswordService();

  public rules: Rules = new Rules();

  public viewModel = reactive({
    stepper: 1,
    confirmPassword: "",
    showPassword: false,
    showConfirmPassword: false,
  });

  public model = reactive({
    email: "",
  } as IResetPasswordRequest);

  public submitLoading = ref(false);

  public async resetPassword(): Promise<void> {
    if ((this.$refs.resetPassword as any).validate()) {
      await this.resetPasswordService
        .SendAsync(this.model)
        .then(() => this.pushToLogin());
    }
  }

  mounted() {
    const emailRegex = new RegExp(
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@(([[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );

    if (
      this.$route.query.token == null ||
      !emailRegex.test(atob(this.$route.query["email"] as string))
    )
      this.pushToLogin();

    this.model.token = this.$route.query.token as string;
    this.model.email = atob(this.$route.query["email"] as string);
  }

  public async pushToLogin(): Promise<void> {
    this.$router.push({ name: RouteNameEnum.Login });
  }
}
</script>
