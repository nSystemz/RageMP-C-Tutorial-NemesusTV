<template>
  <div class="hud" style="overflow-y: auto;background-color:transparent; height:100%; width:100%">
    <div style="height: 100%; background-color: transparent;">
      <div class="row justify-content-center tuningCentering">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Tuning <small>by Nemesus.de</small>
                    <i v-if="pearlEffect && selectedTuning == 'Farbe 1'" @click="setPerlmutt()"
                      class="fa-solid fa-sun float-right"
                      style="color:#3F6791; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                    <i v-if="!pearlEffect && selectedTuning == 'Farbe 1'" @click="setPerlmutt()"
                      class="fa-solid fa-sun float-right"
                      style="color:#FFFFFF; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                    <i v-if="pearlEffect && componentNumbers[69] != 255 && selectedTuning == 'Farbe 1'"
                      @click="resetPerlmutt()" class="fa-solid fa-ban float-right mr-2"
                      style="color:#FFFFFF; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                  </div>
                  <div class="card-body" style="max-height:65vh; width: 27.5vw; overflow-x: auto">
                    <h5>Tuningkomponente auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/tuning/1.png"
                            :class="[(selectedTuning == 'Farbe 1') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Farbe 1')">
                          <img src="../assets/images/tuning/1.png"
                            :class="[(selectedTuning == 'Farbe 2') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Farbe 2')">
                          <img src="../assets/images/tuning/5.png"
                            :class="[(selectedTuning == 'Farbe 3') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Farbe 3')">
                          <img src="../assets/images/tuning/2.png"
                            :class="[(selectedTuning == 'Tuning 1') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Tuning 1')">
                          <img src="../assets/images/tuning/3.png"
                            :class="[(selectedTuning == 'Tuning 2') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Tuning 2')">
                          <img src="../assets/images/tuning/4.png"
                            :class="[(selectedTuning == 'Tuning 3') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Tuning 3')">
                          <img src="../assets/images/tuning/6.png"
                            :class="[(selectedTuning == 'Tuning 4') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Tuning 4')">
                          <img src="../assets/images/tuning/7.png"
                            :class="[(selectedTuning == 'Analyse') ? 'tuningIconActive mr-2':'tuningIcon mr-2']"
                            @click="selectTuning('Analyse')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedTuning">
                    <div class="tuning"
                      v-if="selectedTuning == 'Farbe 1' || selectedTuning == 'Farbe 2' || selectedTuning == 'Farbe 3'">
                      <span class="badge badge-primary mb-3 ml-1">8 Tuningteile</span>
                      <ul class="vehicleColorsList" v-if="selectedTuning == 'Farbe 1'">
                        <li v-for="n in 159" :key="n" :style="{ background: vehicleColors[n].Hex }"
                          @click="tuningPreview(66, n)">
                        </li>
                      </ul>
                      <ul class="vehicleColorsList" v-if="selectedTuning == 'Farbe 2'">
                        <li v-for="n in 159" :key="n" :style="{ background: vehicleColors[n].Hex }"
                          @click="tuningPreview(67, n)">
                        </li>
                      </ul>
                      <ul class="vehicleColorsList" v-if="selectedTuning == 'Farbe 3'">
                        <li v-for="n in 159" :key="n" :style="{ background: vehicleColors[n].Hex }"
                          @click="tuningPreview(68, n)">
                        </li>
                      </ul>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 1'">
                      <div v-if="checkTuning(1) == 1">
                        <div v-for="(component, index) in tuningComponents" :key="index">
                          <div v-if="component != 'n/A' && index <= 10">
                            <span v-if="maxTuning[index] > -1">{{component}} <span
                                class="badge badge-secondary">{{componentNumbers[index]}}</span><span
                                class="badge badge-primary ml-1 mr-1">8 Tuningteile</span></span>
                            <vue-range-slider v-if="maxTuning[index] > -1" ref="slider" tooltip="false" dotSize="14"
                              height="13" :min="-1" :max="maxTuning[index]" value='0' :step="1"
                              v-model="componentNumbers[index]"
                              v-oninput="tuningPreview(index, componentNumbers[index])" />
                          </div>
                        </div>
                      </div>
                      <div v-else>
                        <span style="display: flex; justify-content: center; align-items: center;">Keine kompatiblen
                          Tuningteile vorhanden!</span>
                      </div>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 2'">
                      <div v-if="checkTuning(2) == 1">
                        <div v-for="(component, index2) in tuningComponents" :key="index2">
                          <div v-if="component != 'n/A' && index2 > 10 && index2 <= 24">
                            <span v-if="maxTuning[index2] > -1 && component != 'Panzerung'">{{component}} <span
                                class="badge badge-secondary">{{getComponentName(index2)}}</span><span
                                class="badge badge-primary ml-1 mr-1">{{tuningCost[index2]}} Tuningteile</span></span>
                            <vue-range-slider v-if="maxTuning[index2] > -1 && component != 'Panzerung'" ref="slider"
                              tooltip="false" dotSize="14" height="13" :min="-1" :max="maxTuning[index2]" value='0'
                              :step="1" v-model="componentNumbers[index2]"
                              v-oninput="tuningPreview(index2, componentNumbers[index2])" />
                          </div>
                        </div>
                      </div>
                      <div v-else>
                        <span style="display: flex; justify-content: center; align-items: center;">Keine kompatiblen
                          Tuningteile vorhanden!</span>
                      </div>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 3'">
                      <div v-if="checkTuning(3) == 1">
                        <div v-for="(component, index3) in tuningComponents" :key="index3">
                          <div v-if="component != 'n/A' && index3 > 24">
                            <span v-if="maxTuning[index3] > -1">{{component}} <span
                                class="badge badge-secondary">{{getComponentName(index3)}}</span><span
                                class="badge badge-primary ml-1 mr-1">{{tuningCost[index3]}} Tuningteile</span></span>
                            <vue-range-slider v-if="maxTuning[index3] > -1" ref="slider" tooltip="false" dotSize="14"
                              height="13" :min="-1" :max="maxTuning[index3]" value='0' :step="1"
                              v-model="componentNumbers[index3]"
                              v-oninput="tuningPreview(index3, componentNumbers[index3])" />
                          </div>
                        </div>
                      </div>
                      <div v-else>
                        <span style="display: flex; justify-content: center; align-items: center;">Keine kompatiblen
                          Tuningteile vorhanden!</span>
                      </div>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 4'">
                      <span>Turbo PSI <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[58]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[58]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.01" :max="2.0"
                        value='0.25' :step="0.01" v-model="componentNumbers[58]"
                        v-oninput="tuningPreview(58, componentNumbers[58])" />
                      <span>Zündzeitpunkt <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[59]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[59]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.01" :max="2.0"
                        value='1.3' :step="0.01" v-model="componentNumbers[59]"
                        v-oninput="tuningPreview(59, componentNumbers[59])" />
                      <span>Transmission <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[60]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[60]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="1" :max="50"
                        value='10' :step="1" v-model="componentNumbers[60]"
                        v-oninput="tuningPreview(60, componentNumbers[60])" />
                      <span>Bremskraft (R/F) <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[61]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[61]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.0" :max="2"
                        value='0.5' :step="0.01" v-model="componentNumbers[61]"
                        v-oninput="tuningPreview(61, componentNumbers[61])" />
                      <span>Bremskraft (W/S) <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[62]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[62]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.01" :max="2"
                        value='1.4' :step="0.01" v-model="componentNumbers[62]"
                        v-oninput="tuningPreview(62, componentNumbers[62])" />
                      <span>Laufwerksvorspannung <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[63]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[63]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.00" :max="1"
                        value='0.5' :step="0.01" v-model="componentNumbers[63]"
                        v-oninput="tuningPreview(63, componentNumbers[63])" />
                    </div>
                    <div v-if="selectedTuning == 'Analyse'">
                      <div style="display: flex; justify-content: center; align-items: center; font-size: 1.3vw">
                        <span>Benötigte Tuningteile: {{tuningCosts}}</span>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center; font-size: 1.3vw">
                        <span>Vorhandene Tuningteile: {{stockTuning}}</span>
                      </div>
                      <hr />
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                          type="button" @click="syncTuningFunc(1)" class="btn btn-warning">Tuning
                          Synchronisieren</button>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                          @click="resetTuning(1)" type="button" class="btn btn-danger">Tuning reseten</button>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                          @click="setTuning()" type="button" class="btn btn-success">Tuning anbringen</button>
                      </div>
                    </div>
                    <hr>
                    <span style="display: flex; justify-content: center; align-items: center;">Benutze Taste [K] um die
                      Kamera frei bewegen zu können!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import getVehicleColors from '../assets/vehicleColors.js'

