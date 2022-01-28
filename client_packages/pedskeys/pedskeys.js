//NPCs
//Tutorial PED
mp.peds.new
(
    mp.game.joaat("ig_benny"),
    new mp.Vector3(-420.6354, 1120.6459, 325.85843),
    102.72603,
    0
);

//Police Carspawner PED
mp.peds.new
(
    mp.game.joaat("s_m_y_cop_01"),
    new mp.Vector3(441.1015, -978.8775, 30.689598),
    177.06728,
    0
);


//Keys
//F Taste
mp.keys.bind(0x46, true, function() {
    if(mp.players.local.isTypingInTextChat) return;
    mp.events.callRemote('OnPlayerPressF');
});

//I Taste
mp.keys.bind(0x49, true, function() {
    if(mp.players.local.isTypingInTextChat) return;
    mp.events.callRemote('OnPlayerPressI');
});

//F5 Taste
mp.keys.bind(0x74, true, function() {
    if(mp.players.local.isTypingInTextChat) return;
    mp.events.callRemote('OnPlayerPressF5');
});