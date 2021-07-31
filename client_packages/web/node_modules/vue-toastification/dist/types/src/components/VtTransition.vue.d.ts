import Vue from "vue";
declare const _default: import("vue/types/vue").ExtendedVue<Vue, unknown, {
    beforeEnter(el: HTMLElement): void;
    afterEnter(el: HTMLElement): void;
    afterLeave(el: HTMLElement): void;
    beforeLeave(el: HTMLElement): void;
    leave(el: HTMLElement, done: Function): void;
    setAbsolutePosition(el: HTMLElement): void;
    cleanUpStyles(el: HTMLElement): void;
}, unknown, {
    transition: string | Record<"enter" | "leave" | "move", string>;
    transitionDuration: number | Record<"enter" | "leave", number>;
}>;
export default _default;
