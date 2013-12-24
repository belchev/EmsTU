define([
    //libs
    'lodash',

    //framework
    'framework/corium_instance',
    'framework/class',
    'framework/router',
    'framework/navigator'
], function (_, Corium, Class, Router, Navigator) {
    'use strict';

    var NavigationService = Class.extend({
        constructor: function () {
            var self = this;

            self._router = undefined;
            self._rescueAction = undefined;
            self._actionPatterns = undefined;
            self._routes = undefined;
            self._navigator = undefined;
            self._rootAction = undefined;
            self._rootActionParams = undefined;
        },
        start: function () {
            var self = this;
            self._navigator.start();
        },
        init: function (config) {
            var self = this;

            self._initRouter(config.routes, config.rescueAction);
            self._initNavigator(config.rootAction, config.rootActionParams);
        },
        dispose: function () {
            var self = this;

            self._disposeRouter();
            self._disposeNavigator();
        },
        navigateAction: function (action, params, replace) {
            var self = this,
                url = self._interpolateUrl(action, params);

            if (!url) {
                throw new Error("Navigation failed! Action has no routes defined.");
            }

            self._navigator.setUrl(url, replace, true);

            return self._navigateAction(action, params, url);
        },
        navigateRootAction: function (replace) {
            var self = this;
            self.navigateAction(self._rootAction, self._rootActionParams, replace);
        },
        setHref: function (action, params) {
            var self = this,
                url = self._interpolateUrl(action, params);

            if (url) {
                self._navigator.setUrl(url, false, true);
            }
        },
        interpolateHref: function (action, params) {
            var self = this,
                url = self._interpolateUrl(action, params);

            if (!url) {
                throw new Error("Interpolation failed! Action has no routes defined.");
            }

            return self._navigator.toHref(url);
        },
        _initRouter: function (routes, rescueAction) {
            var self = this;

            self._router = new Router();
            self._rescueAction = rescueAction;
            self._actionPatterns = {};
            self._routes = {};

            _.forEach(routes, function (route) {
                self._actionPatterns[route.action] = self._actionPatterns[route.action] || [];
                self._actionPatterns[route.action].push(route.pattern);

                route.params = self._normalizeParams(route.params);
                self._routes[route.pattern] = route;
            });

            self._router.init(_.keys(self._routes));
        },
        _disposeRouter: function () {
            var self = this;
            self._router.dispose();
            self._router = undefined;
            self._rescueAction = undefined;
            self._actionPatterns = undefined;
            self._routes = undefined;
        },
        _matchUrl: function (url) {
            var self = this,
                match = self._router.match(url),
                route,
                action,
                params;

            if (match) {
                route = self._routes[match.pattern];
                action = route.action;
                params = _.extend(match.params, route.params || {});
            } else {
                action = self._rescueAction;
                params = {};
            }

            self._navigateAction(action, params, url);
        },
        _navigateAction: function (action, params, url) {
            var self = this;

            return Corium.controllerCoordinator.executeAction(action, params)
                .then(function (args) {
                    if (args.finished) {
                        Corium.events.trigger('navigated.NavigationService', {
                            action: action,
                            params: params,
                            href: self._navigator.toHref(url)
                        });
                    }
                });
        },
        _interpolateUrl: function (action, params) {
            var self = this,
                patterns = self._actionPatterns[action],
                pattern,
                route,
                url,
                i;

            params = self._normalizeParams(params);

            for (i = 0; i < patterns.length; i++) {
                pattern = patterns[i];
                route = self._routes[pattern];

                //params should be a superset of route.params
                //to consider the route for interpolation
                if (!_.isEqual(params, _.merge({}, params, route.params))) {
                    continue;
                }

                url = self._router.interpolate(patterns[i], params);
                if (url) {
                    break;
                }
            }

            return url;
        },
        _normalizeParams: function (params) {
            function deepOmit(sourceObj, callback, thisArg) {
                var destObj, i, shouldOmit, newValue;

                if (_.isUndefined(sourceObj)) {
                    return undefined;
                }

                callback = thisArg ? _.bind(callback, thisArg) : callback;

                if (_.isPlainObject(sourceObj)) {
                    destObj = {};
                    _.forOwn(sourceObj, function(value, key) {
                        newValue = deepOmit(value, callback);
                        shouldOmit = callback(newValue, key);
                        if (!shouldOmit) {
                            destObj[key] = newValue;
                        }
                    });
                } else if (_.isArray(sourceObj)) {
                    destObj = [];
                    for (i = 0; i <sourceObj.length; i++) {
                        newValue = deepOmit(sourceObj[i], callback);
                        shouldOmit = callback(newValue, i);
                        if (!shouldOmit) {
                            destObj.push(newValue);
                        }
                    }
                } else {
                    return sourceObj;
                }

                return destObj;
            }

            params = deepOmit(params, function (value) {
                return value === undefined || (_.isPlainObject(value) && !_.keys(value).length);
            });

            params = params || {};

            return params;
        },
        _initNavigator: function (rootAction, rootActionParams) {
            var self = this;

            self._rootAction = rootAction;
            self._rootActionParams = rootActionParams;
            self._navigator = new Navigator();
            self._navigator.init(self._urlInitialized, self, self._urlChanged, self);
        },
        _disposeNavigator: function () {
            var self = this;

            self._navigator.dispose();
            self._navigator = undefined;
            self._rootAction = undefined;
            self._rootActionParams = undefined;
        },
        _urlInitialized: function (initialUrl) {
            var self = this;

            if (!initialUrl) {
                self.navigateRootAction(true);
            } else {
                self._matchUrl(initialUrl);
            }
        },
        _urlChanged: function (newUrl) {
            var self = this;

            self._matchUrl(newUrl);
        }
    });
    return NavigationService;
});
