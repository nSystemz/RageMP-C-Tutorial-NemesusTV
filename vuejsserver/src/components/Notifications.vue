<template>
  <div class="notifications">
  </div>
</template>

<script>
import Vue from "vue";
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

Vue.use(Toast, {
  transition: "Vue-Toastification__bounce",
  maxToasts: 12,
  newestOnTop: true
});

export default {
  name: 'Notifications',
  methods: {
    showNotification: function(notificationtext,iconpic) {
      this.$toast.success(notificationtext, {
        position: "top-right",
        timeout: 5000,
        closeOnClick: true,
        pauseOnFocusLoss: true,
        pauseOnHover: true,
        draggable: true,
        draggablePercent: 0.6,
        showCloseButtonOnHover: false,
        hideProgressBar: false,
        closeButton: "button",
        icon: iconpic,
        rtl: false
      });
    }
  }
}
</script>

<style scoped>
</style>
