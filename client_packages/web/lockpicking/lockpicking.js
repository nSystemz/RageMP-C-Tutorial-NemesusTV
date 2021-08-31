class Pin {
  constructor() {
    let self = this;
    this.range = {
      min: 0,
      max: 100
    };
    this.level = 0;
    this.currentposition = 0;
    this.threshold = 0;
    this.point = 50;
    this.domhandler = new DomHandler();
    this.limit = 0;
    this.twisting = false;
    this.vibrating = false;

    let _duration = 100;
    Object.defineProperty(this, 'duration', {
      get: () => {
        return _duration;
      },
      set: (value) => {
        _duration = value;

        if (_duration % 2 === 0) {
          self.vibrating = !self.vibrating;
        }
        if (!_duration) {
          this.domhandler.pop("Dietrich abgebrochen!", "#c0392b");
          this.resetLockpick(1);
	  setTimeout(function(){ mp.trigger('failedLockpicking'); }, 1250);
        }
      }
    });

    let _completed = 0;
    Object.defineProperty(this, 'completed', {
      get: () => {
        return _completed;
      },
      set: (value) => {
        _completed = value;

        if (_completed === 100) {
          this.domhandler.pop("Fahrzeug geknackt!", "#27af60");
          self.resetLockpick(self.level + 1);
          setTimeout(function(){ mp.trigger('successLockpicking'); }, 1250);
        }
      }
    });

    this.duration = 100;
    this.completed = 0;

    this.resetLockpick(1);
  }

  setPinPosition(value) {
    const valueadded = this.point + value;
    if (valueadded >= 0 && valueadded <= 100) {
      this.point = valueadded;
    }
  }

  calculatetwistlimit() {
    let pointrange = {
      min: this.currentposition - this.threshold,
      max: this.currentposition + this.threshold
    };
    if (this.point < pointrange.min) {
      this.limit = this.percentageprogress(this.point, this.range.min, pointrange.min);
    } else if (this.point > pointrange.max) {
      this.limit = this.percentageprogress(100 - this.point, 100 - pointrange.max, 0) * -1;
    } else {
      this.limit = 100;
    }
  }

  percentageprogress(currentvalue, min, max) {
    return currentvalue / (max - min) * 100;
  }

  twistlock() {
    if (this.completed < this.limit) {
      this.completed += 2;
      return;
    }
    if (this.completed < 100 && this.duration > 0) {
      this.duration -= 1;
    }
  }

  untwistlock() {
    if (this.completed > 0) {
      this.completed -= 2;
      this.vibrating = true;
    }
  }

  resetLockpick(lvl) {
    this.level = lvl;
    this.threshold = 20 / this.level;
    this.currentposition = Math.floor(Math.random() * (this.range.max - this.range.min + 1)) + this.range.min;
    this.duration = 100;
    this.twisting = false;
    this.completed = 0;
    this.domhandler.setscore(this.level);
  }
}
class DomHandler {
  constructor() {
    this.innerpopout = document.querySelectorAll('.pop-stripe')[0];
    this.score = document.querySelectorAll('.score')[0];
  }

  pop(message, color) {
    let self = this;
    this.popoutstart(message, color);
    setTimeout(() => {
      self.popoutend();
    }, 1000);
  }

  popoutstart(message, color) {
    this.innerpopout.style.background = color;
    this.innerpopout.textContent = message;
    this.innerpopout.classList.add('show');

  }

  popoutend() {
    this.innerpopout.classList.remove('show');
  }

  setscore(lvl) {
  }
}
class PicklockGame {
  constructor() {
    let self = this;
    this.canvas = document.getElementById('PicklockGame');
    this.context = this.canvas.getContext('2d');
    this.lastposition = {};
    this.pin = new Pin();

    window.addEventListener('resize', () => {
      self.resizeCanvas()
    }, false);

    window.addEventListener('mousemove', () => {
      if (typeof(self.lastposition.x) != 'undefined' && !self.pin.twisting) {
        let deltaX = self.lastposition.x - event.offsetX,
          deltaY = self.lastposition.y - event.offsetY;
        if (Math.abs(deltaX) > Math.abs(deltaY) && deltaX > 0) {
          self.pin.setPinPosition(-0.5);
        } else if (Math.abs(deltaX) > Math.abs(deltaY) && deltaX < 0) {
          self.pin.setPinPosition(0.5);
        }
        self.pin.calculatetwistlimit();
      };
      self.lastposition = {
        x: event.offsetX,
        y: event.offsetY
      };
    }, false);

    window.addEventListener('mousedown', () => {
      self.pin.twisting = true;
    }, false);

    window.addEventListener('mouseup', () => {
      self.pin.twisting = false;
    }, false);

    this.resizeCanvas();
    this.frame();
  }

  resizeCanvas() {
    this.canvas.width = window.innerWidth;
    this.canvas.height = window.innerHeight;
  }

  frame() {
    let self = this;
    this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    if (this.pin.twisting) {
      this.pin.twistlock();
    } else {
      this.pin.untwistlock();
    }
    this.drawlock();
    this.drawlockhole();
    this.drawpin();
    window.requestAnimationFrame(() => {
      self.frame();
    });
  }

  drawlock() {
    this.context.save();
    this.context.beginPath();
    this.context.lineWidth = 0;
    this.context.arc(this.canvas.width / 2, this.canvas.height / 2, 100, 0, 2 * Math.PI, false);
    this.context.fillStyle = '#bdc3c7';
    this.context.fill();
    this.context.stroke();
    this.context.closePath();
    this.context.restore();
  }

  drawlockhole() {
    let percentage = this.pin.completed / 100;
    let degrees = percentage * 90.0;
    let radians = degrees * (Math.PI / 180);
    let x = this.canvas.width / 2;
    let y = this.canvas.height / 2;

    this.context.save();
    this.context.translate(x, y);
    this.context.rotate(radians);
    this.context.translate(-x, -y);
    this.context.fillRect(x, y, 4, 18);
    this.context.fillRect(x, y, 4, -18);
    this.context.fillRect(x, y, -4, 18);
    this.context.fillRect(x, y, -4, -18);
    this.context.restore();
  }

  drawpin() {
    let offset = this.pin.vibrating ? 2 : 0;
    let percentage = (this.pin.point + offset) / 100;
    let degrees = (percentage + 0.5) * 180.0;
    let radians = degrees * (Math.PI / 180);
    let x = this.canvas.width / 2;
    let y = this.canvas.height / 2;

    this.context.save();
    this.context.translate(x, y);
    this.context.rotate(radians);
    this.context.translate(-x, -y);
    this.context.fillStyle = '#000000';
    this.context.fillRect(x - 1, y, 4, 170);
    this.context.fillRect(x + 1, y, -4, 170);
    this.context.restore();
  }
}

new PicklockGame();