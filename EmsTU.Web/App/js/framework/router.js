define([
    //libs
    'lodash',
    'crossroads',

    //framework
    'framework/class'
], function (_, crossroads, Class) {
    /*global decodeURIComponent, encodeURIComponent*/
    'use strict';

    var Router = Class.extend({
        constructor: function () {
            var self = this;

            self._routes = undefined;
            self._crossroads = undefined;
            self._match = undefined;
        },
        init: function (patterns) {
            var self = this;

            self._routes = {};
            self._crossroads = crossroads.create();

            _.forEach(patterns, function (pattern) {
                self._routes[pattern] = self._crossroads.addRoute(pattern);
            });

            self._crossroads.ignoreState = true;
            self._crossroads.routed.add(self._routeMatched, self);
            self._crossroads.bypassed.add(self._routeNotMatched, self);
        },
        dispose: function () {
            var self = this;

            self._routes = undefined;
            self._crossroads.routed.removeAll();
            self._crossroads.bypassed.removeAll();
            self._crossroads.removeAllRoutes();
            self._crossroads = undefined;
        },
        match: function (url) {
            var self = this,
                result;

            self._crossroads.parse(url);

            if (self._match) {
                result = self._match;
            } else if (self._match === false) {
                result = undefined;
            } else {
                throw new Error("Internal router error!");
            }

            self._match = undefined;
            return result;
        },
        interpolate: function (pattern, params) {
            var self = this,
                route = self._routes[pattern],
                url;

            params = _.cloneDeep(params || {}, function(value) {
                return _.isString(value) ? encodeURIComponent(value) : undefined;
            });

            try {
                url = route.interpolate(params);
            } catch (e) {
                //Route.interpolate throws an error if unsuccessful
                url = undefined;
            }

            return url;
        },
        _routeMatched: function (url, data) {
            var self = this,
                params;

            params = _.reduce(data.params, function (paramsObj, paramValue, index) {
                var paramName = data.route._paramsIds[index];

                //remove the leading '?' in query params
                if (paramName[0] === '?') {
                    paramName = paramName.substring(1);
                }

                paramsObj[paramName] = paramValue;
                return paramsObj;
            }, {});

            params = _.cloneDeep(params, function(value) {
                return _.isString(value) ? decodeURIComponent(value) : undefined;
            });

            self._match = {
                pattern: data.route._pattern,
                params: params
            };
        },
        _routeNotMatched: function () {
            var self = this;

            self._match = false;
        }
    });
    return Router;
});
