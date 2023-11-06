<template>
  <div>
    <v-app id="inspire">
      <v-main>
        <v-container class="fill-height" fluid>
          <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="4">
              <v-card class="elevation-12">
                <v-toolbar color="primary" dark flat>
                  <v-toolbar-title>Login</v-toolbar-title>
                  <v-spacer />
                </v-toolbar>
                <v-card-text>
                  <v-form @submit.prevent="loginForm" ref="loginForm">
                    <v-text-field
                      label="Email or username"
                      name="Email"
                      v-model="model.emailOrUsername"
                      prepend-icon="person"
                      type="text"
                      :rules="rules.simpleInputRule.value"
                    />

                    <v-text-field
                      id="password"
                      label="Password"
                      v-model="model.password"
                      prepend-icon="lock"
                      type="password"
                      :rules="rules.simpleInputRule.value"
                    />

                    <div class="col-md-12">
                      <div>
                        <div>
                          <a class="float-left mt-2" @click="pushToSignUp">Sign Up</a>
                          <span class="float-left mt-2 mx-2"> / </span>
                        </div>
                        
                        <a class="float-left mt-2" @click="pushToForgotPassword">Forgot Password</a>
                        <span class="float-left mt-2 mx-2"> / </span>
                        <a class="float-left mt-2" href="/documentation" target="_blank">Documentation</a>
                      </div>

                      <v-btn
                        type="submit"
                        class="float-right"
                        color="primary"
                        :loading="loginService.Loader.value"
                        >Login</v-btn
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
import { ILoginRequest, LoginService } from "@Services/account/login.service";
import { Vue, Component, reactive, ref } from "@/importBase";
import Rules from "@/common/funcs/rules";
import RouteNameEnum from "@/common/enums/routeNameEnum";

@Component
export default class Login extends Vue {
  public loginService: LoginService = new LoginService();

  public rules: Rules = new Rules();

  public model = reactive({} as ILoginRequest);

  public async loginForm(): Promise<void> {
    if ((this.$refs.loginForm as any).validate()) {
      await this.loginService.SendAsync(this.model).then(() => {
        this.$router.push({ name: RouteNameEnum.Dashboard });
      });
    }
  }

  public async pushToForgotPassword(): Promise<void> {
    this.$router.push({ name: RouteNameEnum.ForgotPassword });
  }

  public async pushToSignUp(): Promise<void> {
    this.$router.push({ name: RouteNameEnum.Register });
  }

  public async pushToDocs(): Promise<void> {
    this.$router.push({ name: RouteNameEnum.Documentation });
  }
}
</script>
