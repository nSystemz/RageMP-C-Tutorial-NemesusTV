<template>
  <div class="inventorylist" style="background-color:#4C4C4C">
      <div class="title" style="color:white">
          {{ title }}
      </div>
      <slot></slot>
      <button type="button" class="btn btn-dark mt-1 float-right" @click="sortitems">Items zusammenlegen</button>
  </div>
</template>

<script>

export default {
  props: ['title','arrayitems','showInventory'],
  data() {
    return {
    }
  },
  methods: {
    sortitems: function () {
      for(var x = 0; x<3; x++)
      {
        for(var i = 0; i<this.arrayitems.length; i++)
        {
          for(var j = 0; j<this.arrayitems.length; j++)
          {
            if(this.arrayitems[i] && this.arrayitems[i].descriptionitem == this.arrayitems[j].descriptionitem && this.arrayitems[j].amount > 0)
            {
              if(i != j)
              {
                var size = 0;
                if(this.arrayitems[i].number == 0)
                {
                  size = parseInt(this.arrayitems[i].amount) + parseInt(this.arrayitems[j].amount);
                }
                else
                {
                  size = (parseFloat(this.arrayitems[i].amount) + parseFloat(this.arrayitems[j].amount));
                }
                this.arrayitems[i].amount = size
                this.arrayitems[i].amountmax = size
                this.arrayitems[j].amount = 0
                this.arrayitems[j].amountmax = 0
                this.$delete(this.arrayitems, j)
              }
            }
          }
        }
      }
    }
  }
}
</script>

<style>
  body, html, #app {   min-height: 100%; background-color: transparent; background: transparent;}

  .inventorylist {
      padding: 10px 20px;
      margin: 0 10px;
      border: 1px solid #EEE;
      font-family: sans-serif;
      width: 350px;
  }
  .title {
      padding: 10px;
      font-size: 20px;
      font-weight: 600;
      margin-bottom: 10px;
      text-align: center;
      border-bottom: 1px solid #eee;
  }
</style>
