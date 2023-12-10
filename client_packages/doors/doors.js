const doors = [
    {id: 0, hash: -8873588, locked: true, position: new mp.Vector3(842.7685, -1024.539, 28.34478)}
]

doors.forEach((door) => {
    mp.game.object.doorControl(door.hash, door.position.x, door.position.y, door.position.z, door.locked, 0.0, 0.0, 0.0);
})

mp.keys.bind(0x45, true, () => {
    doors.forEach((door) => {
        if(mp.game.gameplay.getDistanceBetweenCoords(door.position.x, door.position.y, door.position.z, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, true) <= 2.0)
        {
            door.locked = !door.locked;
            mp.game.object.doorControl(door.hash, door.position.x, door.position.y, door.position.z, door.locked, 0.0, 0.0, 0.0);
        }
    })
})