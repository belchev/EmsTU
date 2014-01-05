define([
    //framework
    'framework/corium',

    //src
    'src/utils'
], function (Corium, Utils) {
    'use strict';

    var NomenclatureRepository = Corium.Repository.extend({
        constructor: function () {
        },
        getNomTypes: function () {
            var self = this;
            return self.get('api/reg/noms/system');
        },
        getNomTypeById: function (rNomTypeId) {
            var self = this,
                nomUrl = 'api/reg/noms/system/' + rNomTypeId;
            return self.get(nomUrl);
        },
        getNomTypeValues: function (rNomTypeId) {
            var self = this,
                nomValuesUrl = 'api/reg/noms/system/' + rNomTypeId + '/values';
            return self.get(nomValuesUrl);
        },
        getNomTypeValueById: function (rNomTypeId, rNomTypeValueId) {
            var self = this,
                nomValuesUrl = 'api/reg/noms/system/' + rNomTypeId + '/values/' + rNomTypeValueId;
            return self.get(nomValuesUrl);
        },
        getNomenclaturesFilteredByName: function (rNomTypeId, name) {
            var self = this,
                apiQuery = Utils.Uri.createQuery({
                    'name': name
                }),
                nomValuesUrl = 'api/reg/noms/system/' + rNomTypeId + '/values?' + apiQuery;
            return self.get(nomValuesUrl);
        },
        saveNomValue: function (nomValue) {
            var self = this,
                id = nomValue.rNomTypeValueId,
                url = 'api/reg/noms/system/' + nomValue.rNomTypeId + '/values',
                promise;

            if (id) {
                promise = self.put(url + '/' + id, nomValue);
            } else {
                promise = self.post(url, nomValue);
            }

            return promise;
        }
    });
    return NomenclatureRepository;
});
