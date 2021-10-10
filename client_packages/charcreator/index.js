let charHud;
let bodyCam = null;
const player = mp.players.local;

getCameraOffset = (pos, angle, dist) => {
    angle = angle * 0.0174533;
    pos.y = pos.y + dist * Math.sin(angle);
    pos.x = pos.x + dist * Math.cos(angle);
    return pos;
}

mp.events.add(
{
    "charcreator-show" : () => {
        charHud = mp.browsers.new("http://localhost:8080/");
        charhud.execute(`gui.charcreator.showCharcreator();`)
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        mp.gui.chat.show(false);
        mp.game.ui.displayRadar(false);
        bodyCamStart = player.position;
        let camValues = { Angle: player.getRotation(2).z + 90, Dist: 2.6, Height: 0.2};
        let pos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z+camValues.Height), camValues.Angle, camValues.Dist);
        bodyCam = mp.cameras.new('default', pos, new mp.Vector3(0,0,0), 50);
        bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height);
        bodyCam.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 500, true, false);
        player.setComponentVariation(11, 15, 0, 0);
        player.setComponentVariation(3, 15, 0, 0);
        player.setComponentVariation(8, 15, 0, 0);
        player.setComponentVariation(6, 1, 0, 0);
    }
});