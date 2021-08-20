<template>
<div class="inventory" v-if="showInventar">
    <InventoryList title="Inventar" :arrayitems="inventoryItems" style="max-height: 715px;overflow-y: scroll">
        <Container group-name="1" :get-child-payload="getChildPayload" @drop="onDrop('inventoryitems', $event)">
            <Draggable v-for="item in inventoryItems" :key="item.id">
                <InventoryItem :item="item" class="mt-2" title="Inventar" :arrayitems="inventoryItems">
                </InventoryItem>
            </Draggable>
        </Container>
    </InventoryList>
</div>
</template>

<script>
import { Draggable, Container} from 'vue-smooth-dnd'
import InventoryList from './utils/InventoryList'
import InventoryItem from './utils/InventoryItem.vue'

export default {
    data() {
        return {
            inventoryItems: [{}],
            showInventar: true,
        }
    },
    components: {
        Draggable,
        Container,
        InventoryList,
        InventoryItem
    },
    methods: {
        getChildPayload(index) {
            return this.inventoryItems[index]
        },
        hideInventory: function () {
            this.showInventar = false
        },
        showInventory: function (data) {
            this.inventoryItems = data
            this.showInventar = true
        },
        updateInventory: function() {
            mp.trigger("updateInventory", JSON.stringify(this.inventoryItems));
        }
    }
}
</script>

<style>
    .inventory {
        background-color: transparent !important;
        background: transparent !important;
        display: flex;
        justify-content: center;
        margin: 235px 0;
    }

    ::-webkit-scrollbar {
        width: 3px;
    }

    ::-webkit-scrollbar-track {
        box-shadow: inset 0 0 5px grey;
        border-radius: 10px;
    }

    ::-webkit-scrollbar-thumb {
        background: #eee;
        border-radius: 10px;
    }

</style>
