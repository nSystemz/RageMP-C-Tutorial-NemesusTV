import { ToastContent, RenderableToastContent } from "../types";
declare const isFunction: (value: unknown) => value is Function;
declare const isString: (value: unknown) => value is string;
declare const isNonEmptyString: (value: unknown) => value is string;
declare const isUndefined: (value: unknown) => value is undefined;
declare const isToastContent: (obj: unknown) => obj is ToastContent;
declare const isDOMRect: (obj: unknown) => obj is DOMRect;
declare const hasProp: <O extends unknown, K extends string | number | symbol>(obj: O, propKey: K) => obj is O & { [key in K]: unknown; };
/**
 * ID generator
 */
declare const getId: () => number;
declare function getX(event: MouseEvent | TouchEvent): number;
declare function getY(event: MouseEvent | TouchEvent): number;
declare const removeElement: (el: Element) => void;
declare const getVueComponentFromObj: (obj: ToastContent) => RenderableToastContent;
export { getId, getX, getY, removeElement, isString, isNonEmptyString, isToastContent, getVueComponentFromObj, hasProp, isUndefined, isDOMRect, isFunction, };
