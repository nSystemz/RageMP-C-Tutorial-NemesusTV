const Browser = require('./classes/browser');

let loginBrowser = new Browser('loginBrowser', 'package://web/login/login.html');

setTimeout(() => {
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    mp.gui.chat.show(false);
    mp.game.ui.displayRadar(false);
}, 250)

mp.events.add({
    "Auth.Register": (password) => {
        mp.gui.cursor.show(false, false);
        mp.gui.chat.show(true);
        mp.game.ui.displayHud(true);
        mp.game.ui.displayRadar(true);

        mp.events.callRemote('Auth.OnRegister', password);

        if(loginBrowser !== null)
        {
            loginBrowser.close();
            loginBrowser = null;
        }
    },

    "Auth.Login": (password) => {
        mp.gui.cursor.show(false, false);
        mp.gui.chat.show(true);
        mp.game.ui.displayHud(true);
        mp.game.ui.displayRadar(true);

        mp.events.callRemote('Auth.OnLogin', password);

        if(loginBrowser !== null)
        {
            loginBrowser.close();
            loginBrowser = null;
        }
    }
})