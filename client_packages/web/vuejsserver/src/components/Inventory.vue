<template>
    <div class="inventory">
        <InventoryList title="Inventar" :arrayitems="inventoryitems"
            style="max-height: 715px;overflow-y: scroll">
                <Container group-name="1" :get-child-payload="getChildPayload1" @drop="onDrop('inventoryitems', $event, 50, countweight1(maxweight))">
                    <Draggable v-for="item in inventoryitems" :key="item.id">
                    <InventoryItem :item="item" class="mt-2" title="Inventar"
                        :arrayitems="inventoryitems">
                    </InventoryItem>
                </Draggable>
                </Container>
        </InventoryList>
        <InventoryList title="Kofferraum" :arrayitems="inventoryitems"
            style="max-height: 715px;overflow-y: scroll">
                <Container group-name="2" :get-child-payload="getChildPayload1" @drop="onDrop('inventoryitems', $event, 50, countweight1(maxweight))">
                    <Draggable v-for="item in inventoryitems" :key="item.id">
                    <InventoryItem :item="item" class="mt-2" title="Inventar"
                        :arrayitems="inventoryitems">
                    </InventoryItem>
                </Draggable>
                </Container>
        </InventoryList>
    </div>
</template>

<script>
import InventoryList from './utils/InventoryList'
import InventoryItem from './utils/InventoryItem'
import {
    Draggable,
    Container
} from 'vue-smooth-dnd'
import { applyDrag } from './utils/helpers.js'

export default {
    name: 'Inventory',
    data() {
        return {
            inventoryitems: [{}],
        }
    },
    methods: {
        getChildPayload1 (index) {
            return this.inventoryitems[index]
        },
    },
    components: {
        InventoryList,
        InventoryItem,
        Draggable,
        Container,
        applyDrag
    }
} 
</script>

<style>
    .inventory {
        background-color: transparent !important;
        background: transparent !important;
        display:flex;
        justify-content: center;
        margin: 235px 0;
    }
    .on-drag {
        background-color: #fff;
        color: #fff;
        z-index: 20;
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
