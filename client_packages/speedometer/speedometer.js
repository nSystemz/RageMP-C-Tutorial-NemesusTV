function getSpeedColor(kmh) {
    if(kmh < 65)
        return "~g~"
    if(kmh >= 65 && kmh < 125)
        return "~y~"
    if(kmh > 125)
        return "~r~"
}

function getKmh() {
    const player = mp.players.local;
    if(player.isSittingInAnyVehicle(true)) {
        let vehicle = mp.players.local.vehicle;
        let speed = vehicle.getSpeed()*3.6;
        speed = Math.round(speed)
        mp.game.graphics.drawText(`${getSpeedColor(speed)}${speed} KMH`, [0.45, 0.91], {
            font: 2,
            color: [255, 255, 255, 255],
            scale: [1.5, 1.5],
            outline: true
        });
    }
}

mp.events.add('render', () => {
    getKmh();
});