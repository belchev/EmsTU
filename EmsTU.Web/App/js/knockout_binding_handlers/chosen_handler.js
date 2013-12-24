(function (root, factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([ 'jquery', 'knockout', 'chosen', 'element_handler_factory'], factory);
    } else {
        // Browser globals
        root.ChosenHandler = factory(root.jQuery, root.ko, root.Chosen, root.ElementHandlerFactory);
    }
}(this, function ($, ko, Chosen, ElementHandlerFactory) {
    'use strict';

    function ChosenElementHandler() {
        var self = this;

        self._element = undefined;
        self._$element = undefined;
        self._chosen = undefined;
        self._isMultiple = undefined;
        self._hasSingleDeselect = undefined;
        self._internalSelectedOptionsChange = undefined;
        self._setDisabledComputed = undefined;
        self._selectedOptionsBridge = undefined;
        self._setOptionsComputed = undefined;
        self._setSelectedOptionsComputed = undefined;

        self.init = $.proxy(self.init, self);
        self.update = $.proxy(self.update, self);
    }

    ChosenElementHandler.prototype.init = function (element, valueAccessor) {
        var self = this,
            binding = valueAccessor();

        if (ko.utils.tagNameLower(element) !== 'select') {
            throw new Error('"chosen" binding applies only to SELECT elements');
        }

        self._element = element;
        self._$element = $(element);

        //added to enable single deselect option, as chosen requires at least
        //one option in the select to enable it.
        //should be present in the element before we create chosen !!
        self._$element.html('<option></option>');
        self._chosen = new Chosen(self._$element, binding.chosenOptions);

        self._isMultiple = self._element.multiple;
        self._hasSingleDeselect =
                !self._isMultiple &&
                binding.chosenOptions &&
                binding.chosenOptions.allow_single_deselect;

        self._$element.bind('change', function () {
            self._internalSelectedOptionsChange = true;
            self._selectedOptionsBridge(self._getSelectedOptions());
            self._internalSelectedOptionsChange = false;
        });

        ko.utils.domNodeDisposal.addDisposeCallback(self._element, function () {
            self._setDisabledComputed.dispose();
            self._selectedOptionsBridge.dispose();
            self._setOptionsComputed.dispose();
            self._setSelectedOptionsComputed.dispose();
            if (self._$element.hasClass('chzn-done')) {
                self._$element.empty();
                self._$element.removeData();
                self._$element.unbind();
                self._$element.next().remove();
            }
        });
    };

    ChosenElementHandler.prototype.update = function (element, valueAccessor) {
        var self = this,
            binding = valueAccessor();

        self._bind(binding);
    };

    ChosenElementHandler.prototype._bind = function (binding) {
        var self = this;

        //TODO this is an internal knockout method
        //won't work with minified knockout
        ko.dependencyDetection.ignore(function () {
            self._bindDisabled(binding);
            self._bindOptions(binding);
            self._bindSelectedOptions(binding);
        });
    };

    ChosenElementHandler.prototype._bindDisabled = function (binding) {
        var self = this;

        if (self._setDisabledComputed) {
            self._setDisabledComputed.dispose();
        }
        self._setDisabledComputed = ko.computed({
            read: function () {
                self._setDisabled(ko.utils.unwrapObservable(binding.disabled));
            }
        });
    };

    ChosenElementHandler.prototype._bindOptions = function (binding) {
        var self = this;

        if (self._setOptionsComputed) {
            self._setOptionsComputed.dispose();
        }
        self._setOptionsComputed = ko.computed({
            read: function () {
                self._setOptions(
                    ko.utils.unwrapObservable(binding.options),
                    binding.optionsText,
                    binding.optionsValue
                );
            }
        });
    };

    ChosenElementHandler.prototype._bindSelectedOptions = function (binding) {
        var self = this,
            selectedValueBinding = binding.selectedOptions || binding.value,
            isValueBinding = selectedValueBinding === binding.value;

        //create a computed observable to unify the usage
        //of the 'selectedOptions' and 'value' bindings
        if (self._selectedOptionsBridge) {
            self._selectedOptionsBridge.dispose();
        }
        self._selectedOptionsBridge = ko.computed({
            read: function () {
                var value = ko.utils.unwrapObservable(selectedValueBinding);
                if (!value) {
                    return [];
                }
                return isValueBinding ? [value] : value;
            },
            write: function (value) {
                if (ko.isWriteableObservable(selectedValueBinding)) {
                    selectedValueBinding((isValueBinding && value) ? value[0] : value);
                }
            }
        });

        if (self._setSelectedOptionsComputed) {
            self._setSelectedOptionsComputed.dispose();
        }
        self._setSelectedOptionsComputed = ko.computed({
            read: function () {
                var selectedOptions = self._selectedOptionsBridge();

                if (!self._internalSelectedOptionsChange) {
                    self._setSelectedOptions(selectedOptions);
                }
            }
        });
    };

    ChosenElementHandler.prototype._setDisabled = function (isDisabled) {
        var self = this;

        if (isDisabled) {
            self._$element.attr('disabled', 'disabled');
        }  else {
            self._$element.removeAttr('disabled');
        }
        self._$element.trigger('liszt:updated');
    };

    ChosenElementHandler.prototype._setOptions = function (options, optionsText, optionsValue) {
        var self = this;

        //pass an empty array if 'options' is undefined as we need the caption option
        //being generated by knockout before the initialization of chosen
        //or else it will ignore the 'allow_single_deselect' option
        if (self._hasSingleDeselect) {
            options = options || [];
        }

        //use knockout's 'options' binding to generate the list of options
        ko.bindingHandlers.options.update.call(
            self,
            self._element,
            function () { return options; },
            function () {
                var allBindings = {
                    options: options,
                    optionsText: optionsText,
                    optionsValue: optionsValue
                };

                if (self._hasSingleDeselect) {
                    //use the 'optionsCaption' setting to generate an option
                    //with blank text for the 'allow_single_deselect'
                    allBindings.optionsCaption = ' ';
                }

                return allBindings;
            }
        );

        self._$element.trigger('liszt:updated');
    };

    ChosenElementHandler.prototype._getSelectedOptions = function () {
        var self = this,
            selectedOptions = [];

        ko.utils.arrayForEach(
            self._element.getElementsByTagName('option'),
            function (optionNode) {
                if (optionNode.selected) {
                    selectedOptions.push(ko.selectExtensions.readValue(optionNode));
                }
            }
        );
        return selectedOptions;
    };

    ChosenElementHandler.prototype._setSelectedOptions = function (selectedOptions) {
        var self = this,
            hasNoDeselect = !(self._isMultiple || self._hasSingleDeselect);

        ko.utils.arrayForEach(
            self._element.getElementsByTagName('option'),
            function (optionNode) {
                var optionValue = ko.selectExtensions.readValue(optionNode),
                    isSelected = ko.utils.arrayIndexOf(selectedOptions, optionValue) >= 0;

                optionNode.selected = isSelected;
            }
        );

        self._$element.trigger('liszt:updated');

        if (hasNoDeselect && selectedOptions.length === 0) {
            //trigger a change to update the selected option
            //to the first option in the selectedOptions array
            self._$element.trigger('change');
        }
    };

    function ChosenHandler() {
        return ElementHandlerFactory.prototype.constructor.call(
            this,
            'chosen',
            ChosenElementHandler);
    }

    ChosenHandler.prototype = ElementHandlerFactory.prototype;
    return ChosenHandler;
}));