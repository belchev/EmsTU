(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'jquery', 'knockout' ], factory);
    } else {
        // Browser globals
        root.ConvertedBindingHandler = factory(root.jQuery, root.ko);
    }
}(this, function ($, ko) {
    'use strict';

    function ConvertedBindingHandler(bindingKey, readConverterFunc, writeConverterFunc) {
        var self = this;

        self._bindingKey = bindingKey;
        self._readConverterFunc = readConverterFunc;
        self._writeConverterFunc = writeConverterFunc;

        self.init = $.proxy(self.init, self);
    }

    ConvertedBindingHandler.prototype.init =
        function (element, valueAccessor, allBindingsAccessor) {
            var self = this,
                value = valueAccessor(),
                convertedValueOptions,
                convertedValue,
                newAllBindings,
                newValueAccessor,
                newAllBindingsAccessor,
                triggeredRead;

            convertedValueOptions = {
                read: function () {
                    triggeredRead = true;
                    return self._readConverterFunc(ko.utils.unwrapObservable(value));
                },
                disposeWhenNodeIsRemoved: element
            };

            if (self._writeConverterFunc) {
                convertedValueOptions.write = function (newValue) {
                    var newConvertedValue = self._writeConverterFunc(newValue),
                        allBindings;

                    triggeredRead = false;
                    if (ko.isWriteableObservable(value)) {
                        value(newConvertedValue);
                    } else { //non-observable
                        allBindings = allBindingsAccessor();
                        if (allBindings._ko_property_writers &&
                            allBindings._ko_property_writers[self._bindingKey]
                        ) {
                            allBindings._ko_property_writers[self._bindingKey](newConvertedValue);
                        }
                    }

                    //Notify the subscribers even if the write to the underlying value doesn't.
                    //Since the write function is not always a bijection two different write values
                    //could be mapped to the same converted value and the 'value' observable
                    //will not notify its subscribers. Or the underlying value may not be an
                    //observable at all.
                    //NOTE: This case happens when two consecutive times invalid date strings are
                    //written, the second value would not trigger a read and the bound input will
                    //not be emptied.
                    if (!triggeredRead) {
                        convertedValue.notifySubscribers(
                            self._readConverterFunc(newConvertedValue));
                    }
                };
            }

            convertedValue = ko.computed(convertedValueOptions);

            newAllBindings = {};
            newAllBindings[self._bindingKey] = convertedValue;
            newValueAccessor = function () { return convertedValue; };
            newAllBindingsAccessor = function () { return newAllBindings; };

            if (ko.bindingHandlers[self._bindingKey].init) {
                ko.bindingHandlers[self._bindingKey].init.call(
                    self,
                    element,
                    newValueAccessor,
                    newAllBindingsAccessor);
            }

            ko.computed({
                read: function () {
                    if (ko.bindingHandlers[self._bindingKey].update) {
                        ko.bindingHandlers[self._bindingKey].update.call(
                            self,
                            element,
                            newValueAccessor,
                            newAllBindingsAccessor);
                    }
                },
                disposeWhenNodeIsRemoved: element,
                owner: self
            });

            return { controlsDescendantBindings: false };
        };

    return ConvertedBindingHandler;
}));