var tuningWindow = null;
var showTuning = false;

mp.events.add("Client:ShowTuning", () => {
    let tuningCsv;
    let getTuningsync = mp.players.local.vehicle.getVariable('Vehicle:Tuning');
    let color = mp.players.local.vehicle.getVariable('Vehicle:Color');
    let componentNumbers = [];

    if (!getTuningsync || getTuningsync.length <= 10) {
        for (let i = 0; i <= 57; i++) {
            if (i <= 53 && i != 17 && i != 19 && i != 20 && i != 21 && i != 49 && i != 50 && i != 51 && i != 52) {
                componentNumbers[i] = mp.players.local.vehicle.getMod(i);
            } else {
                componentNumbers[i] = -1;
            }
        }
        componentNumbers[55] = mp.players.local.vehicle.getMod(55);
        if (componentNumbers[55] == -1) {
            componentNumbers[55] = 4;
        }
        componentNumbers[58] = mp.players.local.vehicle.getHandling("FINITIALDRIVEFORCE");
        componentNumbers[59] = mp.players.local.vehicle.getHandling("FDRIVEINERTIA");
        componentNumbers[60] = 1;
        componentNumbers[61] = mp.players.local.vehicle.getHandling("FBRAKEBIASFRONT");
        componentNumbers[62] = mp.players.local.vehicle.getHandling("FBRAKEFORCE");
        componentNumbers[63] = mp.players.local.vehicle.getHandling("FDRIVEBIASFRONT");
        componentNumbers[66] = color.split(',')[0];
        componentNumbers[67] = color.split(',')[1];
        componentNumbers[68] = color.split(',')[3];
        componentNumbers[69] = color.split(',')[2];
        tuningCsv = componentNumbers.join(",");
    } else {
        tuningCsv = getTuningsync;
    }

    if (showTuning == false) {
        tuningWindow = mp.browsers.new('package://nTuning/web/index.html');
        tuningWindow.execute(`tuning.showTuning('${tuningCsv, 5000}');`);
    } else {
        tuningWindow.execute(`tuning.showTuning('null','0');`);
        tuningWindow.destroy();
        tuningWindow = null;
    }
    showTuning = !showTuning;
    if (showTuning == true) {
        mp.gui.cursor.show(true, true);
        showHideChat(false);
    } else {
        mp.gui.cursor.show(false, false);
        showHideChat(true);
    }
});

mp.events.add("Client:HideTuningMenu", () => {
    tuningWindow.execute(`tuning.showTuning('null','0');`);
    tuningWindow.destroy();
    tuningWindow = null;
    showTuning = !showTuning;
    mp.gui.cursor.show(false, false);
    showHideChat(true);
});

mp.events.add("Client:TuningPreview", (tuning, component, pearlEffect) => {
    if (mp.players.local.vehicle) {
        if (isNaN(tuning) || isNaN(component)) return;
        if (tuning != 66 && tuning != 67 && tuning != 68 && tuning != 69 && tuning != 55 && tuning != 56 && tuning != 57 && tuning != 53 && tuning < 58) {
            if (tuning == 22) {
                if (component == -1) {
                    mp.players.local.vehicle.toggleMod(22, false);
                    mp.players.local.vehicle.setMod(parseInt(tuning), -1);
                    mp.game.invoke("0xE41033B25D003A07", mp.players.local.vehicle.handle, 255);
                } else {
                    mp.players.local.vehicle.toggleMod(22, true);
                    mp.players.local.vehicle.setMod(parseInt(tuning), 0);
                    mp.game.invoke("0xE41033B25D003A07", mp.players.local.vehicle.handle, parseInt(component));
                }
            } else {
                mp.players.local.vehicle.setMod(parseInt(tuning), parseInt(component));
            }
        } else if (tuning == 55) {
            mp.players.local.vehicle.setWindowTint(parseInt(component));
        } else if (tuning == 57) {
            if (component > -1) {
                mp.players.local.vehicle.toggleMod(20, true);
                if (component == 0) {
                    mp.players.local.vehicle.setTyreSmokeColor(230, 16, 34);
                } else if (component == 1) {
                    mp.players.local.vehicle.setTyreSmokeColor(2, 35, 250);
                } else if (component == 2) {
                    mp.players.local.vehicle.setTyreSmokeColor(44, 196, 10);
                } else if (component == 3) {
                    mp.players.local.vehicle.setTyreSmokeColor(245, 208, 0);
                } else if (component == 4) {
                    mp.players.local.vehicle.setTyreSmokeColor(128, 14, 124);
                } else if (component == 5) {
                    mp.players.local.vehicle.setTyreSmokeColor(240, 115, 48);
                } else if (component == 6) {
                    mp.players.local.vehicle.setTyreSmokeColor(14, 168, 207);
                } else if (component == 7) {
                    mp.players.local.vehicle.setTyreSmokeColor(209, 82, 207);
                } else if (component == 8) {
                    mp.players.local.vehicle.setTyreSmokeColor(95, 158, 160);
                } else if (component == 9) {
                    mp.players.local.vehicle.setTyreSmokeColor(255, 255, 255);
                } else if (component == 10) {
                    mp.players.local.vehicle.setTyreSmokeColor(1, 4, 10);
                }
            } else {
                mp.players.local.vehicle.toggleMod(20, false);
                mp.players.local.vehicle.setTyreSmokeColor(0, 0, 0);
            }
        } else if (tuning >= 58 && tuning <= 63) {
            if (tuning == 58) {
                mp.players.local.vehicle.setHandling("FINITIALDRIVEFORCE", component);
                setVehicleSpeed(mp.players.local.vehicle);
            } else if (tuning == 59) {
                mp.players.local.vehicle.setHandling("FDRIVEINERTIA", component);
            } else if (tuning == 60) {
                mp.events.callRemote('Server:TuningPreview', 60, component, pearlEffect, null);
                mp.players.local.vehicle.setEnginePowerMultiplier(component);
                mp.players.local.vehicle.setEngineTorqueMultiplier(component);
            } else if (tuning == 61) {
                mp.players.local.vehicle.setHandling("FBRAKEBIASFRONT", component);
            } else if (tuning == 62) {
                mp.players.local.vehicle.setHandling("FBRAKEFORCE", component);
            } else if (tuning == 63)
                mp.players.local.vehicle.setHandling("FDRIVEBIASFRONT", component);
        } else {
            mp.events.callRemote('Server:TuningPreview', parseInt(tuning), parseInt(component), pearlEffect, null);
        }
    }
});

