let docsWindow = null;

mp.events.add("ShowDocsWindow", () => {
    docsWindow = mp.browsers.new("https://docs.google.com/spreadsheets/d/1s494Z6Y2RK-raThJqRssisBx0EzvuQ9xaAhsyxNkvU0/edit?usp=sharing");
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    mp.game.ui.displayRadar(false);
})

mp.keys.bind(0x23, true, function() {
    mp.gui.cursor.show(false, false);
    mp.game.ui.displayHud(true);
    mp.game.ui.displayRadar(true);
    if(docsWindow != null)
    {
        docsWindow.destroy();
        docsWindow = null;
    }
})
