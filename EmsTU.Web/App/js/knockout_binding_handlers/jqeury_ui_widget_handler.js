(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'knockout', 'jquery', 'jquery.ui.widget' ], factory);
    } else {
        // Browser globals
        root.JQueryUIWidgetHandler = factory(root.ko, root.jQuery);
    }
}(this, function (ko, $) {
    'use strict';

    function JQueryUIWidgetHandler() {
    }

    JQueryUIWidgetHandler.prototype.init = function (element, valueAccessor) {
        var value = valueAccessor(),
            widget = value.widget,
            options = value.options;

        if (widget) {
            $(element)[widget](options);
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element)[widget]('destroy');
            });
        }
    };

    return JQueryUIWidgetHandler;
}));