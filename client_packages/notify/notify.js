let notifyHud = null;

mp.events.add('showNotification', (title, text, type) => {
	if(notifyHud == null)
	{
		notifyHud = mp.browsers.new("http://localhost:8080/#/gui");
		notifyHud.execute(`gui.notify.notification(${title}, ${text}, ${type});`);
	}
	else
	{
		notifyHud.execute(`gui.inv.notification(${title}, ${text}, ${type});`);
	}
});