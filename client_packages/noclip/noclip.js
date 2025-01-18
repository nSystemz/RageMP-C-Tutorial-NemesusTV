let noclip = false;
let noclipSpeed = 1.0;
let localPlayer = mp.players.local;

//Keys
//F2
mp.keys.bind(0x73, true, function() {
    toggleNoclip();
});
//+
mp.keys.bind(0xBB, true, function() {
    noclipSpeed += 1;
});
//-
mp.keys.bind(0xBD, true, function() {
    noclipSpeed = Math.max(1, noclipSpeed-1);
});

function toggleNoclip()
{
    noclip = !noclip;

    if(noclip)
    {
        localPlayer.freezePosition(true);
        localPlayer.setInvincible(true);
        localPlayer.setVisible(false, false);
    }
    else
    {
        localPlayer.freezePosition(false);
        localPlayer.setInvincible(false);
        localPlayer.setVisible(true, false);
    }
}

mp.events.add('render', () => {
    if(!noclip) return;

    let rot = mp.game.cam.getGameplayCamRot(2);
    let dirForward = getDirectionVector(rot, 'forward');
    let dirRight = getDirectionVector(rot, 'right');

    let newPos = localPlayer.position;

    if(mp.game.controls.isDisabledControlPressed(0, 32)) newPos = setSpeedToVector(newPos, dirForward, noclipSpeed);
    if(mp.game.controls.isDisabledControlPressed(0, 33)) newPos = setSpeedToVector(newPos, dirForward, -noclipSpeed);
    if(mp.game.controls.isDisabledControlPressed(0, 34)) newPos = setSpeedToVector(newPos, dirRight, -noclipSpeed, true);
    if(mp.game.controls.isDisabledControlPressed(0, 35)) newPos = setSpeedToVector(newPos, dirRight, noclipSpeed, true);

    let z = 0;
    if(mp.game.controls.isDisabledControlPressed(0, 22)) z += noclipSpeed;
    if(mp.game.controls.isDisabledControlPressed(0, 36)) z -= noclipSpeed;

    const endPosition = new mp.Vector3(newPos.x, newPos.y, newPos.z + z);

    if(!isVectorEqual(endPosition, localPlayer.position))
    {
        localPlayer.setCoordsNoOffset(endPosition.x, endPosition.y, endPosition.z, true, true, true);
    }
});


function setSpeedToVector(vector1, vector2, speed, lr = false) {
    return new mp.Vector3(
        vector1.x + vector2.x * speed,
        vector1.y + vector2.y * speed,
        lr ? vector1.z : vector1.z + vector2.z * speed
    );
}

function getDirectionVector(camRot, direction) {
    const rotInRad = {
        x: camRot.x * (Math.PI / 180),
        y: camRot.y * (Math.PI / 180),
        z: camRot.z * (Math.PI / 180) + (direction === 'forward' ? Math.PI / 2 : 0),
    };

    return {
        x: Math.cos(rotInRad.z),
        y: Math.sin(rotInRad.z),
        z: Math.sin(rotInRad.x),
    };
}

function isVectorEqual(vector1, vector2) {
    return vector1.x === vector2.x && vector1.y === vector2.y && vector1.z === vector2.z;
}
