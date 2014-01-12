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
        search: function (
            type,
            name,
            limit,
            offset
            ) {
            var self = this,
                url,
                apiQuery;

            apiQuery = Utils.Uri.createQuery({
                'type': type,
                'name': name,
                'limit': limit,
                'offset': offset
            });
            
            switch (type) {
                case 'BuildingTypes':
                    url = 'api/noms/getNomBuildingTypes?' + apiQuery;
                    break;
                case 'KitchenTypes':
                    url = 'api/noms/getNomKitchenTypes?' + apiQuery;
                    break;
                case 'MusicTypes':
                    url = 'api/noms/getNomMusicTypes?' + apiQuery;
                    break;
                case 'OccasionTypes':
                    url = 'api/noms/getNomOccasionTypes?' + apiQuery;
                    break;
                case 'PaymentTypes':
                    url = 'api/noms/getNomPaymentTypes?' + apiQuery;
                    break;
                case 'Extras':
                    url = 'api/noms/getNomExtras?' + apiQuery;
                    break;
                default:
            }

            return self.get(url);
        }
    });
    return NomRepository;
});
