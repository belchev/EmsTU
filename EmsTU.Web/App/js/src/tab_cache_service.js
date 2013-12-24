define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function ($, ko, Corium) {
    'use strict';

    var TabCacheService = Corium.Class.extend({
        constructor: function (config, cacheTimeoutSeconds) {
            var self = this;

            self._config = $.extend({}, config);
            self._urls = {};
            self._loadedAt = {};
            self._cacheTimeoutSeconds = cacheTimeoutSeconds;
        },
        loadTab: function (key, defaultVal) {
            var self = this;

            return self._load(key, defaultVal);
        },
        saveTab: function (key, val)
        {
            var self = this;

            self._loadedAt[key] = new Date();
            self._urls[key] = val;

            //todo keep no more than 50-100 active records in _urls
        },
        _load: function (key, defaultVal) {
            var self = this;

            if (!self._loadedAt[key] ||
                ((new Date()) - self._loadedAt[key] > self._cacheTimeoutSeconds * 1000) ||
                !self._urls[key]
            ) {
                self._loadedAt[key] = new Date();
                self._urls[key] = defaultVal;
            }

            return self._urls[key];
        }
    });
    return TabCacheService;
});
