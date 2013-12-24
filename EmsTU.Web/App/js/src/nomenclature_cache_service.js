define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium',

    //src
    'src/utils'
], function ($, ko, Corium, Utils) {
    'use strict';

    var NomenclatureCacheService = Corium.Class.extend({
        constructor: function (config, cacheTimeoutSeconds) {
            var self = this;

            self._config = $.extend({}, config);
            self._loadingPromises = {};
            self._loadedAt = {};
            self._cacheTimeoutSeconds = cacheTimeoutSeconds;
        },
        loadNomenclature: function (nomenclatureKey) {
            var self = this;

            return self._load(nomenclatureKey, self._createUrl(nomenclatureKey));
        },
        loadSingleNomenclature: function (nomenclatureKey, id) {
            var self = this;

            return self._load(
                nomenclatureKey + ';' + id,
                self._createUrl(nomenclatureKey, id)
            );
        },
        loadChildNomenclature: function (nomenclatureKey, parentId) {
            var self = this;

            return self._load(
                nomenclatureKey + ';' + parentId,
                self._createUrl(nomenclatureKey, parentId)
            );
        },
        _createUrl: function (nomenclatureKey, parentId) {
            var self = this,
                format;

            if (self._config[nomenclatureKey]) {
                format = self._config[nomenclatureKey];
            } else {
                if (parentId) {
                    format = self._config.childDefault;
                } else {
                    format = self._config.parentDefault;
                }
                format = format.replace('{key}', nomenclatureKey);
            }

            return Utils.String.format(format, parentId);
        },
        _load: function (key, url) {
            var self = this;

            if (!self._loadedAt[key] ||
                ((new Date()) - self._loadedAt[key] > self._cacheTimeoutSeconds * 1000) ||
                !self._loadingPromises[key]
            ) {
                self._loadedAt[key] = new Date();
                self._loadingPromises[key] = $.getJSON(url).then(function (data) {
                    return data;
                }, function (jqXHR, textStatus, errorThrown) {
                    self._loadingPromises[key] = undefined;
                    self._loadedAt[key] = undefined;
                    throw errorThrown;
                });
            }

            return self._loadingPromises[key];
        }
    });
    return NomenclatureCacheService;
});
