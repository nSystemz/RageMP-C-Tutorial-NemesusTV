mp.events.add('entityStreamIn', (entity) => {
    if(entity.type == 'player')
    {
        if(entity.hasVariable("Animations"))
        {
            const anim = entity.getVariable("Animations");
            if(anim)
            {
                const animData = anim.split('|');
                loadAnim(animData[0]);
                setTimeout(function() {
                    if(mp.players.exists(entity))
                    {
                        entity.clearTasksImmediately();
                        entity.taskPlayAnim(animData[0], animData[1], 1, 0, -1, parseInt(animData[2]), 1, 0, 0, 0);
                    }
                }, 150);
            }
        }
    }
});

function loadAnim(dict)
{
    if(!mp.game.streaming.hasAnimDictLoaded(dict))
    {
        mp.game.streaming.requestAnimDict(dict);
    }
}