<template>
  <div id="app">
    <Notifications ref="notification"/>
    <Inventory ref="inventory"/>
    <HUD ref="hud"/>
  </div>
</template>

<script>
import Notifications from './components/Notifications.vue'
import Inventory from './components/Inventory.vue'
import HUD from './components/HUD.vue'

export default {
  name: 'App',
  components: {
    Notifications,
    Inventory,
    HUD
  },
  mounted() {
    global.gui.notify = this.$refs.notification;
    global.gui.inventory = this.$refs.inventory;
    global.gui.hud = this.$refs.hud;
  }
}
</script>

<style>
</style>
