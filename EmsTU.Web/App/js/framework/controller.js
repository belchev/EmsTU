define([
    //framework
    'framework/class'
], function (Class) {
    'use strict';

    var Controller = Class.extend({
        constructor: function () {
        },
        unshiftAction: function (actionOrCallback, params, cancelationToken) {
            /* Adds an action in the beginning of the execution queue.
             * NOTE! This method should be called from within a queueed action
             * otherwise nothing will happen, 'unshiftAction' does not call AsyncQueue.process
             * on its own.
             */
            var self = this;

            self._controllerCoordinator.unshiftAction(actionOrCallback, params, cancelationToken);
        }
    });
    return Controller;
});
