<template>
  <div id="app">
    <Notifications ref="notification"/>
  </div>
</template>

<script>
import Notifications from './components/Notifications.vue'

export default {
  name: 'App',
  components: {
    Notifications
  },
  mounted() {
    global.gui.notify = this.$refs.notification;
  }
}
</script>

<style>
</style>
