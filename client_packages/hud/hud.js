let notifyHud = null;

mp.events.add('showHUD', () => 
{
    notifyHud = mp.browsers.new("http://localhost:8080/");
});

mp.events.add('showNotification', (text, iconpic) => 
{
    notifyHud.execute(`gui.notify.showNotification('${text}', '${iconpic}');`)
});

mp.events.add('updatePB', (bar, wert) => 
{
    notifyHud.execute(`gui.hud.updateProgressbar('${bar}', '${wert}');`)
});

mp.events.addDataHandler("Account:Geld", (entity, value, oldValue) => {
    if(notifyHud != null)
    {
        notifyHud.execute(`gui.money.updateMoney('${value}');`)
    }
});

setInterval(() => {
    mp.game.invoke('0x9E4CFFF989258472');
    mp.game.invoke('0xF4F2C0D4EE209E20');
}, 25000)