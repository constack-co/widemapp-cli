<template>
  <v-app id="inspire" app>
    <div>
    <v-app-bar
      color="primary"
      dense
      dark
    >
      <v-app-bar-nav-icon @click="drawer.value = true"></v-app-bar-nav-icon>
  
      <v-toolbar-title>WIDEMAPP</v-toolbar-title>
      <v-row class="mt-0 ml-5">
        <v-col cols="3">
          <v-autocomplete
          small
          dense
          v-model="selectedProject"
          :items="getProjects"
          item-text="name"
          placeholder="Select a project"
          return-object
          ></v-autocomplete>
        </v-col>
      </v-row>

      <v-spacer></v-spacer>
    </v-app-bar>
  </div>
    <v-navigation-drawer 
    clipped
    absolute 
    temporary 
    v-model="drawer.value">
      <v-sheet class="pa-4">
        <v-avatar class="mb-4" color="grey darken-1" size="64"></v-avatar>

        <div>
          {{ viewModel.JwtClaims.FirstName + " " + viewModel.JwtClaims.LastName }}
        </div>
        <v-row>
          <v-col cols="6">
            <v-btn 
            @click="comingSoon()"
            class="mt-4 mr-4" small
              >Settings<v-icon small class="ml-2">mdi-cog</v-icon></v-btn
            >
          </v-col>

          <v-col cols="6">
            <v-btn @click="logout" color="red" class="mt-4" small>Logout <v-icon class="ml-2" small>mdi-logout</v-icon></v-btn>
          </v-col>
        </v-row>
      </v-sheet>

      <v-divider></v-divider>

      <v-list>
        <v-list-item
          v-for="[icon, text, route] in viewModel.links"
          :key="icon"
          link
          :to="{ name: route }"
          exact
        >
          <v-list-item-icon>
            <v-icon>{{ icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ text }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container class="py-8 px-6" fluid>
        <router-view />
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import RouteNameEnum from "@/common/enums/routeNameEnum";
import { JwtClaims, IJwtClaims } from "@/common/funcs/jwtClaims";
import LocalStorage from "@/common/funcs/localStorage";
import { Vue, Component, reactive, ref, Ref, Watch } from "@/importBase";
import snackBarMessage from "@Common/funcs/snackBarMessage";
import { GetProjectsService } from "@Services/projects/get-projects.service";

@Component
export default class AuthTemplate extends Vue {
  async mounted() {
    this.viewModel.JwtClaims = JwtClaims.claims!;
    await this.getAllProjects();
  }

  comingSoon() {
    snackBarMessage.Info('Available soon')
  }

  public viewModel = reactive({
    drawer: null,
    links: [
      ["mdi-view-dashboard", "Dashboard", RouteNameEnum.Dashboard],
      ["mdi-resistor-nodes", "Entities", RouteNameEnum.Entities],
      ["mdi-send-outline", "Plans", RouteNameEnum.Plans],
      ["mdi-semantic-web", "Templates", RouteNameEnum.Templates],
      ["mdi-folder-table", "Projects", RouteNameEnum.Projects],
    ],
    JwtClaims: {} as IJwtClaims,
  });

  public getProjectsService: GetProjectsService = new GetProjectsService();

  get getProjects() {
    return this.$store.getters.getProjects
  }

  @Watch('getProjects')
  async onProjectsChanged(value: boolean, oldValue: boolean) {}

  public async getAllProjects():Promise<void>{
    await this.getProjectsService.RequestAsync().then((data) => {
      this.$store.dispatch('setProjects', data)
    });
  }

  public drawer: Ref<boolean> = ref(false);

  get selectedProject() {
    this.$store.dispatch('setProjectId', localStorage.SELECTED_PROJECT ?? '')
    const projects = this.getProjects;

    return projects.find(x => x.id == localStorage.SELECTED_PROJECT);
  }

  set selectedProject(value: any) {
    this.$store.dispatch('setProjectId', value.id)
    localStorage.setItem('SELECTED_PROJECT', value.id)
  }

  public logout(){
    LocalStorage.removeToken();
    this.$router.push({name: RouteNameEnum.Login});
  }
}
</script>
