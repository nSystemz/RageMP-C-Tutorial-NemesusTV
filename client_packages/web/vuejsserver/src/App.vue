<template>
  <div id="app">
    <Notifications ref="notification"/>
    <Inventory ref="inventory"/>
  </div>
</template>

<script>
import Notifications from './components/Notifications.vue'
import Inventory from './components/Inventory.vue'

export default {
  name: 'App',
  components: {
    Notifications,
    Inventory
  },
  mounted() {
    global.gui.notify = this.$refs.notification;
  }
}
</script>

<style>
</style>
