define([
    //libs
    'knockout',

    //framework
    'framework/view'
], function (ko, View) {
    'use strict';

    var ListView = View.extend({
        constructor: function (initialValue) {
            var self = this,
                proxy = View.prototype.constructor.call(self, initialValue);

            self.indexOf = self.indexOf.bind(self);
            self.push = self.push.bind(self);
            self.remove = self.remove.bind(self);
            self.splice = self.splice.bind(self);
            self._initVMs = self._initVMs.bind(self);
            self._disposeVMs = self._disposeVMs.bind(self);

            proxy.indexOf = self.indexOf;
            proxy.push = self.push;
            proxy.remove = self.remove;
            proxy.splice = self.splice;
            return proxy;
        },
        indexOf: function (value) {
            var self = this;

            return self._value.indexOf(value);
        },
        push: function (value) {
            var self = this;

            self._value.push(value);
            self._pushUpdate(function () {
                self._initVM(value);
                self.observable.push(value);
            });
            self._update();
        },
        remove: function (value) {
            var self = this;

            self._value = self._value.filter(function (arrayValue) {
                return arrayValue !== value;
            });
            self._pushUpdate(function () {
                var removed = self.observable.remove(value);
                self._disposeVM(removed);
            });
            self._update();
        },
        splice: function () {
            var self = this,
                args = Array.prototype.slice.call(arguments, 0),
                newElements = args.slice(2);

            Array.prototype.splice.apply(self._value, args);
            self._pushUpdate(function () {
                var removed;
                self._initVMs(newElements);
                removed = self.observable.splice.apply(self.observable, args);
                self._disposeVMs(removed);
            });
            self._update();
        },
        _createObservable: function () {
            return ko.observableArray();
        },
        _proxy: function (vmArray) {
            var self = this;

            //invoked as getter
            if (!arguments.length) {
                return self._value;
            }

            //invoked as setter
            vmArray = vmArray || [];
            self._value = vmArray;
            self._clearUpdates();
            self._pushUpdate(function () {
                var oldVMArray = self.observable();
                self._initVMs(self._value);
                //set a copy of the array as the observableArray is using it as its undelying value
                self.observable(self._value.slice(0));
                self._disposeVMs(oldVMArray);
            });
            self._update();
        },
        _initVMs: function (vms) {
            var self = this;

            if (vms && vms.length) {
                vms.forEach(function (vm) {
                    self._initVM(vm);
                });
            }
        },
        _disposeVMs: function (vms) {
            var self = this;

            if (vms && vms.length) {
                vms.forEach(function (vm) {
                    self._disposeVM(vm);
                });
            }
        }
    });
    return ListView;
});
