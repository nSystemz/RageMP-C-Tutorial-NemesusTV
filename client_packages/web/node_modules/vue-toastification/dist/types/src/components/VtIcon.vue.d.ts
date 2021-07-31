import Vue, { Component as VueComponent } from "vue";
import { RenderableToastContent } from "../types";
import { TYPE } from "../ts/constants";
declare const _default: import("vue/types/vue").ExtendedVue<Vue, unknown, {
    trimValue(value: unknown, empty?: string): string;
}, {
    customIconChildren: string;
    customIconClass: string;
    customIconTag: string;
    hasCustomIcon: boolean;
    component: RenderableToastContent;
    iconTypeComponent: VueComponent<import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>>;
    iconClasses: string[];
}, {
    type: TYPE;
    customIcon: string | boolean | import("vue").VueConstructor<Vue> | {
        iconTag?: "object" | "a" | "abbr" | "address" | "applet" | "area" | "article" | "aside" | "audio" | "b" | "base" | "basefont" | "bdi" | "bdo" | "blockquote" | "body" | "br" | "button" | "canvas" | "caption" | "cite" | "code" | "col" | "colgroup" | "data" | "datalist" | "dd" | "del" | "details" | "dfn" | "dialog" | "dir" | "div" | "dl" | "dt" | "em" | "embed" | "fieldset" | "figcaption" | "figure" | "font" | "footer" | "form" | "frame" | "frameset" | "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "head" | "header" | "hgroup" | "hr" | "html" | "i" | "iframe" | "img" | "input" | "ins" | "kbd" | "label" | "legend" | "li" | "link" | "main" | "map" | "mark" | "marquee" | "menu" | "meta" | "meter" | "nav" | "noscript" | "ol" | "optgroup" | "option" | "output" | "p" | "param" | "picture" | "pre" | "progress" | "q" | "rp" | "rt" | "ruby" | "s" | "samp" | "script" | "section" | "select" | "slot" | "small" | "source" | "span" | "strong" | "style" | "sub" | "summary" | "sup" | "table" | "tbody" | "td" | "template" | "textarea" | "tfoot" | "th" | "thead" | "time" | "title" | "tr" | "track" | "u" | "ul" | "var" | "video" | "wbr" | undefined;
        iconChildren?: string | undefined;
        iconClass?: string | undefined;
    } | import("vue").FunctionalComponentOptions<Record<string, any>, import("vue/types/options").PropsDefinition<Record<string, any>>> | import("vue").ComponentOptions<never, import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>, Record<string, any>> | JSX.Element;
}>;
export default _default;