mp.events.add("Client:GetDefaultChiptuning", () => {
    if (mp.players.local.vehicle) {
        var defaultHandling = [];
        defaultHandling[0] = mp.players.local.vehicle.getHandling("FINITIALDRIVEFORCE");
        defaultHandling[1] = mp.players.local.vehicle.getHandling("FDRIVEINERTIA");
        defaultHandling[2] = 1;
        defaultHandling[3] = mp.players.local.vehicle.getHandling("FBRAKEBIASFRONT");
        defaultHandling[4] = mp.players.local.vehicle.getHandling("FBRAKEFORCE");
        defaultHandling[5] = mp.players.local.vehicle.getHandling("FDRIVEBIASFRONT");
        tuningWindow.execute(`tuning.setDefaultChiptuning('${JSON.stringify(defaultHandling)}');`)
    }
});

mp.events.add("Client:GetMaxTuning", () => {
    if (mp.players.local.vehicle) {
        if (tuningWindow != null) {
            var maxtuning = [];
            for (let i = 0; i < 56; i++) {
                let tuningcomp = mp.players.local.vehicle.getNumMods(i);
                if (!tuningcomp || tuningcomp <= 0) {
                    maxtuning[i] = -1;
                } else {
                    maxtuning[i] = tuningcomp;
                    maxtuning[i] = maxtuning[i] - 1;
                }
            }
            maxtuning[22] = 11;
            maxtuning[56] = -1;
            maxtuning[57] = -1;
            maxtuning[53] = -1;
            maxtuning[40] = -1;
            maxtuning[55] = -1;
            maxtuning[50] = -1;
            maxtuning[51] = -1;
            maxtuning[52] = -1;
            maxtuning[17] = -1;
            maxtuning[19] = -1;
            maxtuning[20] = -1;
            maxtuning[21] = -1;
            maxtuning[64] = -1;
            maxtuning[65] = -1;
            if (mp.players.local.vehicle.getClass() != 8 && mp.players.local.vehicle.getClass() != 13 && mp.players.local.vehicle.getClass() != 14 && mp.players.local.vehicle.getClass() != 16 && mp.players.local.vehicle.getClass() != 16 && mp.players.local.vehicle.getClass() != 21) {
                maxtuning[56] = 10;
                maxtuning[57] = 10;
                maxtuning[40] = 0;
                maxtuning[53] = 5;
                maxtuning[55] = 5;
            }
            if (mp.players.local.vehicle.getClass() == 8) {
                maxtuning[57] = 10;
            }
            tuningWindow.execute(`tuning.setMaxTuning('${JSON.stringify(maxtuning)}');`)
        }
    }
});

function showHideChat(setChat) {
    mp.gui.chat.show(setChat);
}