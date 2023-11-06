<template>
  <div class="text-center">
    <v-snackbar v-model="snackBar.show" :color="snackBar.color">
      {{ snackBar.text }}
      <template v-slot:action="{ attrs }">
        <v-btn
          color="white"
          text
          v-bind="attrs"
          v-on:click="snackBar.show = false"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
import { mapState } from 'vuex'
@Component({
    computed:{
        ...mapState(['snackBar'])
    },
})
export default class SnackBarComponent extends Vue{    
 
}
</script>