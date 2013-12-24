(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'knockout', 'jquery', 'q', 'spin' ], factory);
    } else {
        // Browser globals
        root.PromisedClickHandler = factory(root.ko, root.jQuery);
    }
}(this, function (ko, $, Q, Spinner) {
    'use strict';

    function PromisedClickHandler(spinnerOpts) {
        this._spinnerOpts = spinnerOpts;
        this.init = $.proxy(this.init, this);
    }

    PromisedClickHandler.prototype.init = function (
        element,
        valueAccessor,
        allBindingsAccessor,
        viewModel
    ) {
        var self = this;
        ko.utils.registerEventHandler(element, 'click', function (event) {
            var handlerFunction = valueAccessor(),
                argsForHandler,
                handlerReturnValue,
                promise,
                $element,
                spinner;

            if (!handlerFunction) {
                return;
            }

            try {
                // Take all the event args, and prefix with the viewmodel
                argsForHandler = ko.utils.makeArray(arguments);
                argsForHandler.unshift(viewModel);
                handlerReturnValue = handlerFunction.apply(viewModel, argsForHandler);
            } finally {
                event.preventDefault();
                promise = Q.resolve(handlerReturnValue);
                if (!promise.isFulfilled()) {
                    $element = $(element);
                    $element.prop('disabled', true);
                    $element.children().css('visibility', 'hidden');

                    spinner = new Spinner(self._spinnerOpts).spin();

                    $element.append(
                        $(spinner.el).css({
                            top: (0 - Math.floor($element.height() / 2)) + 'px',
                            left: Math.floor($element.width() / 2) + 'px'
                        }));

                    promise.fin(function () {
                        $element.prop('disabled', false);
                        spinner.stop();
                        $element.children().css('visibility', '');
                    });
                }
            }
        });
    };

    return PromisedClickHandler;
}));