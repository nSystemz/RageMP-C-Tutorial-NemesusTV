<template>
  <div class="money">
    <div id="moneyhud">
        {{money}}$
    </div>    
  </div>
</template>

<script>
export default {
  name: 'Money',
  data() {
      return {
          money: 0,
      }
  },
  methods: {
      updateMoney: function(money) {
          this.money = money;
      }
  },
}
</script>

<style scoped>
#moneyhud {
    margin-left: 1.5vw;
    margin-top: 0.5vw;
    font-size: 1.5vw;
    color: #247cc9;
    text-shadow: 2px 2px 4px #000000;
}
</style>
