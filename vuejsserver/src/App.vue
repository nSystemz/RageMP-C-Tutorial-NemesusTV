<template>
  <div id="app">
    <Money ref="money"/>
    <Charcreator ref="charcreator"/>
    <Notifications ref="notification"/>
    <Inventory ref="inventory"/>
    <HUD ref="hud"/>
  </div>
</template>

<script>
import Money from './components/Money.vue'
import Notifications from './components/Notifications.vue'
import Inventory from './components/Inventory.vue'
import HUD from './components/HUD.vue'
import Charcreator from './components/Charcreator.vue'

export default {
  name: 'App',
  components: {
    Money,
    Notifications,
    Inventory,
    HUD,
    Charcreator
  },
  mounted() {
    global.gui.money = this.$refs.money;
    global.gui.notify = this.$refs.notification;
    global.gui.inventory = this.$refs.inventory;
    global.gui.hud = this.$refs.hud;
    global.gui.charcreator = this.$refs.charcreator;
  }
}
</script>

<style>
</style>
