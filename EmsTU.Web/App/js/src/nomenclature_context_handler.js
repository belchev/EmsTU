define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium',

    //src
    'src/nomenclature_context'
], function ($, ko, Corium, NomenclatureContext) {
    'use strict';

    var NomenclatureContextHandler = Corium.Class.extend({
        constructor: function (isChild, isMulti) {
            var self = this;

            self._isChild = isChild;
            self._isMulti = isMulti;
            self.init = self.init.bind(self);
        },
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var self = this,
                binding = valueAccessor(),
                childBindingContext = bindingContext.createChildContext(viewModel),
                nomenclatureContext =
                    new NomenclatureContext(
                        element,
                        binding.nomenclatureKey,
                        binding.idProperty,
                        binding.value,
                        self._isMulti,
                        self._isChild,
                        binding.parent
                    );

            ko.utils.extend(childBindingContext, {
                nomenclatureContext: nomenclatureContext
            });

            ko.applyBindingsToDescendants(childBindingContext, element);

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                nomenclatureContext.dispose();
            });

            return { controlsDescendantBindings: true };
        }
    });
    return NomenclatureContextHandler;
});
