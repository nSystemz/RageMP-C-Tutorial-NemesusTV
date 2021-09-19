<template>
  <div class="hud">
    <div id="container">
        <progress-bar type="circle" ref="healthbar" color="#e03a3a" strokeWidth="0.5" duration="2000" :options="options1">
        </progress-bar>
    </div>
    <div id="container2">
        <progress-bar type="circle" ref="hungerbar" color="#a8450c" strokeWidth="0.5" duration="2000" :options="options2">
        </progress-bar>
    </div>
    <div id="container3">
        <progress-bar type="circle" ref="thirstbar" color="#0c6aa8" strokeWidth="0.5" duration="2000" :options="options3">
        </progress-bar>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import VueProgress from 'vue-progress';
Vue.use(VueProgress);

export default {
  name: 'HUD',
  data() {
      return {
          options1: {
              color: '#e03a3a',
              strokeWidth: 7.0,
              warnings: false,
              svgStyle: {
                  width: '100%',
                  height: '100%'
              },
              text: {
                  value: '&#10084;'
              }
          },
          options2: {
              color: '#a8450c',
              strokeWidth: 7.0,
              warnings: false,
              svgStyle: {
                  width: '100%',
                  height: '100%'
              },
              text: {
                  value: '&#127828;'
              }
          },
          options3: {
              color: '#0c6aa8',
              strokeWidth: 7.0,
              warnings: false,
              svgStyle: {
                  width: '100%',
                  height: '100%'
              },
              text: {
                  value: '&#127865;'
              }
          }
      }
  },
  methods: {
      updateProgressbar: function(bar, wert) {
          if(bar == 1)
          {
              this.$refs.healthbar.animate(wert);
          }
          if(bar == 2)
          {
              this.$refs.hungerbar.animate(wert);
          }
          if(bar == 3)
          {
              this.$refs.thirstbar.animate(wert);
          }
      }
  },
}
</script>

<style scoped>
#container {
    margin-top: 897px;
    margin-left: 310px;
    padding: 5px;
    width: 50px;
    height: 50px;
    clear: left;
}
#container2 {
    margin-left: 310px;
    padding: 5px;
    width: 50px;
    height: 50px;
    clear: left;
}
#container3 {
    margin-left: 310px;
    padding: 5px;
    width: 50px;
    height: 50px;
    clear: left;
}
</style>
