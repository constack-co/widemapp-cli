<template>
  <div>
    <v-sheet>
      <v-tabs color="deep-purple accent-4" centered>
        <v-tab v-for="tab in this.viewModel.tabs" :key="tab.text">{{tab.text}}</v-tab>
        <v-tab-item v-for="(item,n) in this.viewModel.tabs" :key="n">
          <v-container>
            <v-row class="mt-4" justify="center">
              <v-col>
                <v-component
                  :key="n+1"
                  v-if="getUserByIdService.Response.value != undefined"
                  v-bind:UserData="getUserByIdService.Response"
                  :is="`Tab${n+1}`"
                ></v-component>
              </v-col>
            </v-row>
          </v-container>
        </v-tab-item>
      </v-tabs>
    </v-sheet>
  </div>
</template>
<script lang="ts">
import { Component } from "@/importBase";
import AccountComponentTs from "./AccountComponent";

@Component({
  components: {
    Tab1: () => import("./tabs/editAccountInfo/EditAccountInfoComponent.vue"),
    Tab2: () => import("./tabs/editAccountSecurity/EditAccountSecurityComponent.vue"),
    Tab3: () => import("./tabs/editAccountRole/EditAccountRoleComponent.vue")
  }
})
export default class AccountComponent extends AccountComponentTs {}
</script>