let notifyHud = null;

mp.events.add('showNotification', (text, iconpic) => {
    if(notifyHud == null)
    {
        notifyHud = mp.browsers.new("http://localhost:8080/");
        notifyHud.execute(`gui.notify.showNotification('${text}', '${iconpic}');`)
    }
    else
    {
        notifyHud.execute(`gui.notify.showNotification('${text}', '${iconpic}');`)
    }
});