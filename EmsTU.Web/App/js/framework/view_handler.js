define([
    //libs
    'knockout',

    //framework
    'framework/class'
], function (ko, Class) {
    'use strict';

    var ViewHandler = Class.extend({
        constructor: function () {
            var self = this;

            function makeTemplateValueAccessor(view) {
                return function () {
                    var value = {
                            name: function (vm) { return vm ? vm.templateId : undefined; },
                            templateEngine: view.templateEngine
                        };

                    if (typeof view.remove === 'function') { //ListView
                        value.foreach = view.observable;
                    } else {
                        value.data = view.observable;
                        value['if'] = view.observable;
                    }

                    return value;
                };
            }

            self.init = function (element, valueAccessor) {
                var view = valueAccessor();

                view.init(element);
                ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(view));
                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    view.dispose();
                });
            };

            self.update = function (
                element,
                valueAccessor,
                allBindingsAccessor,
                viewModel,
                bindingContext
            ) {
                var view = valueAccessor();

                ko.bindingHandlers.template.update(
                    element,
                    makeTemplateValueAccessor(view),
                    allBindingsAccessor,
                    viewModel,
                    bindingContext);
            };
        }
    });
    return ViewHandler;
});
