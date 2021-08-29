<template>
    <div class="inventoryitem" style="background-color:#5c5b5b">
        <div class="inventorytext" v-if="item.descriptionitem">
            <img :src="require(`../../assets/images/inventory/${item.descriptionitem}.png`)" width="18px" height="18px" class="ml-2">
            <span class="ml-2" style="color:white;font-size:15px">{{item.descriptionitem}} [{{item.amount}}]</span>
            <i style="font-size: 14px" class="fas fas fa-play float-right mt-2 ml-2 mr-2" @click="useitem()"></i>
            <i style="font-size: 14px" class="fas fa-trash-alt float-right mt-2 ml-2" @click="destroyitem()"></i>
           <i style="font-size: 14px" class="fas fa-hand-paper float-right mt-2 ml-2"></i>
        </div>
    </div>
</template>

<script>
export default {
    props: ['item', 'title', 'arrayitems'],
    methods: {
        getChildPayload: function() {
            return this.items;
        },
        useitem: function() {
            // eslint-disable-next-line no-undef
            mp.trigger("InventarAktion", this.item.id, "konsumieren")
        },
        destroyitem: function() {
            // eslint-disable-next-line no-undef
            mp.trigger("InventarAktion", this.item.id, "wegwerfen")
        },
    }
}
</script>

<style>

</style>