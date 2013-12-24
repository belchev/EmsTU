(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'jquery', 'knockout', 'bootstrap' ], factory);
    } else {
        // Browser globals
        root.BootstrapHandler = factory(root.jQuery, root.ko, root.jQuery.fn);
    }
}(this, function ($, ko, Bootstrap) {
    'use strict';

    function BootstrapHandler() {
    }

    BootstrapHandler.prototype.init = function (element, valueAccessor) {
        var value = valueAccessor(),
            plugin = value.plugin,
            options = value.options,
            instance;

        if (plugin) {
            instance = new Bootstrap[plugin].Constructor(element, options);
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).off();
            });
        }
    };

    return BootstrapHandler;
}));