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

mp.events.add({
    "charcreator-show": () => {
        charHud = mp.browsers.new("http://localhost:8080/");
        charHud.execute(`gui.charcreator.showCharcreator();`)
        setTimeout(function () {
            mp.gui.cursor.show(true, true);
        }, 500);
        mp.game.ui.displayHud(false);
        mp.gui.chat.show(false);
        mp.game.ui.displayRadar(false);
        bodyCamStart = player.position;
        let camValues = {
            Angle: player.getRotation(2).z + 90,
            Dist: 2.6,
            Height: 0.2
        };
        let pos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height), camValues.Angle, camValues.Dist);
        bodyCam = mp.cameras.new('default', pos, new mp.Vector3(0, 0, 0), 50);
        bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height);
        bodyCam.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 500, true, false);
        player.setComponentVariation(11, 15, 0, 0);
        player.setComponentVariation(3, 15, 0, 0);
        player.setComponentVariation(8, 15, 0, 0);
        player.setComponentVariation(6, 1, 0, 0);
    }
});

mp.events.add({
    "charcreator-hide": () => {
        
        if(charHud != null)
        {
            charHud.destroy();
            charHud = null;
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            mp.gui.chat.show(true);
            mp.game.ui.displayRadar(true);
        }
        if(bodyCam != null)
        {
            mp.game.cam.renderScriptCams(false, false, 500, true, false);
            bodyCam.setActive(false);
            bodyCam.destroy();
            bodyCam = null;
        }
    }
});

mp.events.add({
    "charcreator-camera": (flag) => {
        let camera = {
            Angle: 0,
            Dist: 1,
            Height: 0.2
        };
        switch (flag) {
            case 0: //Torso
            {
                camera = {
                    Angle: 0,
                    Dist: 2.5,
                    Height: 0.2
                };
                break;
            }
            case 1: //Kopf
            {
                camera = {
                    Angle: 0,
                    Dist: 1,
                    Height: 0.5
                };
                break;
            }
            case 2: //Gesicht
            {
                camera = {
                    Angle: 0,
                    Dist: 0.5,
                    Height: 0.7
                };
                break;
            }
            case 3: //Körper/Brust
            {
                camera = {
                    Angle: 0,
                    Dist: 1,
                    Height: 0.2
                };
                break;
            }
        }
        bodyCamStart = player.position;
        const cameraPos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camera.Height), player.getRotation(2).z + 90 + camera.Angle, camera.Dist);
        bodyCam.setCoord(cameraPos.x, cameraPos.y, cameraPos.z);
        bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camera.Height);
    }
});

