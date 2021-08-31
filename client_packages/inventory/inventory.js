let invHud;

mp.events.add('showPlayerInventory', (data) =>
{
    if(invHud == null)
    {
        invHud = mp.browsers.new("http://localhost:8080/");
        invHud.execute(`gui.inventory.showInventory('${data}');`);
        mp.gui.cursor.show(true, true);
    }
    else
    {
        invHud.execute(`gui.inventory.showInventory('${data}');`);
        mp.gui.cursor.show(true, true);
    }
});

mp.events.add('hideInventory', () =>
{
    if(invHud != null)
    {
        invHud.execute(`gui.inventory.hideInventory();`);
        mp.gui.cursor.show(false, false);
    }
});

mp.events.add('InventarAktion', (itemid, action) => {
    mp.events.callRemote('InventarAktionServer', itemid, action);
});

mp.events.add('updateInventory', (json) => {
    mp.events.callRemote('updateInventoryServer', json);
})

