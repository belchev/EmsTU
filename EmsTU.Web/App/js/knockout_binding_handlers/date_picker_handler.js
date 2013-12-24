(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['jquery', 'knockout', 'datepicker', 'element_handler_factory'], factory);
    } else {
        // Browser globals
        root.DatePickerHandler =
            factory(
                root.jQuery,
                root.ko,
                root.jQuery.fn.datepicker.Constructor,
                root.ElementHandlerFactory);
    }
}(this, function ($, ko, Datepicker, ElementHandlerFactory) {
    'use strict';

    function DatePickerElementHandler(options) {
        var self = this;

        self._format = options.format;
        self._readConverterFunc = options.readConverterFunc;
        self._writeConverterFunc = options.writeConverterFunc;
        self._$element = undefined;
        self._picker = undefined;
        self._dateBridge = undefined;
        self._setDateComputed = undefined;
        self._setDisabledComputed = undefined;

        self.init = $.proxy(self.init, self);
        self.update = $.proxy(self.update, self);
    }

    DatePickerElementHandler.prototype.init = function (element, valueAccessor) {
        var self = this,
            binding = valueAccessor();

        //not expecting observables
        if (binding.options) {
            binding.options.format = binding.options.format || self._format;
        }

        self._$element = $(element);
        self._picker = new Datepicker(self._$element, binding.options);

        self._pickerDateChanged = self._pickerDateChanged.bind(self);
        self._$element.on('changeDate', undefined, undefined, self._pickerDateChanged);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            self._dateBridge.dispose();
            self._setDateComputed.dispose();
            self._setDisabledComputed.dispose();

            self._$element.off('changeDate', undefined, self._pickerDateChanged);
            self._picker.remove();
        });
    };

    DatePickerElementHandler.prototype.update = function (element, valueAccessor) {
        var self = this,
            binding = valueAccessor();

        self._bind(binding);
    };

    DatePickerElementHandler.prototype._bind = function (binding) {
        var self = this;

        //TODO this is an internal knockout method
        //won't work with minified knockout
        ko.dependencyDetection.ignore(function () {
            if (self._dateBridge) {
                self._dateBridge.dispose();
            }
            self._dateBridge = ko.computed({
                read: function () {
                    var date = ko.utils.unwrapObservable(binding.date);
                    if (self._readConverterFunc) {
                        date = self._readConverterFunc(date);
                    }
                    return date;
                },
                write: function (date) {
                    if (ko.isWriteableObservable(binding.date)) {
                        if (self._writeConverterFunc) {
                            date = self._writeConverterFunc(date);
                        }
                        binding.date(date);
                    }
                }
            });

            if (self._setDateComputed) {
                self._setDateComputed.dispose();
            }
            self._setDateComputed = ko.computed({
                read: function () {
                    var date = self._dateBridge();
                    if (date) {
                        self._picker.setDate(date);
                    } else {
                        //the 'bootstrap-datepicker' does not support deselection
                        //so set the currentdate instead
                        self._picker.setDate(new Date());
                    }
                }
            });

            if (self._setDisabledComputed) {
                self._setDisabledComputed.dispose();
            }
            self._setDisabledComputed = ko.computed({
                read: function () {
                    if (ko.utils.unwrapObservable(binding.disabled)) {
                        self._picker._detachEvents();
                    } else {
                        self._picker._attachEvents();
                    }
                }
            });
        });
    };

    DatePickerElementHandler.prototype._pickerDateChanged = function (ev) {
        var self = this,
            //move the date to the local time zone
            //the picker uses UTC for its internal representation
            date = new Date(ev.date.getTime() + (ev.date.getTimezoneOffset() * 60000));

        self._dateBridge(date);
        self._picker.hide();
    };

    function DatePickerHandler(options) {
        return ElementHandlerFactory.prototype.constructor.call(
            this,
            'datePicker',
            DatePickerElementHandler,
            options);
    }

    DatePickerHandler.prototype = ElementHandlerFactory.prototype;
    return DatePickerHandler;
}));