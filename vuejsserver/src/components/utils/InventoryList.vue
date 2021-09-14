<template>
    <div class="inventorylist" style="background-color:#4C4C4C">
        <div class = "title" style="color:white">
            {{title}}
        </div>
        <slot></slot>
        <button class="btn btn-dark mt-1 float-right" @click="sortitems">Items zusammenlegen</button>
    </div>
</template>

<script>
export default {
    props: ['title','arrayitems','showInventory'],
    methods: {
        sortitems: function () {

        }
    }
}
</script>

<style>
    body, html { min-height: 100%; background-color: transparent; background:transparent;}

    .inventorylist {
        padding: 10px 20px;
        margin: 0 10px;
        border: 1px solid #eee;
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
