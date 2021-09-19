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