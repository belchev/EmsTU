define([
    //libs
    'lodash',
    'knockout',

    //src
    'src/utils'
], function (_, ko, Utils) {
    'use strict';

    var ValidationUtils;

    ValidationUtils = {
        isValid: function (viewModel) {
            var hasErrors = false;

            Utils.Object.traverse(viewModel, function (value) {
                if (ko.isObservable(value)) {
                    hasErrors = hasErrors || (value.hasErrors && value.hasErrors.peek());
                    return hasErrors || value.peek();
                }
                return value;
            });

            return !hasErrors;
        }
    };
    return ValidationUtils;
});
