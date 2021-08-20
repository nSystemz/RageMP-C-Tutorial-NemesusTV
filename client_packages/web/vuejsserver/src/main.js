import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip = false

global.gui = {notify: null,inventory:null}

new Vue({
  render: h => h(App),
}).$mount('#app')
