import _Vue from "vue";
import { VueConstructor } from "vue/types";
import { ToastContent, ToastOptions, ToastID, PluginOptions } from "../types";
import { TYPE } from "./constants";
declare const ToastInterface: (Vue: VueConstructor, globalOptions?: PluginOptions, mountContainer?: boolean) => {
    (content: ToastContent, options?: ToastOptions | undefined): ToastID;
    /**
     * Clear all toasts
     */
    clear(): any;
    /**
     * Update Plugin Defaults
     */
    updateDefaults(update: PluginOptions): void;
    /**
     * Dismiss toast specified by an id
     */
    dismiss(id: ToastID): void;
    update: {
        (id: ToastID, { content, options }: {
            content?: string | VueConstructor<_Vue> | import("vue").FunctionalComponentOptions<Record<string, any>, import("vue/types/options").PropsDefinition<Record<string, any>>> | import("vue").ComponentOptions<never, import("vue/types/options").DefaultData<never>, import("vue/types/options").DefaultMethods<never>, import("vue/types/options").DefaultComputed, Record<string, any>, Record<string, any>> | JSX.Element | import("../types").ToastComponent | undefined;
            options?: ToastOptions | undefined;
        }, create?: false | undefined): void;
        (id: ToastID, { content, options }: {
            content: ToastContent;
            options?: ToastOptions | undefined;
        }, create?: true | undefined): void;
    };
    /**
     * Display a success toast
     */
    success(content: ToastContent, options?: (ToastOptions & {
        type?: TYPE.SUCCESS | undefined;
    }) | undefined): ToastID;
    /**
     * Display an info toast
     */
    info(content: ToastContent, options?: (ToastOptions & {
        type?: TYPE.INFO | undefined;
    }) | undefined): ToastID;
    /**
     * Display an error toast
     */
    error(content: ToastContent, options?: (ToastOptions & {
        type?: TYPE.ERROR | undefined;
    }) | undefined): ToastID;
    /**
     * Display a warning toast
     */
    warning(content: ToastContent, options?: (ToastOptions & {
        type?: TYPE.WARNING | undefined;
    }) | undefined): ToastID;
};
export default ToastInterface;
