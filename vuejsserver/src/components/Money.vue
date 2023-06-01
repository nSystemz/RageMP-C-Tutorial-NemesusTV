<template>
  <div class="money">
    <div class="stats" v-if="statsshow">
      <ul class="list-group float-right" style="margin-right: 1vw;">
        <li class="list-group-item"><strong>{{stats.name}}</strong></li>
        <li class="list-group-item">Level {{stats.level}}</li>
        <li class="list-group-item">{{stats.money}}$</li>
        <li class="list-group-item">Payday in {{stats.payday}} Minuten</li>
      </ul>
    </div>
    <div id="moneyhud" v-if="moneyshow">
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
          moneyshow: false,
          statsshow: false,
          stats: []
      }
  },
  methods: {
      updateMoney: function(money) {
        this.money = money;
      },
      showHideMoney: function() {
        this.moneyshow = !this.moneyshow;
      },
      showStats: function(stats) {
        this.statsshow = !this.statsshow;
        if(this.statsshow)
        {
          this.stats = JSON.parse(stats);
        }
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
