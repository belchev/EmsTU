define([
    //libs
    'lodash',
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function (_, $, ko, Corium) {
    'use strict';

    var ValidationExtenders = Corium.Class.extend({
        constructor: function () {
        },
        init: function () {
            var self = this;

            self._initRules();
            self._createExtenders();
        },
        _initRules: function () {
            var self = this;

            function getValue(obj) {
                if (ko.isObservable(obj)) {
                    return obj.peek();
                }
                return obj;
            }
            function isEmptyVal(val) {
                if (val === undefined) {
                    return true;
                }
                if (val === null) {
                    return true;
                }
                if (val === "") {
                    return true;
                }
            }

            self.rules = {};
            self.rules.required = {
                validator: function (val, required) {
                    var stringTrimRegEx = /^\s+|\s+$/g,
                        testVal;

                    if (val === undefined || val === null) {
                        return !required;
                    }

                    testVal = val;
                    if (typeof (val) === "string") {
                        testVal = val.replace(stringTrimRegEx, '');
                    }

                    if (!required) {// if they passed: { required: false }, then don't require this
                        return true;
                    }

                    return ((testVal + '').length > 0);
                },
                message: 'Полето е задължително.'
            };

            self.rules.min = {
                validator: function (val, min) {
                    return isEmptyVal(val) || val >= min;
                },
                message: 'Въведете стойност по-голяма или равна на {0}.'
            };

            self.rules.max = {
                validator: function (val, max) {
                    return isEmptyVal(val) || val <= max;
                },
                message: 'Въведете стойност по-малка или равна на {0}.'
            };

            self.rules.minLength = {
                validator: function (val, minLength) {
                    return isEmptyVal(val) || val.length >= minLength;
                },
                message: 'Въведете най-малко {0} символа.'
            };

            self.rules.maxLength = {
                validator: function (val, maxLength) {
                    return isEmptyVal(val) || val.length <= maxLength;
                },
                message: 'Въведете най-много {0} символа.'
            };

            self.rules.pattern = {
                validator: function (val, regex) {
                    return isEmptyVal(val) || val.toString().match(regex) !== null;
                },
                message: 'Please check this value.'
            };

            self.rules.step = {
                validator: function (val, step) {

                    // in order to handle steps of .1 & .01 etc.. Modulus won't work
                    // if the value is a decimal, so we have to correct for that
                    return isEmptyVal(val) || (val * 100) % (step * 100) === 0;
                },
                message: 'The value must increment by {0}'
            };

            self.rules.email = {
                validator: function (val, validate) {
                    /*jshint maxlen: 1000*/
                    if (!validate) { return true; }

                    //I think an empty email address is also a valid entry
                    //if one want's to enforce entry it should be done with 'required: true'
                    return isEmptyVal(val) || (
                        // jquery validate regex - thanks Scott Gonzalez
                        validate && /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(val)
                    );
                },
                message: 'Въведете коректен имейл адрес.'
            };

            self.rules.date = {
                validator: function (value, validate) {
                    if (!validate) { return true; }
                    return isEmptyVal(value) || (validate && !/Invalid|NaN/.test(new Date(value)));
                },
                message: 'Please enter a proper date'
            };

            self.rules.dateISO = {
                validator: function (value, validate) {
                    if (!validate) { return true; }
                    return isEmptyVal(value) || (validate && /^\d{4}[\/\-]\d{1,2}[\/\-]\d{1,2}$/.test(value));
                },
                message: 'Please enter a proper date'
            };

            self.rules.number = {
                validator: function (value, validate) {
                    if (!validate) { return true; }
                    return isEmptyVal(value) || (validate && /^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(value));
                },
                message: 'Въведете число.'
            };

            self.rules.digit = {
                validator: function (value, validate) {
                    if (!validate) { return true; }
                    return isEmptyVal(value) || (validate && /^\d+$/.test(value));
                },
                message: 'Въведете цифри.'
            };

            self.rules.equal = {
                validator: function (val, params) {
                    var otherValue = params;
                    return val === getValue(otherValue);
                },
                message: 'Стойностите трябва да са равни.'
            };

            self.rules.notEqual = {
                validator: function (val, params) {
                    var otherValue = params;
                    return val !== getValue(otherValue);
                },
                message: 'Моля, изберете друга стойност.'
            };
        },
        _createExtenders: function () {
            var self = this;

            _.forOwn(self.rules, function (rule, ruleName) {
                ko.extenders[ruleName] = function(target, options) {
                    if (!target.errors) {
                        target.errors = ko.observableArray();
                    }

                    target.hasErrors = ko.computed(function () {
                        return target.errors().length > 0;
                    });
                    target.validationMessage = ko.computed(function () {
                        return target.errors().reduce(function (res, err, i) {
                            if (i) {
                                res += '\n';
                            }
                            res += err.message;
                            return res;
                        }, "");
                    });

                    function validate(newValue) {
                        target.errors.remove(function (err) {
                            return err.ruleName === ruleName;
                        });

                        if (!rule.validator(newValue, options)) {
                            target.errors.push({ ruleName: ruleName, message: rule.message });
                        }
                    }

                    validate(target.peek());
                    target.subscribe(validate);

                    return target;
                };
            });
        }
    });
    return ValidationExtenders;
});