mp.events.add({
    "client:charcreator-preview": (x, data) => {
        data = JSON.parse(data);
        switch (x) {
            case 'blendData': {
                mp.events.call('charcreater-camera', 2);
                player.setHeadBlendData(parseInt(data[0]), parseInt(data[1]), 0, parseInt(data[2]), parseInt(data[3]), 0, parseInt(data[4]), parseInt(data[5]), 0.0, true);
                break;
            }
            case 'eyeColor': {
                mp.events.call('charcreater-camera', 2);
                player.setEyeColor(parseInt(data[0]));
                break;
            }
            case 'hair': {
                mp.events.call('charcreator-camera', 1);
                player.setComponentVariation(2, parseInt(data[0]), 0, 0);
                player.setHairColor(parseInt(data[1]), parseInt(data[2]));
                break;
            }
            case 'beard': {
                mp.events.call('charcreator-camera', 2);
                player.setHeadOverlay(1, parseInt(data[0]), 1.0, parseInt(data[1]), parseInt(data[1]));
                break;
            }
            case 'faceFeatures': {
                mp.events.call('charcreator-camera', 2)
                player.setFaceFeature(0, parseFloat(data[0]));
                player.setFaceFeature(1, parseFloat(data[1]));
                player.setFaceFeature(2, parseFloat(data[2]));
                player.setFaceFeature(3, parseFloat(data[3]));
                player.setFaceFeature(4, parseFloat(data[4]));
                player.setFaceFeature(5, parseFloat(data[5]));
                player.setFaceFeature(6, parseFloat(data[6]));
                player.setFaceFeature(7, parseFloat(data[7]));
                player.setFaceFeature(8, parseFloat(data[8]));
                player.setFaceFeature(9, parseFloat(data[9]));
                player.setFaceFeature(10, parseFloat(data[10]));
                player.setFaceFeature(11, parseFloat(data[11]));
                player.setFaceFeature(12, parseFloat(data[12]));
                player.setFaceFeature(13, parseFloat(data[13]));
                player.setFaceFeature(14, parseFloat(data[14]));
                player.setFaceFeature(15, parseFloat(data[15]));
                player.setFaceFeature(16, parseFloat(data[16]));
                player.setFaceFeature(17, parseFloat(data[17]));
                player.setFaceFeature(18, parseFloat(data[18]));
                player.setFaceFeature(19, parseFloat(data[19]));
                break;
            }
            case 'clothing': {
                mp.events.call('charcreator-camera', 0);
                player.setComponentVariation(11, parseInt(data[0]), 0, 0);
                player.setComponentVariation(3, parseInt(data[1]), 0, 0);
                player.setComponentVariation(4, parseInt(data[2]), 0, 0);
                player.setComponentVariation(6, parseInt(data[3]), 0, 0);
                break;
            }
        }
    }
});

mp.events.add({
    "client:charcreator-preview2": (data, data2) => {
        data = JSON.parse(data);
        data2 = JSON.parse(data2);
        mp.events.call('charcreator-camera', 0);
        player.setHeadOverlay(0, parseInt(data[0]), 1.0, parseInt(data2[0]), parseInt(data2[0]));
        player.setHeadOverlay(2, parseInt(data[1]), 1.0, parseInt(data2[1]), parseInt(data2[1]));
        player.setHeadOverlay(3, parseInt(data[2]), 1.0, parseInt(data2[2]), parseInt(data2[2]));
        player.setHeadOverlay(4, parseInt(data[3]), 1.0, parseInt(data2[3]), parseInt(data2[3]));
        player.setHeadOverlay(5, parseInt(data[4]), 1.0, parseInt(data2[4]), parseInt(data2[4]));
        player.setHeadOverlay(6, parseInt(data[5]), 1.0, parseInt(data2[5]), parseInt(data2[5]));
        player.setHeadOverlay(7, parseInt(data[6]), 1.0, parseInt(data2[6]), parseInt(data2[6]));
        player.setHeadOverlay(8, parseInt(data[7]), 1.0, parseInt(data2[7]), parseInt(data2[7]));
        player.setHeadOverlay(9, parseInt(data[8]), 1.0, parseInt(data2[8]), parseInt(data2[8]));
        player.setHeadOverlay(10, parseInt(data[9]), 1.0, parseInt(data2[9]), parseInt(data2[9]));
        player.setHeadOverlay(11, parseInt(data[10]), 1.0, parseInt(data2[10]), parseInt(data2[10]));
        player.setHeadOverlay(12, parseInt(data[11]), 1.0, parseInt(data2[11]), parseInt(data2[11]));
    }
});

mp.events.add({
    "client:charcreator-resetcloths": () => {
        player.setComponentVariation(11, 15, 0, 0);
        player.setComponentVariation(3, 15, 0, 0);
        player.setComponentVariation(8, 15, 0, 0);
        player.setComponentVariation(6, 1, 0, 0);
    }
});

mp.events.add({
    "client:charcreator-setgender": (gender) => {
        mp.events.callRemote('SetPlayerGender', gender);
    }
});

mp.events.add({
    "client:charcreator-create": (character) => {
        mp.events.callRemote('CharacterCreated', character, true);
    }
});
