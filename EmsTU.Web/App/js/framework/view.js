define([
    //libs
    'knockout',
    'q',

    //framework
    'framework/class'
], function (ko, Q, Class) {
    'use strict';

    var View = Class.extend({
        constructor: function (initialValue, templateEngine) {
            var self = this;

            self.observable = self._createObservable();
            self.templateEngine = templateEngine;
            self._element = undefined;
            self._updates = undefined;
            //used to track the current value for the View
            self._value = initialValue;

            self.init = self.init.bind(self);
            self.dispose = self.dispose.bind(self);
            self._createObservable = self._createObservable.bind(self);
            self._proxy = self._proxy.bind(self);
            self._initVM = self._initVM.bind(self);
            self._disposeVM = self._disposeVM.bind(self);
            self._pushUpdate = self._pushUpdate.bind(self);
            self._clearUpdates = self._clearUpdates.bind(self);
            self._update = self._update.bind(self);

            //set references to all public view methods to the '_proxy' setter
            //as we'll use it as the return value for our newly constructed view
            //to allow it to be used directly for getting/setting
            self._proxy.init = self.init;
            self._proxy.dispose = self.dispose;
            self._proxy.observable = self.observable;
            self._proxy.templateEngine = self.templateEngine;
            return self._proxy;
        },
        init: function (element) {
            var self = this;

            if (self._element) {
                throw new Error('Already initializied');
            }

            self._element = element;
            self._updates = [];
            self._proxy(self._value);
        },
        dispose: function () {
            var self = this;

            self._disposeVM(self.observable());
            self.observable(undefined);
            self._element = undefined;
            self._updates = undefined;
        },
        _createObservable: function () {
            return ko.observable();
        },
        _proxy: function (vm) {
            var self = this;

            //invoked as getter
            if (!arguments.length) {
                return self._value;
            }

            //invoked as setter
            self._value = vm;
            self._clearUpdates();
            self._pushUpdate(function () {
                //the order should always be
                // - init
                // - change
                // - dispose
                var oldVM = self.observable();
                self._initVM(self._value);
                self.observable(self._value);
                self._disposeVM(oldVM);
            });
            return self._update();
        },
        _initVM: function (vm) {
            if (vm && typeof vm.init === 'function') {
                vm.init();
            }
        },
        _disposeVM: function (vm) {
            if (vm && typeof vm.dispose === 'function') {
                vm.dispose();
            }
        },
        _pushUpdate: function (update) {
            var self = this;

            if (self._updates) {
                self._updates.push(update);
            }
        },
        _clearUpdates: function () {
            var self = this;

            if (self._updates) {
                self._updates = [];
            }
        },
        _update: function () {
            var self = this;

            if (!self._element) {
                //not initialized
                return;
            }

            return Q.delay(0).then(function () {
                if (!self._element) {
                    //we have been disposed in the meantime
                    return;
                }

                self._updates.forEach(function (update) {
                    update();
                });
                self._updates = [];
            });
        }
    });
    return View;
});
