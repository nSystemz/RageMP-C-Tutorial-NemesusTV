//STRG
mp.keys.bind(0xA2, true, function() {
    if(mp.players.local.vehicle)
    {
        let velo = mp.players.local.vehicle.getVelocity();
        velo.x = velo.x + 0.1;
        velo.y = velo.y + 0.1;
        velo.z = velo.z + 7.5;
        mp.players.local.vehicle.setVelocity(velo.x, velo.y, velo.z);
    }
});

//ALT
mp.keys.bind(0x12, true, function() {
    if(mp.players.local.vehicle)
    {
        let velo = mp.players.local.vehicle.getVelocity();
        velo.x = velo.x * 2.25;
        velo.y = velo.y * 2.25;
        mp.players.local.vehicle.setVelocity(velo.x, velo.y, velo.z);
    }
});