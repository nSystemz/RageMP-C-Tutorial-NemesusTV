<template>
<div class="inventory" v-if="showInventar">
    <InventoryList title="Inventar" :arrayitems="inventoryItems" style="max-height: 715px;overflow-y: scroll">
        <Container group-name="1" :get-child-payload="getChildPayload" @drop="onDrop('inventoryItems', $event)">
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
import { applyDrag } from './utils/helpers.js'

export default {
    data() {
        return {
            inventoryItems: [],
            showInventar: false,
        }
    },
    components: {
        Draggable,
        Container,
        InventoryList,
        InventoryItem,
        // eslint-disable-next-line vue/no-unused-components
        applyDrag

    },
    methods: {
        onDrop(collection, dropResult) {
            this[collection] = applyDrag(this[collection], dropResult)
            this.updateInventory()
        },
        showInventory: function (data) {
            this.showInventar = true
            this.inventoryItems = JSON.parse(data)
        },
        getChildPayload(index) {
            return this.showInventar[index];
        },
        hideInventory: function () {
            this.showInventar = false
        },
        updateInventory: function() {
            // eslint-disable-next-line no-undef
            mp.trigger("updateInventory", JSON.stringify(this.inventoryItems))
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
