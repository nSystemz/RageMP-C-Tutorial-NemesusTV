let notifyHud = null;
let cayoLoaded = false;

let pos;
let table;
let ball;
let lib = 'anim_casino_b@amb@casino@games@roulette@table';
let ped = null;

mp.game.ui.setRadarZoom(1100);

mp.gui.chat.show(false); //Disables default RageMP Chat
const chat = mp.browsers.new('package://advanced-chat/index.html');
chat.markAsChat();

mp.events.add('showHUD', () => 
{
    notifyHud = mp.browsers.new("http://localhost:8080/");
});

mp.events.add('showNotification', (text, iconpic) => 
{
    notifyHud.execute(`gui.notify.showNotification('${text}', '${iconpic}');`)
});

mp.events.add('showHideMoneyHud', () => 
{
    notifyHud.execute(`gui.money.showHideMoney();`)
});

mp.events.add('showStats', (stats) => {
    notifyHud.execute(`gui.money.showStats('${stats}');`)
})

mp.events.add('updatePB', (bar, wert) => 
{
    notifyHud.execute(`gui.hud.updateProgressbar('${bar}', '${wert}');`)
});

mp.events.add('showcrosshair', (crosshair) => {
    notifyHud.execute(`gui.hud.showCrosshair('${crosshair}')`)
});

mp.events.add('hidecrosshair', () => {
    notifyHud.execute(`gui.hud.hideCrosshair()`)
});

mp.events.add('Player:Snow', () => {
    mp.game.invoke("0x6E9EF3A33C8899F8", true); // Enable snow textures
    mp.game.invoke("0x7F06937B0CDCBC1A", 0.9);
});

mp.events.addDataHandler("Account:Geld", (entity, value, oldValue) => {
    if(notifyHud != null)
    {
        notifyHud.execute(`gui.money.updateMoney('${value}');`)
    }
});

mp.keys.bind(0x72, false, () => {
    cayoLoaded = !cayoLoaded;
    mp.game.invoke("0x9A9D1BA639675CF1", "HeistIsland", cayoLoaded);
    mp.game.invoke("0x5E1460624D194A38", cayoLoaded);

    mp.gui.chat.push(`Insel geladen/nicht geladen!`);
});

//Key F6
mp.keys.bind(0x75, false, () => {

    const maxSpeed = 10.0;
    const minHeight = 15.0;
    const maxHeight = 45.0;

    if(!mp.game.invoke("0x4E417C547182C84D", mp.players.local.vehicle.handle))
    {
        return;
    }
    if(mp.players.local.vehicle.getSpeed() > maxSpeed) {
        return;
    }
    const currentHeight = mp.players.local.vehicle.getHeightAboveGround();
    if(currentHeight < minHeight || currentHeight > maxHeight)
    {
        return;
    }
    const taskStatus = mp.players.local.getScriptTaskStatus(-275944640);
    if(taskStatus == 0 || taskStatus == 1)
    {
        return;
    }
    mp.players.local.clearTasks();
    mp.players.local.taskRappelFromHeli(10.0);
});

setInterval(() => {
    mp.game.invoke('0x9E4CFFF989258472');
    mp.game.invoke('0xF4F2C0D4EE209E20');
}, 25000)

mp.events.add("PlayMusic", (music) => {
   if(notifyHud != null)
   {
        notifyHud.execute(`gui.hud.playSound('${music}');`);
   }     
})

mp.game.streaming.requestAnimDict(lib);
mp.game.streaming.requestModel(mp.game.joaat('vw_prop_casino_roulette_01'));
mp.game.streaming.requestModel(mp.game.joaat('vw_prop_roulette_ball'));

mp.events.add('createRoulette', () => {
    pos = new mp.Vector3(-403.68314, 1118.8087, 325.86072-1);
    table = mp.objects.new(mp.game.joaat('vw_prop_casino_roulette_01'), pos);
    ball = mp.objects.new(mp.game.joaat('vw_prop_roulette_ball'), new mp.Vector3(pos.x-0.734742, pos.y-0.16617, pos.z+1.0715));
    ball.attachTo(table.handle, 0, 0, 0, 0, 0, 0, 0, true, true, false, false, 0, false);
});

mp.events.add('startRoulette', (number) => {
    mp.gui.chat.push('Roulette gestartet!');
    ball.position = new mp.Vector3(pos.x-0.734742, pos.y-0.16617, pos.z+1.0715);
    ball.rotation = new mp.Vector3(0, 0, 32.6);

    ball.playAnim('intro_ball', lib, 1000.0, false, true, true, 0, 136704);
    ball.playAnim('loop_ball', lib, 1000.0, false, true, false, 0, 136704);
 
    table.playAnim('intro_wheel', lib, 1000.0, false, true, true, 0, 136704);
    table.playAnim('loop_wheel', lib, 1000.0, false, true, false, 0, 136704);
    
    ball.playAnim('exit_'+number+'_ball', lib, 1000.0, false, true, false, 0, 136704);
    table.playAnim('exit_'+number+'_wheel', lib, 1000.0, false, true, false, 0, 136704);
});

mp.events.add('PetFollow', (ped) => {
    ped.freezePosition(false);
    ped.setCanBeDamaged(false);
    ped.setInvincible(true);
    ped.setHealth(100);
    ped.setOnlyDamagedByPlayer(true);
    ped.setFleeAttributes(0.0, false);
    ped.setProofs(false, false, false, false, false, false, false, false);
    ped.taskFollowToOffsetOf(mp.players.local.handle, 1.5, 1.5, 1.5, 1.5, -1, 10, true);
});

mp.events.add("playerCommand", (command) => {
    const args = command.split(/[ ]+/);
    const commandName = args[0];

    args.shift();

    if(commandName == "q")
    {
        mp.game.vehicle.createMissionTrain(24, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, true);
    }
});