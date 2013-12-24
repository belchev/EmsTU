define([
    //framework
    'framework/corium_instance',
    'framework/class',
    'framework/async_queue'
], function (Corium, Class, AsyncQueue) {
    'use strict';

    var ControllerCoordinator = Class.extend({
        constructor: function () {
            var self = this;

            self._controllers = undefined;
            self._actionQueue = undefined;
            self._cancelationToken = undefined;
        },
        init: function (controllers) {
            var self = this;

            self._controllers = controllers;
            self._actionQueue = new AsyncQueue();
            self._cancelationToken = { isCanceled: false };
        },
        dispose: function () {
            var self = this;

            self._controllers = undefined;
            self._actionQueue.cancel();
            self._actionQueue.clear();
            self._actionQueue = undefined;
            self._cancelationToken.isCanceled = true;
            self._cancelationToken = undefined;
        },
        executeAction: function (action, params) {
            var self = this;

            params = params || {};

            self._actionQueue.cancel();
            self._actionQueue.clear();
            self._cancelationToken.isCanceled = true;
            self._cancelationToken = { isCanceled: false };
            Corium.dialogs.hide(true);

            self._actionQueue
                .unshift(self._createExecution(action, params, self._cancelationToken, false));

            return self._actionQueue.process().then(undefined, function (error) {
                Corium.events.trigger('error.Corium', error);
            });
        },
        unshiftAction: function (actionOrCallback, params, cancelationToken) {
            /* Adds an action in the beginning of the execution queue.
             * NOTE! This method should be called from within a queueed action
             * otherwise nothing will happen, 'unshiftAction' does not call AsyncQueue.process
             * on its own.
             */
            var self = this;

            self._actionQueue
                .unshift(self._createExecution(actionOrCallback, params, cancelationToken, false));
        },
        _createExecution: function (action, params, cancelationToken, dependenciesExecuted) {
            var self = this;
            return function () {
                var splitAction,
                    controllerName,
                    actionName,
                    ControllerConstructor,
                    controllerInstance,
                    actionConfig,
                    i;

                if (typeof action === 'function') {
                    return action();
                }

                splitAction = action.split('#');
                controllerName = splitAction[0];
                actionName = splitAction[1];
                ControllerConstructor = self._controllers[controllerName];
                actionConfig = ControllerConstructor.prototype[actionName];

                if (!actionConfig.dependencies || dependenciesExecuted) {
                    controllerInstance = new ControllerConstructor();
                    controllerInstance._controllerCoordinator = self;
                    return actionConfig.action.call(controllerInstance, params, cancelationToken);
                }

                self._actionQueue
                    .unshift(self._createExecution(action, params, cancelationToken, true));
                for (i = actionConfig.dependencies.length - 1; i >= 0; i--) {
                    self._actionQueue
                        .unshift(
                            self._createExecution(
                                actionConfig.dependencies[i],
                                params,
                                cancelationToken,
                                false));
                }
                //returns nothing - just continue execution
            };
        }
    });
    return ControllerCoordinator;
});
