(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports, require('vue')) :
    typeof define === 'function' && define.amd ? define(['exports', 'vue'], factory) :
    (global = typeof globalThis !== 'undefined' ? globalThis : global || self, factory(global.VueToastification = {}, global.Vue));
}(this, (function (exports, Vue) { 'use strict';

    function _interopDefaultLegacy (e) { return e && typeof e === 'object' && 'default' in e ? e : { 'default': e }; }

    var Vue__default = /*#__PURE__*/_interopDefaultLegacy(Vue);

    /*! *****************************************************************************
    Copyright (c) Microsoft Corporation.

    Permission to use, copy, modify, and/or distribute this software for any
    purpose with or without fee is hereby granted.

    THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH
    REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY
    AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT,
    INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM
    LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR
    OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR
    PERFORMANCE OF THIS SOFTWARE.
    ***************************************************************************** */

    function __awaiter(thisArg, _arguments, P, generator) {
        function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
        return new (P || (P = Promise))(function (resolve, reject) {
            function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
            function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
            function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
            step((generator = generator.apply(thisArg, _arguments || [])).next());
        });
    }

    function __generator(thisArg, body) {
        var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
        return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
        function verb(n) { return function (v) { return step([n, v]); }; }
        function step(op) {
            if (f) throw new TypeError("Generator is already executing.");
            while (_) try {
                if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
                if (y = 0, t) op = [op[0] & 2, t.value];
                switch (op[0]) {
                    case 0: case 1: t = op; break;
                    case 4: _.label++; return { value: op[1], done: false };
                    case 5: _.label++; y = op[1]; op = [0]; continue;
                    case 7: op = _.ops.pop(); _.trys.pop(); continue;
                    default:
                        if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                        if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                        if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                        if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                        if (t[2]) _.ops.pop();
                        _.trys.pop(); continue;
                }
                op = body.call(thisArg, _);
            } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
            if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
        }
    }

    (function (TYPE) {
        TYPE["SUCCESS"] = "success";
        TYPE["ERROR"] = "error";
        TYPE["WARNING"] = "warning";
        TYPE["INFO"] = "info";
        TYPE["DEFAULT"] = "default";
    })(exports.TYPE || (exports.TYPE = {}));
    (function (POSITION) {
        POSITION["TOP_LEFT"] = "top-left";
        POSITION["TOP_CENTER"] = "top-center";
        POSITION["TOP_RIGHT"] = "top-right";
        POSITION["BOTTOM_LEFT"] = "bottom-left";
        POSITION["BOTTOM_CENTER"] = "bottom-center";
        POSITION["BOTTOM_RIGHT"] = "bottom-right";
    })(exports.POSITION || (exports.POSITION = {}));
    var EVENTS;
    (function (EVENTS) {
        EVENTS["ADD"] = "add";
        EVENTS["DISMISS"] = "dismiss";
        EVENTS["UPDATE"] = "update";
        EVENTS["CLEAR"] = "clear";
        EVENTS["UPDATE_DEFAULTS"] = "update_defaults";
    })(EVENTS || (EVENTS = {}));
    var VT_NAMESPACE = "Vue-Toastification";

    var COMMON = {
        type: {
            type: String,
            default: exports.TYPE.DEFAULT,
        },
        classNames: {
            type: [String, Array],
            default: function () { return []; },
        },
        trueBoolean: {
            type: Boolean,
            default: true,
        },
    };
    var ICON = {
        type: COMMON.type,
        customIcon: {
            type: [String, Boolean, Object, Function],
            default: true,
        },
    };
    var CLOSE_BUTTON = {
        component: {
            type: [String, Object, Function, Boolean],
            default: "button",
        },
        classNames: COMMON.classNames,
        showOnHover: Boolean,
        ariaLabel: {
            type: String,
            default: "close",
        },
    };
    var PROGRESS_BAR = {
        timeout: {
            type: [Number, Boolean],
            default: 5000,
        },
        hideProgressBar: Boolean,
        isRunning: Boolean,
    };
    var TRANSITION = {
        transition: {
            type: [Object, String],
            default: VT_NAMESPACE + "__bounce",
        },
        transitionDuration: {
            type: [Number, Object],
            default: 750,
        },
    };
    var CORE_TOAST = {
        position: {
            type: String,
            default: exports.POSITION.TOP_RIGHT,
        },
        draggable: COMMON.trueBoolean,
        draggablePercent: {
            type: Number,
            default: 0.6,
        },
        pauseOnFocusLoss: COMMON.trueBoolean,
        pauseOnHover: COMMON.trueBoolean,
        closeOnClick: COMMON.trueBoolean,
        timeout: PROGRESS_BAR.timeout,
        hideProgressBar: PROGRESS_BAR.hideProgressBar,
        toastClassName: COMMON.classNames,
        bodyClassName: COMMON.classNames,
        icon: ICON.customIcon,
        closeButton: CLOSE_BUTTON.component,
        closeButtonClassName: CLOSE_BUTTON.classNames,
        showCloseButtonOnHover: CLOSE_BUTTON.showOnHover,
        accessibility: {
            type: Object,
            default: function () { return ({
                toastRole: "alert",
                closeButtonLabel: "close",
            }); },
        },
        rtl: Boolean,
        eventBus: Object,
    };
    var TOAST = {
        id: {
            type: [String, Number],
            required: true,
        },
        type: COMMON.type,
        content: {
            type: [String, Object, Function],
            required: true,
        },
        onClick: Function,
        onClose: Function,
    };
    var CONTAINER = {
        container: {
            type: undefined,
            default: function () { return document.body; },
        },
        newestOnTop: COMMON.trueBoolean,
        maxToasts: {
            type: Number,
            default: 20,
        },
        transition: TRANSITION.transition,
        transitionDuration: TRANSITION.transitionDuration,
        toastDefaults: Object,
        filterBeforeCreate: {
            type: Function,
            default: function (toast) { return toast; },
        },
        filterToasts: {
            type: Function,
            default: function (toasts) { return toasts; },
        },
        containerClassName: COMMON.classNames,
        onMounted: Function,
    };
    var PROPS = {
        CORE_TOAST: CORE_TOAST,
        TOAST: TOAST,
        CONTAINER: CONTAINER,
        PROGRESS_BAR: PROGRESS_BAR,
        ICON: ICON,
        TRANSITION: TRANSITION,
        CLOSE_BUTTON: CLOSE_BUTTON,
    };

    // eslint-disable-next-line @typescript-eslint/ban-types
    var isFunction = function (value) {
        return typeof value === "function";
    };
    var isString = function (value) { return typeof value === "string"; };
    var isNonEmptyString = function (value) {
        return isString(value) && value.trim().length > 0;
    };
    var isNumber = function (value) { return typeof value === "number"; };
    var isUndefined = function (value) {
        return typeof value === "undefined";
    };
    var isObject = function (value) {
        return typeof value === "object" && value !== null;
    };
    var isJSX = function (obj) {
        return hasProp(obj, "tag") && isNonEmptyString(obj.tag);
    };
    var isTouchEvent = function (event) {
        return window.TouchEvent && event instanceof TouchEvent;
    };
    var isToastComponent = function (obj) {
        return hasProp(obj, "component") && isToastContent(obj.component);
    };
    var isConstructor = function (c) {
        return isFunction(c) && hasProp(c, "cid");
    };
    var isVueComponent = function (c) {
        if (isConstructor(c)) {
            return true;
        }
        if (!isObject(c)) {
            return false;
        }
        if (c.extends || c._Ctor) {
            return true;
        }
        if (isString(c.template)) {
            return true;
        }
        return hasRenderFunction(c);
    };
    var isVueInstanceOrComponent = function (obj) { return obj instanceof Vue__default['default'] || isVueComponent(obj); };
    var isToastContent = function (obj) {
        // Ignore undefined
        return !isUndefined(obj) &&
            // Is a string
            (isString(obj) ||
                // Regular Vue instance or component
                isVueInstanceOrComponent(obj) ||
                // Object with a render function
                hasRenderFunction(obj) ||
                // JSX template
                isJSX(obj) ||
                // Nested object
                isToastComponent(obj));
    };
    var isDOMRect = function (obj) {
        return isObject(obj) &&
            isNumber(obj.height) &&
            isNumber(obj.width) &&
            isNumber(obj.right) &&
            isNumber(obj.left) &&
            isNumber(obj.top) &&
            isNumber(obj.bottom);
    };
    var hasProp = function (obj, propKey) {
        return Object.prototype.hasOwnProperty.call(obj, propKey);
    };
    var hasRenderFunction = function (obj
    // eslint-disable-next-line @typescript-eslint/ban-types
    ) {
        return hasProp(obj, "render") && isFunction(obj.render);
    };
    /**
     * ID generator
     */
    var getId = (function (i) { return function () { return i++; }; })(0);
    function getX(event) {
        return isTouchEvent(event) ? event.targetTouches[0].clientX : event.clientX;
    }
    function getY(event) {
        return isTouchEvent(event) ? event.targetTouches[0].clientY : event.clientY;
    }
    var removeElement = function (el) {
        if (!isUndefined(el.remove)) {
            el.remove();
        }
        else if (el.parentNode) {
            el.parentNode.removeChild(el);
        }
    };
    var getVueComponentFromObj = function (obj) {
        if (isToastComponent(obj)) {
            // Recurse if component prop
            return getVueComponentFromObj(obj.component);
        }
        if (isJSX(obj)) {
            // Create render function for JSX
            return {
                render: function () {
                    return obj;
                },
            };
        }
        // Return the actual object if regular vue component
        return obj;
    };

    var script = Vue__default['default'].extend({
        props: PROPS.PROGRESS_BAR,
        data: function () {
            return {
                hasClass: true,
            };
        },
        computed: {
            style: function () {
                return {
                    animationDuration: this.timeout + "ms",
                    animationPlayState: this.isRunning ? "running" : "paused",
                    opacity: this.hideProgressBar ? 0 : 1,
                };
            },
            cpClass: function () {
                return this.hasClass ? VT_NAMESPACE + "__progress-bar" : "";
            },
        },
        mounted: function () {
            this.$el.addEventListener("animationend", this.animationEnded);
        },
        beforeDestroy: function () {
            this.$el.removeEventListener("animationend", this.animationEnded);
        },
        methods: {
            animationEnded: function () {
                this.$emit("close-toast");
            },
        },
        watch: {
            timeout: function () {
                var _this = this;
                this.hasClass = false;
                this.$nextTick(function () { return (_this.hasClass = true); });
            },
        },
    });

    function normalizeComponent(template, style, script, scopeId, isFunctionalTemplate, moduleIdentifier /* server only */, shadowMode, createInjector, createInjectorSSR, createInjectorShadow) {
        if (typeof shadowMode !== 'boolean') {
            createInjectorSSR = createInjector;
            createInjector = shadowMode;
            shadowMode = false;
        }
        // Vue.extend constructor export interop.
        const options = typeof script === 'function' ? script.options : script;
        // render functions
        if (template && template.render) {
            options.render = template.render;
            options.staticRenderFns = template.staticRenderFns;
            options._compiled = true;
            // functional template
            if (isFunctionalTemplate) {
                options.functional = true;
            }
        }
        // scopedId
        if (scopeId) {
            options._scopeId = scopeId;
        }
        let hook;
        if (moduleIdentifier) {
            // server build
            hook = function (context) {
                // 2.3 injection
                context =
                    context || // cached call
                        (this.$vnode && this.$vnode.ssrContext) || // stateful
                        (this.parent && this.parent.$vnode && this.parent.$vnode.ssrContext); // functional
                // 2.2 with runInNewContext: true
                if (!context && typeof __VUE_SSR_CONTEXT__ !== 'undefined') {
                    context = __VUE_SSR_CONTEXT__;
                }
                // inject component styles
                if (style) {
                    style.call(this, createInjectorSSR(context));
                }
                // register component module identifier for async chunk inference
                if (context && context._registeredComponents) {
                    context._registeredComponents.add(moduleIdentifier);
                }
            };
            // used by ssr in case component is cached and beforeCreate
            // never gets called
            options._ssrRegister = hook;
        }
        else if (style) {
            hook = shadowMode
                ? function (context) {
                    style.call(this, createInjectorShadow(context, this.$root.$options.shadowRoot));
                }
                : function (context) {
                    style.call(this, createInjector(context));
                };
        }
        if (hook) {
            if (options.functional) {
                // register for functional component in vue file
                const originalRender = options.render;
                options.render = function renderWithStyleInjection(h, context) {
                    hook.call(context);
                    return originalRender(h, context);
                };
            }
            else {
                // inject component registration as beforeCreate hook
                const existing = options.beforeCreate;
                options.beforeCreate = existing ? [].concat(existing, hook) : [hook];
            }
        }
        return script;
    }

    /* script */
    const __vue_script__ = script;

    /* template */
    var __vue_render__ = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c("div", { class: _vm.cpClass, style: _vm.style })
    };
    var __vue_staticRenderFns__ = [];
    __vue_render__._withStripped = true;

      /* style */
      const __vue_inject_styles__ = undefined;
      /* scoped */
      const __vue_scope_id__ = undefined;
      /* module identifier */
      const __vue_module_identifier__ = undefined;
      /* functional template */
      const __vue_is_functional_template__ = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__ = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__, staticRenderFns: __vue_staticRenderFns__ },
        __vue_inject_styles__,
        __vue_script__,
        __vue_scope_id__,
        __vue_is_functional_template__,
        __vue_module_identifier__,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$1 = Vue__default['default'].extend({
        props: PROPS.CLOSE_BUTTON,
        computed: {
            buttonComponent: function () {
                if (this.component !== false) {
                    return getVueComponentFromObj(this.component);
                }
                return "button";
            },
            classes: function () {
                var classes = [VT_NAMESPACE + "__close-button"];
                if (this.showOnHover) {
                    classes.push("show-on-hover");
                }
                return classes.concat(this.classNames);
            },
        },
    });

    /* script */
    const __vue_script__$1 = script$1;

    /* template */
    var __vue_render__$1 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        _vm.buttonComponent,
        _vm._g(
          {
            tag: "component",
            class: _vm.classes,
            attrs: { "aria-label": _vm.ariaLabel }
          },
          _vm.$listeners
        ),
        [_vm._v("\n  ×\n")]
      )
    };
    var __vue_staticRenderFns__$1 = [];
    __vue_render__$1._withStripped = true;

      /* style */
      const __vue_inject_styles__$1 = undefined;
      /* scoped */
      const __vue_scope_id__$1 = undefined;
      /* module identifier */
      const __vue_module_identifier__$1 = undefined;
      /* functional template */
      const __vue_is_functional_template__$1 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$1 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$1, staticRenderFns: __vue_staticRenderFns__$1 },
        __vue_inject_styles__$1,
        __vue_script__$1,
        __vue_scope_id__$1,
        __vue_is_functional_template__$1,
        __vue_module_identifier__$1,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$2 = {};

    /* script */
    const __vue_script__$2 = script$2;

    /* template */
    var __vue_render__$2 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "svg",
        {
          staticClass: "svg-inline--fa fa-check-circle fa-w-16",
          attrs: {
            "aria-hidden": "true",
            focusable: "false",
            "data-prefix": "fas",
            "data-icon": "check-circle",
            role: "img",
            xmlns: "http://www.w3.org/2000/svg",
            viewBox: "0 0 512 512"
          }
        },
        [
          _c("path", {
            attrs: {
              fill: "currentColor",
              d:
                "M504 256c0 136.967-111.033 248-248 248S8 392.967 8 256 119.033 8 256 8s248 111.033 248 248zM227.314 387.314l184-184c6.248-6.248 6.248-16.379 0-22.627l-22.627-22.627c-6.248-6.249-16.379-6.249-22.628 0L216 308.118l-70.059-70.059c-6.248-6.248-16.379-6.248-22.628 0l-22.627 22.627c-6.248 6.248-6.248 16.379 0 22.627l104 104c6.249 6.249 16.379 6.249 22.628.001z"
            }
          })
        ]
      )
    };
    var __vue_staticRenderFns__$2 = [];
    __vue_render__$2._withStripped = true;

      /* style */
      const __vue_inject_styles__$2 = undefined;
      /* scoped */
      const __vue_scope_id__$2 = undefined;
      /* module identifier */
      const __vue_module_identifier__$2 = undefined;
      /* functional template */
      const __vue_is_functional_template__$2 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$2 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$2, staticRenderFns: __vue_staticRenderFns__$2 },
        __vue_inject_styles__$2,
        __vue_script__$2,
        __vue_scope_id__$2,
        __vue_is_functional_template__$2,
        __vue_module_identifier__$2,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$3 = {};

    /* script */
    const __vue_script__$3 = script$3;

    /* template */
    var __vue_render__$3 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "svg",
        {
          staticClass: "svg-inline--fa fa-info-circle fa-w-16",
          attrs: {
            "aria-hidden": "true",
            focusable: "false",
            "data-prefix": "fas",
            "data-icon": "info-circle",
            role: "img",
            xmlns: "http://www.w3.org/2000/svg",
            viewBox: "0 0 512 512"
          }
        },
        [
          _c("path", {
            attrs: {
              fill: "currentColor",
              d:
                "M256 8C119.043 8 8 119.083 8 256c0 136.997 111.043 248 248 248s248-111.003 248-248C504 119.083 392.957 8 256 8zm0 110c23.196 0 42 18.804 42 42s-18.804 42-42 42-42-18.804-42-42 18.804-42 42-42zm56 254c0 6.627-5.373 12-12 12h-88c-6.627 0-12-5.373-12-12v-24c0-6.627 5.373-12 12-12h12v-64h-12c-6.627 0-12-5.373-12-12v-24c0-6.627 5.373-12 12-12h64c6.627 0 12 5.373 12 12v100h12c6.627 0 12 5.373 12 12v24z"
            }
          })
        ]
      )
    };
    var __vue_staticRenderFns__$3 = [];
    __vue_render__$3._withStripped = true;

      /* style */
      const __vue_inject_styles__$3 = undefined;
      /* scoped */
      const __vue_scope_id__$3 = undefined;
      /* module identifier */
      const __vue_module_identifier__$3 = undefined;
      /* functional template */
      const __vue_is_functional_template__$3 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$3 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$3, staticRenderFns: __vue_staticRenderFns__$3 },
        __vue_inject_styles__$3,
        __vue_script__$3,
        __vue_scope_id__$3,
        __vue_is_functional_template__$3,
        __vue_module_identifier__$3,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$4 = {};

    /* script */
    const __vue_script__$4 = script$4;

    /* template */
    var __vue_render__$4 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "svg",
        {
          staticClass: "svg-inline--fa fa-exclamation-circle fa-w-16",
          attrs: {
            "aria-hidden": "true",
            focusable: "false",
            "data-prefix": "fas",
            "data-icon": "exclamation-circle",
            role: "img",
            xmlns: "http://www.w3.org/2000/svg",
            viewBox: "0 0 512 512"
          }
        },
        [
          _c("path", {
            attrs: {
              fill: "currentColor",
              d:
                "M504 256c0 136.997-111.043 248-248 248S8 392.997 8 256C8 119.083 119.043 8 256 8s248 111.083 248 248zm-248 50c-25.405 0-46 20.595-46 46s20.595 46 46 46 46-20.595 46-46-20.595-46-46-46zm-43.673-165.346l7.418 136c.347 6.364 5.609 11.346 11.982 11.346h48.546c6.373 0 11.635-4.982 11.982-11.346l7.418-136c.375-6.874-5.098-12.654-11.982-12.654h-63.383c-6.884 0-12.356 5.78-11.981 12.654z"
            }
          })
        ]
      )
    };
    var __vue_staticRenderFns__$4 = [];
    __vue_render__$4._withStripped = true;

      /* style */
      const __vue_inject_styles__$4 = undefined;
      /* scoped */
      const __vue_scope_id__$4 = undefined;
      /* module identifier */
      const __vue_module_identifier__$4 = undefined;
      /* functional template */
      const __vue_is_functional_template__$4 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$4 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$4, staticRenderFns: __vue_staticRenderFns__$4 },
        __vue_inject_styles__$4,
        __vue_script__$4,
        __vue_scope_id__$4,
        __vue_is_functional_template__$4,
        __vue_module_identifier__$4,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$5 = {};

    /* script */
    const __vue_script__$5 = script$5;

    /* template */
    var __vue_render__$5 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "svg",
        {
          staticClass: "svg-inline--fa fa-exclamation-triangle fa-w-18",
          attrs: {
            "aria-hidden": "true",
            focusable: "false",
            "data-prefix": "fas",
            "data-icon": "exclamation-triangle",
            role: "img",
            xmlns: "http://www.w3.org/2000/svg",
            viewBox: "0 0 576 512"
          }
        },
        [
          _c("path", {
            attrs: {
              fill: "currentColor",
              d:
                "M569.517 440.013C587.975 472.007 564.806 512 527.94 512H48.054c-36.937 0-59.999-40.055-41.577-71.987L246.423 23.985c18.467-32.009 64.72-31.951 83.154 0l239.94 416.028zM288 354c-25.405 0-46 20.595-46 46s20.595 46 46 46 46-20.595 46-46-20.595-46-46-46zm-43.673-165.346l7.418 136c.347 6.364 5.609 11.346 11.982 11.346h48.546c6.373 0 11.635-4.982 11.982-11.346l7.418-136c.375-6.874-5.098-12.654-11.982-12.654h-63.383c-6.884 0-12.356 5.78-11.981 12.654z"
            }
          })
        ]
      )
    };
    var __vue_staticRenderFns__$5 = [];
    __vue_render__$5._withStripped = true;

      /* style */
      const __vue_inject_styles__$5 = undefined;
      /* scoped */
      const __vue_scope_id__$5 = undefined;
      /* module identifier */
      const __vue_module_identifier__$5 = undefined;
      /* functional template */
      const __vue_is_functional_template__$5 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$5 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$5, staticRenderFns: __vue_staticRenderFns__$5 },
        __vue_inject_styles__$5,
        __vue_script__$5,
        __vue_scope_id__$5,
        __vue_is_functional_template__$5,
        __vue_module_identifier__$5,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$6 = Vue__default['default'].extend({
        props: PROPS.ICON,
        computed: {
            customIconChildren: function () {
                return hasProp(this.customIcon, "iconChildren")
                    ? this.trimValue(this.customIcon.iconChildren)
                    : "";
            },
            customIconClass: function () {
                if (isString(this.customIcon)) {
                    return this.trimValue(this.customIcon);
                }
                else if (hasProp(this.customIcon, "iconClass")) {
                    return this.trimValue(this.customIcon.iconClass);
                }
                return "";
            },
            customIconTag: function () {
                if (hasProp(this.customIcon, "iconTag")) {
                    return this.trimValue(this.customIcon.iconTag, "i");
                }
                return "i";
            },
            hasCustomIcon: function () {
                return this.customIconClass.length > 0;
            },
            component: function () {
                if (this.hasCustomIcon) {
                    return this.customIconTag;
                }
                if (isToastContent(this.customIcon)) {
                    return getVueComponentFromObj(this.customIcon);
                }
                return this.iconTypeComponent;
            },
            iconTypeComponent: function () {
                var _a;
                var types = (_a = {},
                    _a[exports.TYPE.DEFAULT] = __vue_component__$3,
                    _a[exports.TYPE.INFO] = __vue_component__$3,
                    _a[exports.TYPE.SUCCESS] = __vue_component__$2,
                    _a[exports.TYPE.ERROR] = __vue_component__$5,
                    _a[exports.TYPE.WARNING] = __vue_component__$4,
                    _a);
                return types[this.type];
            },
            iconClasses: function () {
                var classes = [VT_NAMESPACE + "__icon"];
                if (this.hasCustomIcon) {
                    return classes.concat(this.customIconClass);
                }
                return classes;
            },
        },
        methods: {
            trimValue: function (value, empty) {
                if (empty === void 0) { empty = ""; }
                return isNonEmptyString(value) ? value.trim() : empty;
            },
        },
    });

    /* script */
    const __vue_script__$6 = script$6;

    /* template */
    var __vue_render__$6 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(_vm.component, { tag: "component", class: _vm.iconClasses }, [
        _vm._v(_vm._s(_vm.customIconChildren))
      ])
    };
    var __vue_staticRenderFns__$6 = [];
    __vue_render__$6._withStripped = true;

      /* style */
      const __vue_inject_styles__$6 = undefined;
      /* scoped */
      const __vue_scope_id__$6 = undefined;
      /* module identifier */
      const __vue_module_identifier__$6 = undefined;
      /* functional template */
      const __vue_is_functional_template__$6 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$6 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$6, staticRenderFns: __vue_staticRenderFns__$6 },
        __vue_inject_styles__$6,
        __vue_script__$6,
        __vue_scope_id__$6,
        __vue_is_functional_template__$6,
        __vue_module_identifier__$6,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$7 = Vue__default['default'].extend({
        components: { ProgressBar: __vue_component__, CloseButton: __vue_component__$1, Icon: __vue_component__$6 },
        inheritAttrs: false,
        props: Object.assign({}, PROPS.CORE_TOAST, PROPS.TOAST),
        data: function () {
            var data = {
                isRunning: true,
                disableTransitions: false,
                beingDragged: false,
                dragStart: 0,
                dragPos: { x: 0, y: 0 },
                dragRect: {},
            };
            return data;
        },
        computed: {
            classes: function () {
                var classes = [
                    VT_NAMESPACE + "__toast",
                    VT_NAMESPACE + "__toast--" + this.type,
                    "" + this.position,
                ].concat(this.toastClassName);
                if (this.disableTransitions) {
                    classes.push("disable-transition");
                }
                if (this.rtl) {
                    classes.push(VT_NAMESPACE + "__toast--rtl");
                }
                return classes;
            },
            bodyClasses: function () {
                var classes = [
                    VT_NAMESPACE + "__toast-" + (isString(this.content) ? "body" : "component-body"),
                ].concat(this.bodyClassName);
                return classes;
            },
            draggableStyle: function () {
                if (this.dragStart === this.dragPos.x) {
                    return {};
                }
                if (this.beingDragged) {
                    return {
                        transform: "translateX(" + this.dragDelta + "px)",
                        opacity: 1 - Math.abs(this.dragDelta / this.removalDistance),
                    };
                }
                return {
                    transition: "transform 0.2s, opacity 0.2s",
                    transform: "translateX(0)",
                    opacity: 1,
                };
            },
            dragDelta: function () {
                return this.beingDragged ? this.dragPos.x - this.dragStart : 0;
            },
            removalDistance: function () {
                if (isDOMRect(this.dragRect)) {
                    return ((this.dragRect.right - this.dragRect.left) * this.draggablePercent);
                }
                return 0;
            },
        },
        mounted: function () {
            if (this.draggable) {
                this.draggableSetup();
            }
            if (this.pauseOnFocusLoss) {
                this.focusSetup();
            }
        },
        beforeDestroy: function () {
            if (this.draggable) {
                this.draggableCleanup();
            }
            if (this.pauseOnFocusLoss) {
                this.focusCleanup();
            }
        },
        destroyed: function () {
            var _this = this;
            setTimeout(function () {
                removeElement(_this.$el);
            }, 1000);
        },
        methods: {
            getVueComponentFromObj: getVueComponentFromObj,
            closeToast: function () {
                this.eventBus.$emit(EVENTS.DISMISS, this.id);
            },
            clickHandler: function () {
                if (this.onClick) {
                    this.onClick(this.closeToast);
                }
                if (this.closeOnClick) {
                    if (!this.beingDragged || this.dragStart === this.dragPos.x) {
                        this.closeToast();
                    }
                }
            },
            timeoutHandler: function () {
                this.closeToast();
            },
            hoverPause: function () {
                if (this.pauseOnHover) {
                    this.isRunning = false;
                }
            },
            hoverPlay: function () {
                if (this.pauseOnHover) {
                    this.isRunning = true;
                }
            },
            focusPause: function () {
                this.isRunning = false;
            },
            focusPlay: function () {
                this.isRunning = true;
            },
            focusSetup: function () {
                addEventListener("blur", this.focusPause);
                addEventListener("focus", this.focusPlay);
            },
            focusCleanup: function () {
                removeEventListener("blur", this.focusPause);
                removeEventListener("focus", this.focusPlay);
            },
            draggableSetup: function () {
                var element = this.$el;
                element.addEventListener("touchstart", this.onDragStart, {
                    passive: true,
                });
                element.addEventListener("mousedown", this.onDragStart);
                addEventListener("touchmove", this.onDragMove, { passive: false });
                addEventListener("mousemove", this.onDragMove);
                addEventListener("touchend", this.onDragEnd);
                addEventListener("mouseup", this.onDragEnd);
            },
            draggableCleanup: function () {
                var element = this.$el;
                element.removeEventListener("touchstart", this.onDragStart);
                element.removeEventListener("mousedown", this.onDragStart);
                removeEventListener("touchmove", this.onDragMove);
                removeEventListener("mousemove", this.onDragMove);
                removeEventListener("touchend", this.onDragEnd);
                removeEventListener("mouseup", this.onDragEnd);
            },
            onDragStart: function (event) {
                this.beingDragged = true;
                this.dragPos = { x: getX(event), y: getY(event) };
                this.dragStart = getX(event);
                this.dragRect = this.$el.getBoundingClientRect();
            },
            onDragMove: function (event) {
                if (this.beingDragged) {
                    event.preventDefault();
                    if (this.isRunning) {
                        this.isRunning = false;
                    }
                    this.dragPos = { x: getX(event), y: getY(event) };
                }
            },
            onDragEnd: function () {
                var _this = this;
                if (this.beingDragged) {
                    if (Math.abs(this.dragDelta) >= this.removalDistance) {
                        this.disableTransitions = true;
                        this.$nextTick(function () { return _this.closeToast(); });
                    }
                    else {
                        setTimeout(function () {
                            _this.beingDragged = false;
                            if (isDOMRect(_this.dragRect) &&
                                _this.pauseOnHover &&
                                _this.dragRect.bottom >= _this.dragPos.y &&
                                _this.dragPos.y >= _this.dragRect.top &&
                                _this.dragRect.left <= _this.dragPos.x &&
                                _this.dragPos.x <= _this.dragRect.right) {
                                _this.isRunning = false;
                            }
                            else {
                                _this.isRunning = true;
                            }
                        });
                    }
                }
            },
        },
    });

    /* script */
    const __vue_script__$7 = script$7;

    /* template */
    var __vue_render__$7 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "div",
        {
          class: _vm.classes,
          style: _vm.draggableStyle,
          on: {
            click: _vm.clickHandler,
            mouseenter: _vm.hoverPause,
            mouseleave: _vm.hoverPlay
          }
        },
        [
          _vm.icon
            ? _c("Icon", { attrs: { "custom-icon": _vm.icon, type: _vm.type } })
            : _vm._e(),
          _vm._v(" "),
          _c(
            "div",
            {
              class: _vm.bodyClasses,
              attrs: { role: _vm.accessibility.toastRole || "alert" }
            },
            [
              typeof _vm.content === "string"
                ? [_vm._v(_vm._s(_vm.content))]
                : _c(
                    _vm.getVueComponentFromObj(_vm.content),
                    _vm._g(
                      _vm._b(
                        {
                          tag: "component",
                          attrs: { "toast-id": _vm.id },
                          on: { "close-toast": _vm.closeToast }
                        },
                        "component",
                        _vm.content.props,
                        false
                      ),
                      _vm.content.listeners
                    )
                  )
            ],
            2
          ),
          _vm._v(" "),
          !!_vm.closeButton
            ? _c("CloseButton", {
                attrs: {
                  component: _vm.closeButton,
                  "class-names": _vm.closeButtonClassName,
                  "show-on-hover": _vm.showCloseButtonOnHover,
                  "aria-label": _vm.accessibility.closeButtonLabel
                },
                on: {
                  click: function($event) {
                    $event.stopPropagation();
                    return _vm.closeToast($event)
                  }
                }
              })
            : _vm._e(),
          _vm._v(" "),
          _vm.timeout
            ? _c("ProgressBar", {
                attrs: {
                  "is-running": _vm.isRunning,
                  "hide-progress-bar": _vm.hideProgressBar,
                  timeout: _vm.timeout
                },
                on: { "close-toast": _vm.timeoutHandler }
              })
            : _vm._e()
        ],
        1
      )
    };
    var __vue_staticRenderFns__$7 = [];
    __vue_render__$7._withStripped = true;

      /* style */
      const __vue_inject_styles__$7 = undefined;
      /* scoped */
      const __vue_scope_id__$7 = undefined;
      /* module identifier */
      const __vue_module_identifier__$7 = undefined;
      /* functional template */
      const __vue_is_functional_template__$7 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$7 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$7, staticRenderFns: __vue_staticRenderFns__$7 },
        __vue_inject_styles__$7,
        __vue_script__$7,
        __vue_scope_id__$7,
        __vue_is_functional_template__$7,
        __vue_module_identifier__$7,
        false,
        undefined,
        undefined,
        undefined
      );

    // Transition methods taken from https://github.com/BinarCode/vue2-transitions
    var script$8 = Vue__default['default'].extend({
        inheritAttrs: false,
        props: PROPS.TRANSITION,
        methods: {
            beforeEnter: function (el) {
                var enterDuration = typeof this.transitionDuration === "number"
                    ? this.transitionDuration
                    : this.transitionDuration.enter;
                el.style.animationDuration = enterDuration + "ms";
                el.style.animationFillMode = "both";
                this.$emit("before-enter", el);
            },
            afterEnter: function (el) {
                this.cleanUpStyles(el);
                this.$emit("after-enter", el);
            },
            afterLeave: function (el) {
                this.cleanUpStyles(el);
                this.$emit("after-leave", el);
            },
            beforeLeave: function (el) {
                var leaveDuration = typeof this.transitionDuration === "number"
                    ? this.transitionDuration
                    : this.transitionDuration.leave;
                el.style.animationDuration = leaveDuration + "ms";
                el.style.animationFillMode = "both";
                this.$emit("before-leave", el);
            },
            // eslint-disable-next-line @typescript-eslint/ban-types
            leave: function (el, done) {
                this.setAbsolutePosition(el);
                this.$emit("leave", el, done);
            },
            setAbsolutePosition: function (el) {
                el.style.left = el.offsetLeft + "px";
                el.style.top = el.offsetTop + "px";
                el.style.width = getComputedStyle(el).width;
                el.style.height = getComputedStyle(el).height;
                el.style.position = "absolute";
            },
            cleanUpStyles: function (el) {
                el.style.animationFillMode = "";
                el.style.animationDuration = "";
            },
        },
    });

    /* script */
    const __vue_script__$8 = script$8;

    /* template */
    var __vue_render__$8 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "transition-group",
        {
          attrs: {
            tag: "div",
            "enter-active-class": _vm.transition.enter
              ? _vm.transition.enter
              : _vm.transition + "-enter-active",
            "move-class": _vm.transition.move
              ? _vm.transition.move
              : _vm.transition + "-move",
            "leave-active-class": _vm.transition.leave
              ? _vm.transition.leave
              : _vm.transition + "-leave-active"
          },
          on: {
            leave: _vm.leave,
            "before-enter": _vm.beforeEnter,
            "before-leave": _vm.beforeLeave,
            "after-enter": _vm.afterEnter,
            "after-leave": _vm.afterLeave
          }
        },
        [_vm._t("default")],
        2
      )
    };
    var __vue_staticRenderFns__$8 = [];
    __vue_render__$8._withStripped = true;

      /* style */
      const __vue_inject_styles__$8 = undefined;
      /* scoped */
      const __vue_scope_id__$8 = undefined;
      /* module identifier */
      const __vue_module_identifier__$8 = undefined;
      /* functional template */
      const __vue_is_functional_template__$8 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$8 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$8, staticRenderFns: __vue_staticRenderFns__$8 },
        __vue_inject_styles__$8,
        __vue_script__$8,
        __vue_scope_id__$8,
        __vue_is_functional_template__$8,
        __vue_module_identifier__$8,
        false,
        undefined,
        undefined,
        undefined
      );

    var script$9 = Vue__default['default'].extend({
        components: { Toast: __vue_component__$7, VtTransition: __vue_component__$8 },
        props: Object.assign({}, PROPS.CORE_TOAST, PROPS.CONTAINER, PROPS.TRANSITION),
        data: function () {
            var data = {
                count: 0,
                positions: Object.values(exports.POSITION),
                toasts: {},
                defaults: {},
            };
            return data;
        },
        computed: {
            toastArray: function () {
                return Object.values(this.toasts);
            },
            filteredToasts: function () {
                return this.defaults.filterToasts(this.toastArray);
            },
        },
        beforeMount: function () {
            this.setup(this.container);
            var events = this.eventBus;
            events.$on(EVENTS.ADD, this.addToast);
            events.$on(EVENTS.CLEAR, this.clearToasts);
            events.$on(EVENTS.DISMISS, this.dismissToast);
            events.$on(EVENTS.UPDATE, this.updateToast);
            events.$on(EVENTS.UPDATE_DEFAULTS, this.updateDefaults);
            this.defaults = this.$props;
        },
        methods: {
            setup: function (container) {
                return __awaiter(this, void 0, void 0, function () {
                    return __generator(this, function (_a) {
                        switch (_a.label) {
                            case 0:
                                if (!isFunction(container)) return [3 /*break*/, 2];
                                return [4 /*yield*/, container()];
                            case 1:
                                container = _a.sent();
                                _a.label = 2;
                            case 2:
                                removeElement(this.$el);
                                container.appendChild(this.$el);
                                return [2 /*return*/];
                        }
                    });
                });
            },
            setToast: function (props) {
                if (!isUndefined(props.id)) {
                    this.$set(this.toasts, props.id, props);
                }
            },
            addToast: function (params) {
                var props = Object.assign({}, this.defaults, params.type &&
                    this.defaults.toastDefaults &&
                    this.defaults.toastDefaults[params.type], params);
                var toast = this.defaults.filterBeforeCreate(props, this.toastArray);
                toast && this.setToast(toast);
            },
            dismissToast: function (id) {
                var toast = this.toasts[id];
                if (!isUndefined(toast) && !isUndefined(toast.onClose)) {
                    toast.onClose();
                }
                this.$delete(this.toasts, id);
            },
            clearToasts: function () {
                var _this = this;
                Object.keys(this.toasts).forEach(function (id) {
                    _this.dismissToast(id);
                });
            },
            getPositionToasts: function (position) {
                var toasts = this.filteredToasts
                    .filter(function (toast) { return toast.position === position; })
                    .slice(0, this.defaults.maxToasts);
                return this.defaults.newestOnTop ? toasts.reverse() : toasts;
            },
            updateDefaults: function (update) {
                // Update container if changed
                if (!isUndefined(update.container)) {
                    this.setup(update.container);
                }
                this.defaults = Object.assign({}, this.defaults, update);
            },
            updateToast: function (_a) {
                var id = _a.id, options = _a.options, create = _a.create;
                if (this.toasts[id]) {
                    // If a timeout is defined, and is equal to the one before, change it
                    // a little so the progressBar is reset
                    if (options.timeout && options.timeout === this.toasts[id].timeout) {
                        options.timeout++;
                    }
                    this.setToast(Object.assign({}, this.toasts[id], options));
                }
                else if (create) {
                    this.addToast(Object.assign({}, { id: id }, options));
                }
            },
            getClasses: function (position) {
                var classes = [VT_NAMESPACE + "__container", position];
                return classes.concat(this.defaults.containerClassName);
            },
        },
    });

    /* script */
    const __vue_script__$9 = script$9;

    /* template */
    var __vue_render__$9 = function() {
      var _vm = this;
      var _h = _vm.$createElement;
      var _c = _vm._self._c || _h;
      return _c(
        "div",
        _vm._l(_vm.positions, function(pos) {
          return _c(
            "div",
            { key: pos },
            [
              _c(
                "VtTransition",
                {
                  class: _vm.getClasses(pos),
                  attrs: {
                    transition: _vm.defaults.transition,
                    "transition-duration": _vm.defaults.transitionDuration
                  }
                },
                _vm._l(_vm.getPositionToasts(pos), function(toast) {
                  return _c(
                    "Toast",
                    _vm._b({ key: toast.id }, "Toast", toast, false)
                  )
                }),
                1
              )
            ],
            1
          )
        }),
        0
      )
    };
    var __vue_staticRenderFns__$9 = [];
    __vue_render__$9._withStripped = true;

      /* style */
      const __vue_inject_styles__$9 = undefined;
      /* scoped */
      const __vue_scope_id__$9 = undefined;
      /* module identifier */
      const __vue_module_identifier__$9 = undefined;
      /* functional template */
      const __vue_is_functional_template__$9 = false;
      /* style inject */
      
      /* style inject SSR */
      
      /* style inject shadow dom */
      

      
      const __vue_component__$9 = /*#__PURE__*/normalizeComponent(
        { render: __vue_render__$9, staticRenderFns: __vue_staticRenderFns__$9 },
        __vue_inject_styles__$9,
        __vue_script__$9,
        __vue_scope_id__$9,
        __vue_is_functional_template__$9,
        __vue_module_identifier__$9,
        false,
        undefined,
        undefined,
        undefined
      );

    var ToastInterface = function (Vue, globalOptions, mountContainer) {
        if (globalOptions === void 0) { globalOptions = {}; }
        if (mountContainer === void 0) { mountContainer = true; }
        var events = (globalOptions.eventBus = globalOptions.eventBus || new Vue());
        if (mountContainer) {
            var containerComponent = new (Vue.extend(__vue_component__$9))({
                el: document.createElement("div"),
                propsData: globalOptions,
            });
            var onMounted = globalOptions.onMounted;
            if (!isUndefined(onMounted)) {
                onMounted(containerComponent);
            }
        }
        /**
         * Display a toast
         */
        var toast = function (content, options) {
            var props = Object.assign({}, { id: getId(), type: exports.TYPE.DEFAULT }, options, {
                content: content,
            });
            events.$emit(EVENTS.ADD, props);
            return props.id;
        };
        /**
         * Clear all toasts
         */
        toast.clear = function () { return events.$emit(EVENTS.CLEAR); };
        /**
         * Update Plugin Defaults
         */
        toast.updateDefaults = function (update) {
            events.$emit(EVENTS.UPDATE_DEFAULTS, update);
        };
        /**
         * Dismiss toast specified by an id
         */
        toast.dismiss = function (id) {
            events.$emit(EVENTS.DISMISS, id);
        };
        function updateToast(id, _a, create) {
            var content = _a.content, options = _a.options;
            if (create === void 0) { create = false; }
            events.$emit(EVENTS.UPDATE, {
                id: id,
                options: Object.assign({}, options, { content: content }),
                create: create,
            });
        }
        toast.update = updateToast;
        /**
         * Display a success toast
         */
        toast.success = function (content, options) { return toast(content, Object.assign({}, options, { type: exports.TYPE.SUCCESS })); };
        /**
         * Display an info toast
         */
        toast.info = function (content, options) { return toast(content, Object.assign({}, options, { type: exports.TYPE.INFO })); };
        /**
         * Display an error toast
         */
        toast.error = function (content, options) { return toast(content, Object.assign({}, options, { type: exports.TYPE.ERROR })); };
        /**
         * Display a warning toast
         */
        toast.warning = function (content, options) { return toast(content, Object.assign({}, options, { type: exports.TYPE.WARNING })); };
        return toast;
    };

    function createToastInterface(optionsOrEventBus, Vue) {
        if (Vue === void 0) { Vue = Vue__default['default']; }
        var isVueInstance = function (obj) {
            return obj instanceof Vue;
        };
        if (isVueInstance(optionsOrEventBus)) {
            return ToastInterface(Vue, { eventBus: optionsOrEventBus }, false);
        }
        return ToastInterface(Vue, optionsOrEventBus, true);
    }
    var VueToastificationPlugin = function (Vue, options) {
        var toast = createToastInterface(options, Vue);
        Vue.$toast = toast;
        Vue.prototype.$toast = toast;
    };

    exports.createToastInterface = createToastInterface;
    exports.default = VueToastificationPlugin;

    Object.defineProperty(exports, '__esModule', { value: true });

})));
//# sourceMappingURL=index.js.map
