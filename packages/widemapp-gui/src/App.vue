<template>
  <div>
    <v-app>
      <router-view />
      <SnackBar></SnackBar>
    </v-app>
  </div>
</template>
<script lang="ts">
import { Vue, Component } from "@/importBase";
import { JwtClaims } from "./common/funcs/jwtClaims";
import LocalStorage from "./common/funcs/localStorage";

@Component({
  components: {
    SnackBar: () => import("@/common/components/SnackBarComponent.vue"),
  },
})
export default class App extends Vue {
  private async created(): Promise<void> {
    this.$vuetify.theme.dark = false;
  }
}
</script>
