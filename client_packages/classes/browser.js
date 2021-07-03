let Browsers = [];

class Browser {
    constructor(identifier, path) {
        this.identifier = identifier;
        this.path = path;
        this.browser = null;
        this.open();
    }

    open() {
        this.browser = mp.browsers.new(this.path);
        Browsers.push(this);
    }

    close() {
        this.browser.destroy();
        Browsers.splice(Browsers.indexOf(this), 1);
    }

    callFunction() {
        let code = '' +arguments[0];

        if(arguments.length > 1) {
            code += "(";
            for(var i = 1; i < arguments.length; i++) {
                if(i == arguments.length-1) code += JSON.stringify(arguments[i]) +")";
                else code += JSON.stringify(arguments[i]) + ',';
            }
        } else {
            code += '()';
        }

        this.browser.execute(code);
    }
}

exports = Browser;