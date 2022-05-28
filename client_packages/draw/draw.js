let player = mp.players.local;
let street = undefined;
let zone = undefined;

mp.events.add('render', () => {
    mp.game.graphics.drawText('~b~Nemesus.de', [0.5, 0.005], {
        font: 0,
        color: [255, 255, 255, 255],
        scale: [0.5, 0.5],
        outline: true
    });

    street = mp.game.pathfind.getStreetNameAtCoord(player.position.x, player.position.y, player.position.z, 0, 0);
    zone = mp.game.gxt.get(mp.game.zone.getNameOfZone(player.position.x, player.position.y, player.position.z));
    mp.game.graphics.drawText(`~w~${mp.game.ui.getStreetNameFromHashKey(street.streetName)}\n~s~${zone}`, [0.205, 0.925], {
        font: 4,
        color: [244, 210, 66, 255],
        scale: [0.5, 0.5],
        outline: true
    });
});