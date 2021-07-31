import { PropOptions, PropType } from "vue";
import { CommonOptions, PluginOptions, ToastOptionsAndRequiredContent } from "../types";
import { TYPE } from "./constants";
import { RecordPropsDefinition } from "vue/types/options";
declare type CommonOptionsType = Required<CommonOptions>;
export declare type PluginOptionsType = Required<Omit<PluginOptions, keyof CommonOptionsType>>;
declare const _default: {
    CORE_TOAST: RecordPropsDefinition<Required<CommonOptions>>;
    TOAST: RecordPropsDefinition<Required<Pick<ToastOptionsAndRequiredContent, "id" | "type" | "onClick" | "onClose" | "content">>>;
    CONTAINER: RecordPropsDefinition<Required<Pick<PluginOptions, "transition" | "transitionDuration" | "container" | "newestOnTop" | "maxToasts" | "toastDefaults" | "filterBeforeCreate" | "filterToasts" | "containerClassName" | "onMounted">>>;
    PROGRESS_BAR: {
        timeout: PropOptions<number | false>;
        hideProgressBar: PropType<boolean>;
        isRunning: PropType<boolean>;
    };
    ICON: {
        type: PropOptions<TYPE>;
        customIcon: PropOptions<string | boolean | {
            iconTag?: "object" | "a" | "abbr" | "address" | "applet" | "area" | "article" | "aside" | "audio" | "b" | "base" | "basefont" | "bdi" | "bdo" | "blockquote" | "body" | "br" | "button" | "canvas" | "caption" | "cite" | "code" | "col" | "colgroup" | "data" | "datalist" | "dd" | "del" | "details" | "dfn" | "dialog" | "dir" | "div" | "dl" | "dt" | "em" | "embed" | "fieldset" | "figcaption" | "figure" | "font" | "footer" | "form" | "frame" | "frameset" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "head" | "header" | "hgroup" | "hr" | "html" | "i" | "iframe" | "img" | "input" | "ins" | "kbd" | "label" | "legend" | "li" | "link" | "main" | "map" | "mark" | "marquee" | "menu" | "meta" | "meter" | "nav" | "noscript" | "ol" | "optgroup" | "option" | "output" | "p" | "param" | "picture" | "pre" | "progress" | "q" | "rp" | "rt" | "ruby" | "s" | "samp" | "script" | "section" | "select" | "slot" | "small" | "source" | "span" | "strong" | "style" | "sub" | "summary" | "sup" | "table" | "tbody" | "td" | "template" | "textarea" | "tfoot" | "th" | "thead" | "time" | "title" | "tr" | "track" | "u" | "ul" | "var" | "video" | "wbr" | undefined;
            iconChildren?: string | undefined;
            iconClass?: string | undefined;
        } | import("vue").VueConstructor<import("vue").default> | import("vue").FunctionalComponentOptions<Record<string, any>, import("vue/types/options").PropsDefinition<Record<string, any>>> | import("vue").ComponentOptions<never, import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>, Record<string, any>> | JSX.Element>;
    };
    TRANSITION: {
        transition: PropOptions<string | Record<"enter" | "leave" | "move", string>>;
        transitionDuration: PropOptions<number | Record<"enter" | "leave", number>>;
    };
    CLOSE_BUTTON: {
        component: PropOptions<false | "object" | import("vue").VueConstructor<import("vue").default> | import("vue").FunctionalComponentOptions<Record<string, any>, import("vue/types/options").PropsDefinition<Record<string, any>>> | import("vue").ComponentOptions<never, import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>, Record<string, any>> | JSX.Element | "a" | "abbr" | "address" | "applet" | "area" | "article" | "aside" | "audio" | "b" | "base" | "basefont" | "bdi" | "bdo" | "blockquote" | "body" | "br" | "button" | "canvas" | "caption" | "cite" | "code" | "col" | "colgroup" | "data" | "datalist" | "dd" | "del" | "details" | "dfn" | "dialog" | "dir" | "div" | "dl" | "dt" | "em" | "embed" | "fieldset" | "figcaption" | "figure" | "font" | "footer" | "form" | "frame" | "frameset" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "head" | "header" | "hgroup" | "hr" | "html" | "i" | "iframe" | "img" | "input" | "ins" | "kbd" | "label" | "legend" | "li" | "link" | "main" | "map" | "mark" | "marquee" | "menu" | "meta" | "meter" | "nav" | "noscript" | "ol" | "optgroup" | "option" | "output" | "p" | "param" | "picture" | "pre" | "progress" | "q" | "rp" | "rt" | "ruby" | "s" | "samp" | "script" | "section" | "select" | "slot" | "small" | "source" | "span" | "strong" | "style" | "sub" | "summary" | "sup" | "table" | "tbody" | "td" | "template" | "textarea" | "tfoot" | "th" | "thead" | "time" | "title" | "tr" | "track" | "u" | "ul" | "var" | "video" | "wbr">;
        classNames: PropOptions<string | string[]>;
        showOnHover: PropType<boolean>;
        ariaLabel: PropOptions<string>;
    };
};
export default _default;
