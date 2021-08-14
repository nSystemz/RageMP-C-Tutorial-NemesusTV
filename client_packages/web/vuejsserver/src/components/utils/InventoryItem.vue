<template>
    <div class="inventoryitem" style="background-color:#5c5b5b">
        <div class="inventorytext" v-if="item">
            <img :src="require(`../../assets/images/inventory/${item.descriptionitem}.png`)" width="18px" height="18px">
            <span class="ml-2" style="color:white;font-size:15px">{{item.descriptionitem}}[{{item.amount}}]</span>
            <i v-if="title == 'Inventar'" style="font-size: 14px" class="fas fas fa-play float-right mt-2 ml-2" @click="useitem()"></i>
            <i style="font-size: 14px;cursor:pointer" class="fas fa-divide float-right mt-2 ml-2"
                @click="showModal = true"></i>
            <i v-if="title == 'Inventar'" style="font-size: 14px;" class="fas fa-trash-alt float-right mt-2 ml-2" @click="destroyitem"></i>
            <i v-if="title == 'Inventar'" style="font-size: 14px" class="fas fa-hand-paper float-right mt-2 ml-2" @click="giveitem()"></i>
            <!-- Divide Modal -->
            <div v-if="showModal">
                <div class="modal-dialog" role="document" style="width: 250px">
                    <div class="modal-content" style="background-color:#4C4C4C">
                        <div class="modal-body" style="color:white">
                            <span class="alert-danger flex">{{errormessage}}</span>
                            <div class="flex mt-2">
                                <input v-if="item.number == 0" type="number" v-model="item.amountmax" min="1" step="1"
                                    style="width:75px;" />
                                <input v-else type="number" v-model="item.amountmax" min="0.1" step="0.1"
                                    style="width:75px;" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary"
                                @click="showModal = false">Abbrechen</button>
                            <button type="button" class="btn btn-dark" @click="split(item)">Aufsplitten</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: ['item', 'title', 'arrayitems'],
    methods: {
        getChildPayload: function () {
            return this.items;
        },
        giveitem: function () {
        },
        useitem: function () {
        },
        destroyitem: function () {
        },
    },
    data() {
        return {
            splitSize: 0,
            errormessage: '',
            showModal: false
        }
    }
}
</script>

<style>
    body, html, #app {   min-height: 100%; background-color: transparent; background: transparent; }

    .inventorytext{
        padding: 10px 20px;
        border: 1px solid #ddd;
        font-size: 18px;
    }
    .inventorytext:hover {
        cursor: move;
    }
    .flex {
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .fa-trash-alt:hover {
        color: #ddd;
    }
    .fa-divide:hover {
        color: #ddd;
    }
    .fa-play:hover {
        color: #ddd;
    }
</style>
