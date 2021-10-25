let charHud;
let bodyCam = null;
let bodyCamStart;
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
        charHud.execute(`gui.charcreator.showCharcreator();`)
	setTimeout(function() {
          mp.gui.cursor.show(true, true);
	}, 500);
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

mp.events.add(
{
    "charcreator-camera": (flag) => {
        let camera = { Angle: 0, Dist: 1, Height: 0.2 };
        switch(flag)
        {
            case 0: //Torso
            {
                camera = { Angle: 0, Dist: 2.5, Height: 0.2 };
		break;
            }
            case 1: //Kopf
            {
                camera = { Angle: 0, Dist: 1, Height: 0.5 };
		break;
            }
            case 2: //Gesicht
            {
                camera = { Angle: 0, Dist: 0.5, Height: 0.7 };
		break;
            }
            case 3: //Körper/Brust
            {
                camera = { Angle: 0, Dist: 1, Height: 0.2 };
		break;
            }
        }
        bodyCamStart = player.position;
        const cameraPos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camera.Height), player.getRotation(2).z+90+camera.Angle, camera.Dist);
        bodyCam.setCoord(cameraPos.x, cameraPos.y, cameraPos.z);
        bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camera.Height);
    }
});

mp.events.add(
{
    "client:charcreator-preview": (x, data) => {
        data = JSON.parse(data);
        switch(x) {
            case 'hair': {
                mp.events.call('charcreator-camera', 1);
                player.setComponentVariation(2, parseInt(data[0]), 0, 0);
                player.setHairColor(parseInt(data[1]), parseInt(data[2]));
                break;
            }
	    case 'beard': {
		mp.events.call('charcreator-camera', 2);
            	break;
	    }
        }    
    }
});