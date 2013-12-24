define([
    //libs
    'q',

    //framework
    'framework/class'
], function (Q, Class) {
    'use strict';

    var AsyncQueue = Class.extend({
        constructor: function () {
            var self = this;

            self._queue = [];
            self._cancelationToken = { isCanceled: false };
        },
        push: function (func) {
            var self = this;

            self._queue.push(func);
            return this;
        },
        unshift: function (func) {
            var self = this;

            self._queue.unshift(func);
            return this;
        },
        cancel: function () {
            var self = this;

            self._cancelationToken.isCanceled = true;
            self._cancelationToken = { isCanceled: false };
        },
        clear: function () {
            var self = this;

            self._queue.length = 0;
        },
        process: function () {
            var self = this,
                cancelationToken = self._cancelationToken;

            function processQueue() {
                var nextFunc,
                    promise;

                if (cancelationToken.isCanceled) {
                    return Q.resolve({ canceled: true });
                } else if (!self._queue.length) {
                    return Q.resolve({ finished: true });
                } else {
                    nextFunc = self._queue.shift();
                    promise = nextFunc();

                    return Q.resolve(promise).then(processQueue);
                }
            }

            return processQueue();
        }
    });
    return AsyncQueue;
});
