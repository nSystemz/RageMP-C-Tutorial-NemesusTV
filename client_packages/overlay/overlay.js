let overLayModus = false;
let overlayList = [];
let batch;

mp.events.add("playerCommand", (command) => {
    const args = command.split(/[ ]+/);
    const commandName = args[0];

    if(commandName === "overlaymodus")
    {
        overLayModus = !overLayModus;
        mp.game.graphics.setEntityOverlayPassEnabled(overLayModus);
        if(overLayModus)
        {
            let overlayParams = {
                enableDepth: false,
                deleteWhenUnused: false,
                keepNonBlurred: true,
                processAttachments: true,
                fill: { enable: false, color: 0xFFFFFFFF },
                noise: { enable: false, size: 0.0, speed: 0.0, intensity: 0.0 },
                outline: { enable: true, color: 0xFF9000FF, width: 1.0, blurRadius: 1.0, blurIntensity: 1.0 },
                wireframe: { enable: false }
            };

            batch = mp.game.graphics.createEntityOverlayBatch(overlayParams);
        }
    }
});

mp.events.add('entityStreamIn', (entity) => {
    if(overLayModus)
    {
        if(mp.vehicles.exists(entity) && 0 !== entity.handle && entity.type == 'vehicle')
        {
            overlayList.push(entity);
        }
    }
});

mp.events.add('entityStreamOut', (entity) => {
    if(overLayModus && entity.type == 'vehicle')
    {
        overlayList = overlayList.filter(function (element) {
            element != entity;
        });
    }
});

mp.events.add('render', () => {
    if(overLayModus)
    {
        overlayList.forEach(function (entity) {
            if(entity != null)
            {
                batch.addThisFrame(entity);
            }
        });
    }
});