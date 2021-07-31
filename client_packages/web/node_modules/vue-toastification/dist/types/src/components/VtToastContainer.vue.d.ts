import Vue from "vue";
import { POSITION } from "../ts/constants";
import { PluginOptionsType } from "../ts/propValidators";
import { PluginOptions, ToastID, ToastOptionsAndContent, ToastOptionsAndRequiredContent } from "../types";
declare const _default: import("vue/types/vue").ExtendedVue<Vue, {
    count: number;
    positions: POSITION[];
    toasts: {
        [toastId: string]: ToastOptionsAndRequiredContent;
        [toastId: number]: ToastOptionsAndRequiredContent;
    };
    defaults: PluginOptionsType;
}, {
    setup(container: PluginOptionsType["container"]): Promise<void>;
    setToast(props: ToastOptionsAndRequiredContent): void;
    addToast(params: ToastOptionsAndRequiredContent): void;
    dismissToast(id: ToastID): void;
    clearToasts(): void;
    getPositionToasts(position: POSITION): ToastOptionsAndRequiredContent[];
    updateDefaults(update: PluginOptions): void;
    updateToast({ id, options, create, }: {
        id: ToastID;
        options: ToastOptionsAndContent;
        create: boolean;
    }): void;
    getClasses(position: POSITION): string[];
}, {
    toastArray: ToastOptionsAndRequiredContent[];
    filteredToasts: ToastOptionsAndRequiredContent[];
}, {
    position: POSITION;
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
    container: HTMLElement | (() => HTMLElement | Promise<HTMLElement>);
    newestOnTop: boolean;
    maxToasts: number;
    transition: string | Record<"enter" | "leave" | "move", string>;
    transitionDuration: number | Record<"enter" | "leave", number>;
    toastDefaults: Partial<Record<import("../ts/constants").TYPE, import("../types").ToastOptions>>;
    filterBeforeCreate: (toast: ToastOptionsAndRequiredContent, toasts: ToastOptionsAndRequiredContent[]) => false | ToastOptionsAndRequiredContent;
    filterToasts: (toasts: ToastOptionsAndRequiredContent[]) => ToastOptionsAndRequiredContent[];
    containerClassName: string | string[];
    onMounted: (containerComponent: Record<never, unknown> & Vue) => void;
}>;
export default _default;
