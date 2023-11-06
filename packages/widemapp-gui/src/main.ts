import Vue from 'vue'
import App from './App.vue'
import router from '@/router/base/routerBase';
import store from './store'
import vuetify from './plugins/vuetify'
import VueMask from 'v-mask'
import VueCompositionApi from '@vue/composition-api';
import '@mdi/font/css/materialdesignicons.css'
import LocalStorage from './common/funcs/localStorage';
import { JwtClaims } from './common/funcs/jwtClaims';

Vue.use(VueCompositionApi);
Vue.config.productionTip = false
Vue.use(VueMask);

LocalStorage.initializeToken();
JwtClaims.initialize();

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
