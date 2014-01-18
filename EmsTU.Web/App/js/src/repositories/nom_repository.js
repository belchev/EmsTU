define([
    //framework
    'framework/corium',

    //src
    'src/utils'
], function (Corium, Utils) {
    'use strict';

    var NomRepository = Corium.Repository.extend({
        constructor: function () {
        },
        getNomTypes: function () {
            var self = this,
                url = 'api/nomenclatures';

            return self.get(url);
        },
        getNoms: function (nomTypeId, name, alias, isActive, limit, offset) {
            var self = this,
                apiQuery,
                urlQuery;

            apiQuery = Utils.Uri.createQuery({
                'name': name,
                'alias': alias,
                'isActive': isActive,
                'limit': limit,
                'offset': offset
            });

            urlQuery = 'api/nomenclatures/' + nomTypeId + '/?' + apiQuery;
            return self.get(urlQuery);
        },
        getNom: function (id) {
            var self = this,
                nomId = parseInt(id, 10),
                url = 'api/nomenclatures/nom/' + nomId;

            return self.get(url);
        },
        deleteNom: function (id) {
            var self = this,
                nomId = parseInt(id, 10),
                url = 'api/nomenclatures/nom/' + nomId;

            return self.del(url);
        },
        save: function (nomData) {
            var self = this,
                id = nomData.nomId,
                url,
                promise;

            if (id) {
                url = 'api/nomenclatures/nom/' + id;
                promise = self.put(url, nomData);
            } else {
                url = 'api/nomenclatures/nom';
                promise = self.post(url, nomData);
            }
            return promise;
        }
    });
    return NomRepository;
});
