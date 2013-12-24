(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'jquery', 'knockout' ], factory);
    } else {
        // Browser globals
        root.ElementHandlerFactory = factory(root.jQuery, root.ko);
    }
}(this, function ($, ko) {
    'use strict';

    function ElementHandlerFactory(key, handlerConstructor, handlerOptions) {
        var self = this;

        self._jqDataKey = 'elementHandler.' + key;
        self._handlerConstructor = handlerConstructor;
        self._handlerOptions = handlerOptions;

        self.init = $.proxy(self.init, self);
        self.update = $.proxy(self.update, self);
    }

    ElementHandlerFactory.prototype.init =
        function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var self = this,
                handler = $.data(element, self._jqDataKey),
                result;

            if (!handler) {
                handler = new self._handlerConstructor(self._handlerOptions);
                result = handler.init(
                    element,
                    valueAccessor,
                    allBindingsAccessor,
                    viewModel,
                    bindingContext);

                $.data(element, self._jqDataKey, handler);
                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    $.removeData(element, self._jqDataKey);
                });
            }

            return result;
        };

    ElementHandlerFactory.prototype.update =
        function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var self = this,
                handler = $.data(element, self._jqDataKey);

            return handler.update(
                element,
                valueAccessor,
                allBindingsAccessor,
                viewModel,
                bindingContext);
        };

    return ElementHandlerFactory;
}));