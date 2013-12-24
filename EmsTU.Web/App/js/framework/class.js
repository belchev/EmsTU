// Inheritance implementation copied from BackboneJS(as in version 0.9.2)
define([
    //libs
    'jquery'
], function ($) {
    'use strict';

    // Shared empty constructor function to aid in prototype-chain creation.
    var Ctor = function () {},
        inherits,
        extend,
        Class;

    // Helper function to correctly set up the prototype chain, for subclasses.
    // Similar to `goog.inherits`, but uses a hash of prototype properties and
    // class properties to be extended.
    inherits = function (parent, protoProps, staticProps) {
        var child;

        // The constructor function for the new subclass is either defined by you
        // (the "constructor" property in your `extend` definition), or defaulted
        // by us to simply call the parent's constructor.
        if (protoProps && protoProps.hasOwnProperty('constructor')) {
            child = protoProps.constructor;
        } else {
            child = function () { parent.apply(this, arguments); };
        }

        // Inherit class (static) properties from parent.
        $.extend(child, parent);

        // Set the prototype chain to inherit from `parent`, without calling
        // `parent`'s constructor function.
        Ctor.prototype = parent.prototype;
        child.prototype = new Ctor();

        // Add prototype properties (instance properties) to the subclass,
        // if supplied.
        if (protoProps) {
            $.extend(child.prototype, protoProps);
        }

        // Add static properties to the constructor function, if supplied.
        if (staticProps) {
            $.extend(child, staticProps);
        }

        // Correctly set child's `prototype.constructor`.
        child.prototype.constructor = child;

        // Set a convenience property in case the parent's prototype is needed later.
        child.__super__ = parent.prototype;

        return child;
    };

    extend = function (protoProps, classProps) {
        var child = inherits(this, protoProps, classProps);
        child.extend = extend;
        return child;
    };

    Class = function () {};
    Class.extend = extend;
    return Class;
});
