<template>
    <div class="inventoryitem" style="background-color:#5c5b5b">
        <div class="inventorytext" v-if="item">
            <img :src="require(`../../assets/images/inventory/${item.discriptionitem}.png`)" width="18px" height="18px">
            <span class="ml-2" style="color:white;font-size:15px">{{item.discriptionitem}}{{item.amount}}</span>
            <i style="font-size: 14px" class="fas fas fa-play float-right mt-2 ml-2"></i>
            <i style="font-size: 14px" class="fas fa-trash-alt float-right mt-2 ml-2"></i>
           <i style="font-size: 14px" class="fas fa-hand-paper float-right mt-2 ml-2"></i>
        </div>
    </div>
</template>

<template>
  
</template>

<script>
export default {
    props: ['item', 'title', 'arrayitems'],
    methods: {
        getChildPayload: function() {
            return this.items;
        },
        useitem: function() {
            mp.trigger("InventarAktion", this.item.id, "konsumieren")
        },
        destroyitem: function() {
            mp.trigger("InventarAktion", this.item.id, "wegwerfen")
        },
    }
}
</script>

<style>

</style>