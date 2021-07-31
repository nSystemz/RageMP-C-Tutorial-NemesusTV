import Vue from "vue";
declare const _default: import("vue/types/vue").ExtendedVue<Vue, {
    dragRect: DOMRect | Record<string, unknown>;
    isRunning: boolean;
    disableTransitions: boolean;
    beingDragged: boolean;
    dragStart: number;
    dragPos: {
        x: number;
        y: number;
    };
}, {
    getVueComponentFromObj: (obj: import("../types").ToastContent) => import("../types").RenderableToastContent;
    closeToast(): void;
    clickHandler(): void;
    timeoutHandler(): void;
    hoverPause(): void;
    hoverPlay(): void;
    focusPause(): void;
    focusPlay(): void;
    focusSetup(): void;
    focusCleanup(): void;
    draggableSetup(): void;
    draggableCleanup(): void;
    onDragStart(event: TouchEvent | MouseEvent): void;
    onDragMove(event: TouchEvent | MouseEvent): void;
    onDragEnd(): void;
}, {
    classes: string[];
    bodyClasses: string[];
    draggableStyle: {
        transition?: string | undefined;
        opacity?: number | undefined;
        transform?: string | undefined;
    };
    dragDelta: number;
    removalDistance: number;
}, {
    position: import("../ts/constants").POSITION;
    draggable: boolean;
    draggablePercent: number;
    pauseOnFocusLoss: boolean;
    pauseOnHover: boolean;
    closeOnClick: boolean;
    timeout: number | false;
    toastClassName: string | string[];
    bodyClassName: string | string[];
    hideProgressBar: boolean;
    showCloseButtonOnHover: boolean;
    icon: string | boolean | import("vue").VueConstructor<Vue> | {
        iconTag?: "object" | "a" | "abbr" | "address" | "applet" | "area" | "article" | "aside" | "audio" | "b" | "base" | "basefont" | "bdi" | "bdo" | "blockquote" | "body" | "br" | "button" | "canvas" | "caption" | "cite" | "code" | "col" | "colgroup" | "data" | "datalist" | "dd" | "del" | "details" | "dfn" | "dialog" | "dir" | "div" | "dl" | "dt" | "em" | "embed" | "fieldset" | "figcaption" | "figure" | "font" | "footer" | "form" | "frame" | "frameset" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "head" | "header" | "hgroup" | "hr" | "html" | "i" | "iframe" | "img" | "input" | "ins" | "kbd" | "label" | "legend" | "li" | "link" | "main" | "map" | "mark" | "marquee" | "menu" | "meta" | "meter" | "nav" | "noscript" | "ol" | "optgroup" | "option" | "output" | "p" | "param" | "picture" | "pre" | "progress" | "q" | "rp" | "rt" | "ruby" | "s" | "samp" | "script" | "section" | "select" | "slot" | "small" | "source" | "span" | "strong" | "style" | "sub" | "summary" | "sup" | "table" | "tbody" | "td" | "template" | "textarea" | "tfoot" | "th" | "thead" | "time" | "title" | "tr" | "track" | "u" | "ul" | "var" | "video" | "wbr" | undefined;
        iconChildren?: string | undefined;
        iconClass?: string | undefined;
    } | import("vue").FunctionalComponentOptions<Record<string, any>, import("vue/types/options").PropsDefinition<Record<string, any>>> | import("vue").ComponentOptions<never, import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>, Record<string, any>> | JSX.Element;
    closeButton: false | "object" | import("vue").VueConstructor<Vue> | import("vue").FunctionalComponentOptions<Record<string, any>, import("vue/types/options").PropsDefinition<Record<string, any>>> | import("vue").ComponentOptions<never, import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>, Record<string, any>> | JSX.Element | "a" | "abbr" | "address" | "applet" | "area" | "article" | "aside" | "audio" | "b" | "base" | "basefont" | "bdi" | "bdo" | "blockquote" | "body" | "br" | "button" | "canvas" | "caption" | "cite" | "code" | "col" | "colgroup" | "data" | "datalist" | "dd" | "del" | "details" | "dfn" | "dialog" | "dir" | "div" | "dl" | "dt" | "em" | "embed" | "fieldset" | "figcaption" | "figure" | "font" | "footer" | "form" | "frame" | "frameset" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "head" | "header" | "hgroup" | "hr" | "html" | "i" | "iframe" | "img" | "input" | "ins" | "kbd" | "label" | "legend" | "li" | "link" | "main" | "map" | "mark" | "marquee" | "menu" | "meta" | "meter" | "nav" | "noscript" | "ol" | "optgroup" | "option" | "output" | "p" | "param" | "picture" | "pre" | "progress" | "q" | "rp" | "rt" | "ruby" | "s" | "samp" | "script" | "section" | "select" | "slot" | "small" | "source" | "span" | "strong" | "style" | "sub" | "summary" | "sup" | "table" | "tbody" | "td" | "template" | "textarea" | "tfoot" | "th" | "thead" | "time" | "title" | "tr" | "track" | "u" | "ul" | "var" | "video" | "wbr";
    closeButtonClassName: string | string[];
    accessibility: {
        toastRole?: string | undefined;
        closeButtonLabel?: string | undefined;
    };
    rtl: boolean;
    eventBus: import("vue/types/vue").CombinedVueInstance<Vue, object, object, object, Record<never, any>>;
    id: import("../types").ToastID;
    type: import("../ts/constants").TYPE;
    onClick: (closeToast: Function) => void;
    onClose: () => void;
    content: import("../types").ToastContent;
}>;
export default _default;
