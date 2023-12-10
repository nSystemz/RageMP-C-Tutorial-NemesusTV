<template>
  <div id="app">
    <TuningMenu ref="tuning"/>
  </div>
</template>

<script>
import TuningMenu from './components/Tuning_Menu.vue'

export default {
  name: 'App',
  components: {
    TuningMenu
  },
  mounted() {
    global.tuning = this.$refs.tuning;
  }
}
</script>

<style>
@import './assets/adminlte/adminlte.css';

html,
body,
* {
    -webkit-user-select: none;
    -khtml-user-select: none;
    -moz-user-select: none;
    -o-user-select: none;
    user-select: none;
    background-color: transparent;
}

::-webkit-scrollbar-thumb {
    background-color: #a9a9a9;
}

::-webkit-scrollbar-track {
    background-color: transparent;
}

::-webkit-scrollbar-corner {
    background-color: transparent;
}

::-webkit-scrollbar {
    width: 0;
}

::-webkit-scrollbar {
    width: .1rem;
    height: .1rem;
}
</style>
