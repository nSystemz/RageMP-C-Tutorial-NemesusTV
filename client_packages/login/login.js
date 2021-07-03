let loginWindow = mp.browsers.new("package://web/login/login.html");

setTimeout(() => {
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    mp.gui.chat.show(false);
    mp.game.ui.displayRadar(false);
}, 150)

mp.events.add("Auth.Register", (password) => 
{
    mp.gui.cursor.show(false, false);
    mp.gui.chat.show(true);
    mp.game.ui.displayHud(true);
    mp.game.ui.displayRadar(true);

    mp.events.callRemote('Auth.OnRegister', password);

    if(loginWindow != null)
    {
        loginWindow.destroy();
        loginWindow = null;
    }
})

mp.events.add("Auth.Login", (password) => {
    mp.gui.cursor.show(false, false);
    mp.gui.chat.show(true);
    mp.game.ui.displayHud(true);
    mp.game.ui.displayRadar(true);

    mp.events.callRemote('Auth.OnLogin', password);

    if(loginWindow != null)
    {
        loginWindow.destroy();
        loginWindow = null;
    }
})