let lpWindow = null;

mp.events.add("showLockpicking", () => 
{
    mp.gui.cursor.show(true, true);
    lpWindow = mp.browsers.new("package://web/lockpicking/lockpicking.html");
})

mp.events.add("hideLockpicking", () => {
    mp.gui.cursor.show(false, false);

    if(lpWindow != null)
    {
        lpWindow.destroy();
        lpWindow = null;
    }
})

mp.events.add("successLockpicking", () => {
    mp.events.callRemote('successLockpickingServer');

    if(lpWindow != null)
    {
        lpWindow.destroy();
        lpWindow = null;
    }
})

mp.events.add("failedLockpicking", () => {
    mp.events.callRemote('failedLockpickingServer');
    
    if(lpWindow != null)
    {
        lpWindow.destroy();
        lpWindow = null;
    }
})