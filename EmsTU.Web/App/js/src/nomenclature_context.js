define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function ($, ko, Corium) {
    'use strict';

    var NomenclatureContext = Corium.Class.extend({
        constructor: function (
            element,
            nomenclatureKey,
            idProperty,
            value,
            isMulti,
            isChild,
            parentValueId
        ) {
            var self = this;

            self.loading = ko.observable(false);
            self.error = ko.observable(false);
            self.options = ko.observable([]);
            //used to set value to undefined until the options are loaded,
            //should always be set after 'options' to trigger a 'value' update
            //on a populated 'options'
            self._optionsLoaded = ko.observable(false);
            self._valueBridge = ko.computed({
                read: function () {
                    return ko.utils.unwrapObservable(value);
                },
                write: function (newValue) {
                    if (ko.isObservable(value)) {
                        value(newValue);
                    }
                }
            });
            self.value = ko.computed({
                read: function () {
                    if (!self._optionsLoaded()) {
                        return undefined;
                    }

                    return self._valueBridge();
                },
                write: function (value) {
                    self._valueBridge(value);
                }
            });
            self._nomenclatureKey = nomenclatureKey;//not expecting observables
            self._idProperty = idProperty;
            self._isMulti = isMulti;
            self._subscription = undefined;
            self._cancelationToken = { isCanceled: false };

            if (isChild) {
                if (ko.isObservable(parentValueId)) {
                    self._subscription = parentValueId.subscribe(function (newParentValueId) {
                        self.loading(false);
                        self._cancelationToken.isCanceled = true;
                        self._cancelationToken = { isCanceled: false };

                        if (newParentValueId) {
                            self._optionsLoaded(false);
                        }

                        self._valueBridge(undefined);
                        self.options([]);

                        if (newParentValueId) {
                            self._loadOptions(newParentValueId);
                        }
                    });

                    parentValueId = parentValueId.peek();
                    if (parentValueId) {
                        self._loadOptions(parentValueId);
                    }
                } else if (parentValueId) {
                    self._loadOptions(parentValueId);
                }
            } else {
                self._loadOptions();
            }
        },
        dispose: function () {
            var self = this;

            self.value.dispose();
            self._valueBridge.dispose();
            if (self._subscription) {
                self._subscription.dispose();
            }
        },
        _loadOptions: function (parentValueId) {
            var self = this,
                promise;

            self.loading(true);
            if (parentValueId) {
                promise = Corium.app.services.nomenclatureCache
                    .loadChildNomenclature(self._nomenclatureKey, parentValueId);
            } else {
                promise = Corium.app.services.nomenclatureCache
                    .loadNomenclature(self._nomenclatureKey);
            }

            promise.then(
                self._dataLoaded.bind(self, self._cancelationToken),
                self._dataError.bind(self, self._cancelationToken));
        },
        _dataLoaded: function (cancelationToken, data) {
            var self = this,
                currentValue = self._valueBridge.peek();

            if (cancelationToken.isCanceled) {
                return;
            }

            //leave only active or selected values
            data = data.filter(function (nomValue) {
                return nomValue.isActive ||
                    (self._isMulti ?
                        (currentValue && currentValue.indexOf(nomValue[self._idProperty]) !== -1) :
                        currentValue === nomValue[self._idProperty]
                    );
            });

            self.options(data);
            self._optionsLoaded(true);
            self.error(false);
            self.loading(false);
        },
        _dataError: function (cancelationToken, error) {
            var self = this;

            if (cancelationToken.isCanceled) {
                return;
            }

            self.options([]);
            self._optionsLoaded(true);
            self.error(error);
            self.loading(false);
        }
    });
    return NomenclatureContext;
});
