<template>
    <div>
    <v-app id="inspire">
      <v-main>
        <v-container class="fill-height" fluid>
          <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="4">
              <v-card class="elevation-12">
                <v-toolbar color="primary" dark flat>
                  <v-toolbar-title>Forgot Password</v-toolbar-title>
                  <v-spacer />
                </v-toolbar>
                <v-card-text>
                  <v-form @submit.prevent="forgotPassword" ref="forgotPassword">
                    <v-text-field
                      label="Email"
                      name="Email"
                      v-model="model.email"
                      prepend-icon="person"
                      type="text"
                      :rules="rules.simpleInputRule.value"
                    />

                    <div class="col-md-12">
                      <v-row>
                        <v-col>
                          <a class="float-left mt-2" @click="pushToLogin">Login</a>
                        </v-col>
                        <v-col>
                          <div id="html_element" class="float-right"></div>
                        </v-col>
                        <v-col>
                          <v-btn
                            type="submit"
                            class="float-right"
                            color="primary"
                            :loading="forgotPasswordService.Loader.value"
                            >SEND</v-btn
                          >
                        </v-col>
                      </v-row>
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
import {ForgotPasswordService, IForgotPasswordRequest} from "@Services/account/forgot-password.service";
import {Vue, Component, reactive, ref} from "@/importBase";
import Rules from '@/common/funcs/rules';
import RouteNameEnum from "@/common/enums/routeNameEnum";

@Component
export default class ForgotPassword extends Vue {

    mounted() {
      this.initReCaptcha();
    }

    public forgotPasswordService: ForgotPasswordService = new ForgotPasswordService();

    public rules:Rules = new Rules();

    public model = reactive({} as IForgotPasswordRequest);

    public submitLoading = ref(false);

    public async forgotPassword():Promise<void>{
        if ((this.$refs.forgotPassword as any).validate()) {
          await this.forgotPasswordService.SendAsync(this.model, [{name: 'Recaptcha-Token', value: this.model.token}])
          .then(async () => await this.pushToLogin()).catch((error) => {
            (grecaptcha as any).reset();
          });
        }
    }

    public async pushToLogin():Promise<void>{
      this.$router.push({name: RouteNameEnum.Login });
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

    
}
</script>