export default {
  name: 'nTuning',
  data: function () {
    return {
      selectedTuning: '',
      maxTuning: [],
      defaultHandling: [],
      tuningComponents: ['Spoiler', 'Vordere Stoßstange', 'Hintere Stoßstange', 'Seitenschweller', 'Auspuff', 'Rahmen', 'Gitter', 'Motorhaube', 'Linker Kotflügel', 'Rechter Kotflügel', 'Dach', 'Motor', 'Bremsen', 'Getriebe', 'Hupe', 'Federung', 'Panzerung', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'Xeon Scheinwerfer', 'Reifen', 'Hinterreifen', 'n/A', 'n/A', 'Beschläge', 'Ornaments', 'Armaturenbrett', 'Tacho', 'Türlautsprecher', 'Sitze', 'Lenkrad', 'Schalthebel', 'Fahrzeugplaketten', 'Lautsprecher', 'Kofferraum', 'Hydraulik', 'Motorblock', 'Nitro', 'Streben', 'Arch-Abdeckung', 'Antenne', 'Fahrzeugausstattung', 'Tank', 'Fenster', 'n/A', 'Paintjob', 'n/A', 'n/A', 'n/A', 'n/A', 'Kennzeichen', 'n/A', 'Fensterfarben', 'Neon', 'Reifenqualm'],
      tuningCost: [8, 9, 9, 7, 7, 8, 8, 9, 8, 8, 6, 105, 30, 40, 5, 7, 60, 0, 0, 0, 0, 0, 6, 10, 10, 0, 0, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 8, 15, 10, 55, 8, 7, 7, 6, 6, 6, 0, 15, 0, 0, 0, 0, 6, 0, 8, 90, 70, 20, 15, 20, 15, 15, 15],
      componentNumbers: [64],
      componentNumbersBackup: [64],
      pearlEffect: false,
      tuningReset: (Date.now() / 1000),
      tuningSync: (Date.now() / 1000),
      tuningSet: (Date.now() / 1000),
      stockTuning: 0,
      tuningCosts: 0,
    }
  },
  methods:
  {
    syncTuningFunc(set) {
      if ((Date.now() / 1000) > this.tuningSync || set == 0) {
        if (this.tuningCosts <= 0 && set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Es wurden keine neuen Tuningteile angebracht!', 'error', 'top-left', '3250');
          return;
        }
        // eslint-disable-next-line no-undef
        mp.trigger('Client:TuningSync', this.componentNumbers.join(','));
        if (set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Das Tuning wurde synchronisiert!', 'success', 'top-left', '3250');
          this.tuningSync = (Date.now() / 1000) + (10);
        }
      } else {
        if (set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Du musst kurz warten, bevor du das Tuning wieder synchronisieren kannst!', 'error', 'top-left', '3250');
        }
      }
    },
    getTuningComp() {
      let count = 0;
      for (let i = 0; i <= 69; i++) {
        if (this.componentNumbers[i] != this.componentNumbersBackup[i] && !isNaN(this.componentNumbersBackup[i])) {
          if (i == 66 || i == 67 || i == 68 || i == 69) {
            count += 8;
          } else {
            count += this.tuningCost[i];
          }
        }
      }
      return count;
    },
    resetTuning(set) {
      if ((Date.now() / 1000) > this.tuningReset || set == 0) {
        if (this.tuningCosts <= 0 && set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Es wurden keine neuen Tuningteile angebracht!', 'error', 'top-left', '3250');
          return;
        }
        if (set == 1) {
          this.tuningReset = (Date.now() / 1000) + (7);
        }
        for (let i = 0; i < this.componentNumbersBackup.length; i++) {
          if (this.componentNumbersBackup[i]) {
            this.componentNumbers[i] = this.componentNumbersBackup[i];
            this.tuningPreview(i, this.componentNumbersBackup[i]);
          }
        }
        this.tuningCosts = this.getTuningComp();
        this.pearlEffect = false;
        if (parseInt(set) == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Das Tuning wurde resetet!', 'success', 'top-left', '3250');
        }
      } else {
        if (parseInt(set) == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Du musst kurz warten, bevor du das Tuning wieder reseten kannst!', 'error', 'top-left', '3250');
        }
      }
    },
    setTuning() {
      if ((Date.now() / 1000) > this.tuningSet) {
        if (this.tuningCosts > 0) {
          if (this.componentNumbers[11] > -1) {
            if (this.componentNumbers[11] > this.componentNumbersBackup[11] && (this.componentNumbers[11] - 1 == this.componentNumbers[11])) {
              // eslint-disable-next-line no-undef
              mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst diese Tuningkomponente nur schrittweise erhöhen!', 'error', 'top-left', '3250');
              return;
            }
          }
          if (this.componentNumbers[12] > -1) {
            if (this.componentNumbers[12] > this.componentNumbersBackup[12] && (this.componentNumbers[12] - 1 == this.componentNumbers[12])) {
              // eslint-disable-next-line no-undef
              mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst diese Tuningkomponente nur schrittweise erhöhen!', 'error', 'top-left', '3250');
              return;
            }
          }
          if (this.componentNumbers[13] > -1) {
            if (this.componentNumbers[13] > this.componentNumbersBackup[13] && (this.componentNumbers[13] - 1 == this.componentNumbers[13])) {
              // eslint-disable-next-line no-undef
              mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst diese Tuningkomponente nur schrittweise erhöhen!', 'error', 'top-left', '3250');
              return;
            }
          }
          // eslint-disable-next-line no-undef
          mp.trigger('Client:TuningSet', this.componentNumbers.join(','), this.tuningCosts);
          this.tuningSet = (Date.now() / 1000) + (10);
        } else {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Es wurden keine neuen Tuningteile angebracht!', 'error', 'top-left', '3250');
        }
      }
    },
    setPerlmutt() {
      this.pearlEffect = !this.pearlEffect;
    },
    resetPerlmutt() {
      if (this.pearlEffect == true) {
        this.pearlEffect = false;
        this.tuningPreview(69, 255);
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Der Perlmutt Effekt wurde resetet!', 'success', 'top-left', '3250');
      } else {
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Kein Perlmutt Effekt vorhanden!', 'error', 'top-left', '3250');
      }
    },
    checkTuning(index) {
      let check = false;
      for (let i = 0; i <= 56; i++) {
        if (index == 1) {
          if (i > 10) break;
          if (this.maxTuning[i] != -1) {
            check = true;
          }
        } else if (index == 2) {
          if (i <= 10) continue;
          if (i > 24) break;
          if (this.maxTuning[i] != -1) {
            check = true;
          }
        } else if (index == 3) {
          if (i <= 24) continue;
          if (this.maxTuning[i] > -1) {
            check = true;
          }
        }
      }
      if (check == true) {
        return 1;
      }
      return 0;
    },
    getComponentName(index) {
      if (index == 11) {
        if (this.componentNumbers[index] == -1) {
          return 'Standard Motor';
        } else if (this.componentNumbers[index] == 0) {
          return 'EMS-Verbesserung 1';
        } else if (this.componentNumbers[index] == 1) {
          return 'EMS-Verbesserung 2';
        } else if (this.componentNumbers[index] == 2) {
          return 'EMS-Verbesserung 3';
        } else if (this.componentNumbers[index] == 3) {
          return 'EMS-Verbesserung 4';
        }
      } else if (index == 12) {
        if (this.componentNumbers[index] == -1) {
          return 'Standardbremsen';
        } else if (this.componentNumbers[index] == 0) {
          return 'Straßenbremsen';
        } else if (this.componentNumbers[index] == 1) {
          return 'Sportbremsen';
        } else if (this.componentNumbers[index] == 2) {
          return 'Rennbremsen';
        }
      } else if (index == 13) {
        if (this.componentNumbers[index] == -1) {
          return 'Standardgetriebe';
        } else if (this.componentNumbers[index] == 0) {
          return 'Straßengetriebe';
        } else if (this.componentNumbers[index] == 1) {
          return 'Sportgetriebe';
        } else if (this.componentNumbers[index] == 2) {
          return 'Renngetriebe';
        }
      } else if (index == 22) {
        if (this.componentNumbers[index] == -1) {
          return 'Keine';
        } else if (this.componentNumbers[index] == 0) {
          return 'Weiss';
        } else if (this.componentNumbers[index] == 1) {
          return 'Blau';
        } else if (this.componentNumbers[index] == 2) {
          return 'Hellblau';
        } else if (this.componentNumbers[index] == 3) {
          return 'Hellgrün';
        } else if (this.componentNumbers[index] == 4) {
          return 'Hellgelb';
        } else if (this.componentNumbers[index] == 5) {
          return 'Gelb';
        } else if (this.componentNumbers[index] == 6) {
          return 'Orange';
        } else if (this.componentNumbers[index] == 7) {
          return 'Rot';
        } else if (this.componentNumbers[index] == 8) {
          return 'Hellpink';
        } else if (this.componentNumbers[index] == 9) {
          return 'Pink';
        } else if (this.componentNumbers[index] == 10) {
          return 'Lila';
        } else if (this.componentNumbers[index] == 11) {
          return 'Helllila';
        }
      } else if (index == 56 || index == 57) {
        if (this.componentNumbers[index] == -1) {
          if (index == 56) {
            return 'Kein Neon';
          } else {
            return 'Kein Reifenqualm';
          }
        } else if (this.componentNumbers[index] == 0) {
          return 'Rot';
        } else if (this.componentNumbers[index] == 1) {
          return 'Blau';
        } else if (this.componentNumbers[index] == 2) {
          return 'Grün';
        } else if (this.componentNumbers[index] == 3) {
          return 'Gelb';
        } else if (this.componentNumbers[index] == 4) {
          return 'Lila';
        } else if (this.componentNumbers[index] == 5) {
          return 'Orange';
        } else if (this.componentNumbers[index] == 6) {
          return 'Türkis';
        } else if (this.componentNumbers[index] == 7) {
          return 'Rosa';
        } else if (this.componentNumbers[index] == 8) {
          return 'Kadet Blau';
        } else if (this.componentNumbers[index] == 9) {
          return 'Weiss';
        } else if (this.componentNumbers[index] == 10) {
          return 'Schwarz';
        }
      } else if (index == 40) {
        if (this.componentNumbers[index] == -1) {
          return 'Kein Nitro';
        } else if (this.componentNumbers[index] == 0) {
          return 'Nitro';
        }
      } else {
        return this.componentNumbers[index];
      }
    },
    showTuning(csvString, stock) {
      if (this.tuningshow == false) {
        this.componentNumbers = csvString.split(',');
        for (let i = 0; i < this.componentNumbers.length; i++) {
          if (this.componentNumbers[i]) {
            this.componentNumbersBackup[i] = this.componentNumbers[i];
          }
        }
        this.vehicleColors = getVehicleColors;
        this.pearlEffect = false;
        this.selectedTuning = 'Farbe 1';
        this.tuningSync = (Date.now() / 1000);
        this.tuningSet = (Date.now() / 1000);
        this.stockTuning = parseInt(stock);
        this.tuningCosts = 0;
        this.syncTuning = (Date.now() / 1000);
        this.tuningReset = (Date.now() / 1000);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:GetMaxTuning');
        // eslint-disable-next-line no-undef
        mp.trigger('Client:GetDefaultChiptuning');
      }
      var self = this;
      setTimeout(function () {
        self.tuningshow = !self.tuningshow;
      }, 55);
    },
    tuningPreview(tuning, component) {
      let perlm = false;
      if (tuning == 66) {
        perlm = this.perlmutt1;
        if (perlm == true) {
          this.componentNumbers[69] = component;
          this.$forceUpdate();
        } else {
          this.componentNumbers[66] = component;
        }
      } else if (tuning == 67) {
        this.componentNumbers[67] = component;
      } else if (tuning == 68) {
        this.componentNumbers[68] = component;
      } else if (tuning == 69) {
        perlm = this.perlmutt1;
        this.componentNumbers[69] = component;
        this.tuningCosts = this.getTuningComp();
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:TuningPreview', tuning, component, perlm);
    },
    selectTuning(tuning) {
      var self = this;
      if (tuning == 'Analyse') {
        this.tuningCosts = this.getTuningComp();
      }
      setTimeout(function () {
        self.selectedTuning = tuning;
      }, 35);
    },
    setMaxTuning(max) {
      this.maxTuning = JSON.parse(max);
    },
    setDefaultChiptuning(handling) {
      this.defaultHandling = JSON.parse(handling);
      this.componentNumbers[58] = this.defaultHandling[0];
      this.componentNumbers[59] = this.defaultHandling[1];
      this.componentNumbers[60] = this.defaultHandling[2];
      this.componentNumbers[61] = this.defaultHandling[3];
      this.componentNumbers[62] = this.defaultHandling[4];
      this.componentNumbers[63] = this.defaultHandling[5];
      this.$forceUpdate();
    },
  }
}
</script>

<style scoped>
html, body, template, * {
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -o-user-select: none;
  user-select: none;
}

.tuning {
  width: 100%;
  margin: 0 auto;
  margin-top: 10px;
  display: flex;
  flex-direction: column;
  cursor: pointer;
}

.tuning ul {
  display: flex;
  grid-gap: 3px;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
  list-style: none;
  padding: 0;
}

.tuning ul li {
  width: 25px;
  height: 25px;
  border-radius: 100%;
  transition: all 0.4s ease;
  margin: 3px;
  border: 2px solid transparent;
}

.tuning ul li:active {
  background: white !important;
}

.tuning ul li:hover {
  box-shadow: 0 5px 10px rgb(0 0 0 / 15%);
  border-color: white;
}

.tuningCentering {
  margin: 0;
  position: absolute;
  top: 15%;
  right: 37%;
  margin-right: -50%;
  transform: translate(-50%, -50%)
}

.tuningIconActive {
  max-width: 42px;
  height: auto;
  border: 3px solid #fff;
  border-radius: 1vw;
  padding: 4px;
  text-shadow: 0 0 2px #000;
}

.tuningIcon {
  max-width: 42px;
  border: 3px solid #3F6791;
  border-radius: 1vw;
  padding: 4px;
  text-shadow: 0 0 2px #000;
}

.tuningIcon:hover {
  border: 3px solid #fff;
  text-shadow: 0 0 2px #000;
}
</style>