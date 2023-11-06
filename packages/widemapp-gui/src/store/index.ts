import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    snackBar:{},
    projectId: '',
    projects: [] as any
  },
  mutations: {
    SET_SNACKBAR(state, snackBar){
      state.snackBar = snackBar;
    },
    SET_PROJECT(state, projectId){
      state.projectId = projectId;
    },
    SET_PROJECTS(state, projects){
      state.projects = projects;
    },
  },
  actions: {
    setSnackBar({commit}, snackBar){
      commit('SET_SNACKBAR', snackBar);
    },
    setProjectId({commit}, projectId){
      commit('SET_PROJECT', projectId);
    },
    setProjects({commit}, projects){
      commit('SET_PROJECTS', projects);
    },
  },
  getters: {
    getProjectId (state) {
      return state.projectId
    },
    getProjects (state) {
      return state.projects
    }
  },
  modules: {
  }
})
