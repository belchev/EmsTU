(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'knockout' ], factory);
    } else {
        // Browser globals
        root.ReadOnlyHandler = factory(root.ko);
    }
}(this, function (ko) {
    'use strict';

    function ReadOnlyHandler() {
    }

    ReadOnlyHandler.prototype.update = function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value) {
            element.setAttribute('readonly', 'readonly');
        }  else {
            element.removeAttribute('readonly');
        }
    };

    return ReadOnlyHandler;
}));