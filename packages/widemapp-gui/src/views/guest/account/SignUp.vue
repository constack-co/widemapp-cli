<template>
  <div>
    <v-app id="inspire">
      <v-main>
        <v-container class="fill-height" fluid>
          <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="6">
              <v-card class="elevation-12">
                <v-toolbar color="primary" dark flat>
                  <v-toolbar-title>Sign Up</v-toolbar-title>
                  <v-spacer />
                </v-toolbar>
                <v-form @submit.prevent="registerForm()" ref="step3">
                  <v-card-text>
                    <v-row>
                      <v-col cols="6">
                        <v-text-field
                          label="First name"
                          type="text"
                          v-model="model.firstName"
                          :rules="rules.simpleInputRule.value"
                        />
                      </v-col>
                      <v-col cols="6">
                        <v-text-field
                          label="Last name"
                          type="text"
                          v-model="model.lastName"
                          :rules="rules.simpleInputRule.value"
                        />
                      </v-col>
                      <v-col cols="6">
                        <v-text-field
                          v-model="model.username"
                          label="Username"
                          type="text"
                          :rules="rules.simpleInputRule.value"
                        />
                      </v-col>
                      <v-col cols="6">
                        <v-text-field
                          v-model="model.email"
                          label="Email"
                          type="text"
                          :rules="rules.simpleInputRule.value"
                        />
                      </v-col>
                      <v-col cols="6">
                        <v-text-field
                          :append-icon="
                            viewModel.showPassword ? 'mdi-eye' : 'mdi-eye-off'
                          "
                          :type="viewModel.showPassword ? 'text' : 'password'"
                          @click:append="
                            viewModel.showPassword = !viewModel.showPassword
                          "
                          v-model="model.password"
                          label="Password"
                          :rules="rules.passwordRule.value"
                          autocomplete="password"
                          @keyup="viewModel.confirmPassword = ''"
                          :counter="rules.simpleInputCounter.value"
                        />
                      </v-col>
                      <v-col cols="6">
                        <v-text-field
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
                  
                  </v-card-text>
                  <v-card-actions>
                    <a class="ml-2 text-body-2" @click="pushToLogin">Back to Login</a>
                    <v-spacer></v-spacer>
                    <div id="html_element"></div>
                    <v-btn
                        class="float-right"
                        color="primary"
                        :loading="addUserService.Loader.value"
                        type="submit"
                        >Sign Up</v-btn
                      >
                  </v-card-actions>
                  </v-form>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </v-app>
  </div>
</template>

<script lang="ts">
import { AddUserService, IAddUserRequest } from "@Services/users/AddUser";
import { Vue, Component, reactive, Watch, ref } from "@/importBase";
import Rules from "@/common/funcs/rules";
import RouteNameEnum from "@/common/enums/routeNameEnum";

@Component
export default class SignUp extends Vue {
  public addUserService: AddUserService = new AddUserService();
  public rules: Rules = new Rules();

  public model = reactive({} as IAddUserRequest);

  public viewModel = reactive({
    stepper: 1,
    confirmPassword: "",
    showPassword: false,
    showConfirmPassword: false,
  });

  mounted() {
    this.initReCaptcha();
  }

  initReCaptcha() {
      const _this = this;
      setTimeout(function () {
          if (typeof grecaptcha === 'undefined' || typeof grecaptcha.render ==='undefined') {
            _this.initReCaptcha();
          } else {
              (grecaptcha as any).render('html_element', {
                  sitekey: '6LeTl-MhAAAAAIrWyP9XvWmWJ-P76Ds8KkxM6xnW',
                  callback: _this.verifyCallback
              });
          }
      }.bind(this), 100);
  }

  verifyCallback (token) {
    this.model.token = token;
  }
  
  public async registerForm(): Promise<void> {
    if (!(this.$refs.step3 as any).validate()) this.viewModel.stepper = 3;
    
    else await this.addUserService.SendAsync(this.model, [{name: 'Recaptcha-Token', value: this.model.token}])
      .then(async () => await this.pushToLogin()).catch((error) => {
        (grecaptcha as any).reset();
      });
  }

  public async pushToLogin(): Promise<void> {
    this.$router.push({ name: RouteNameEnum.Login });
  }
}
</script>
