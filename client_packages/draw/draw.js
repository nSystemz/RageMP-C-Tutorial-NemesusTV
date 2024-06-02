let player = mp.players.local;
let street = undefined;
let zone = undefined;
var TargetsToRender = [];
var scale = 0;

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

    for(var i = 0; i < TargetsToRender.length; i++)
    {
        RenderThings(TargetsToRender[i]);
    }
});

function CreateRenderTarget(name, model)
{
	if(!mp.game.ui.isNamedRendertargetRegistered(name))
		mp.game.ui.registerNamedRendertarget(name, false);
	if(!mp.game.ui.isNamedRendertargetLinked(mp.game.joaat(model)))
		mp.game.ui.linkNamedRendertarget(mp.game.joaat(model));
	if(mp.game.ui.isNamedRendertargetRegistered(name))
		return mp.game.ui.getNamedRendertargetRenderId(name);
	return -1;
}

function CreateModel(model, pos, rot)
{
	if(!mp.game.streaming.hasModelLoaded(mp.game.joaat(model)))
		mp.game.streaming.requestModel(mp.game.joaat(model));
	while(!mp.game.streaming.hasModelLoaded(mp.game.joaat(model)))
		mp.game.wait(0);
	return mp.objects.new(mp.game.joaat(model), pos,
	{
		rotation: rot,
		alpha: 255,
		dimension: mp.players.local.dimension
	});
}

mp.keys.bind(69, false, () => //Button E
{
	var pos = mp.players.local.position;
	pos.z += 1;
	
	scale = mp.game.graphics.requestScaleformMovie("cellphone_ifruit");
	while(!mp.game.graphics.hasScaleformMovieLoaded(scale))
		mp.game.wait(0);
	
	var x = CreateModel("xm_prop_x17dlc_monitor_wall_01a", pos, new mp.Vector3());
	pos.x += 10;
	var x = CreateModel("xm_prop_x17dlc_monitor_wall_01a", pos, new mp.Vector3());
	var id = CreateRenderTarget("prop_x17dlc_monitor_wall_01a", "xm_prop_x17dlc_monitor_wall_01a");
	if(id != -1)
		TargetsToRender.push(id);
	else
		mp.gui.chat.push("Could not create render target.");
});

function RenderThings(id)
{
	mp.game.ui.setTextRenderId(id);
	mp.game.graphics.set2dLayer(4); 
	
	mp.game.graphics.drawScaleformMovie(scale, 0.5, 0.5, 1, 1, 255, 255,255, 255, 0);

    let b = mp.browsers.newHeadless("https://www.youtube.com/", 1920, 1080);
    b.headlessTextureHeightScale = 100;
	
	mp.game.graphics.drawSprite(b.headlessTextureDict, b.headlessTextureName, 0.25, 0.5, 0.25, 0.25, 0, 255, 255, 255, 255);
	
	mp.game.ui.setTextRenderId(1);
}