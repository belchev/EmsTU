define([
    //framework
    'framework/corium',

    //src
    'src/utils'
], function (Corium, Utils) {
    'use strict';

    var BuildingRepository = Corium.Repository.extend({
        constructor: function () {
        },
        newBuilding: function () {
            var self = this,
                url = 'api/buildings/newBuilding';

            return self.get(url);
        },
        getBuildings: function (
            name,
            buildingTypeId,
            kitchenTypeId,
            musicTypeId,
            occasionTypeId,
            extraId,
            limit,
            offset
            ) {
            var self = this,
                apiQuery,
                urlQuery;

            apiQuery = Utils.Uri.createQuery({
                'name': name,
                'buildingTypeId': buildingTypeId,
                'kitchenTypeId': kitchenTypeId,
                'musicTypeId': musicTypeId,
                'occasionTypeId': occasionTypeId,
                'extraId': extraId,
                'limit': limit,
                'offset': offset
            });

            urlQuery = 'api/buildings?' + apiQuery;

            return self.get(urlQuery);
        },
        search: function (
            name,
            //email,
            limit,
            offset
            ) {
            var self = this,
                url,
                apiQuery;

            apiQuery = Utils.Uri.createQuery({
                'name': name,
                //'email': email,
                'limit': limit,
                'offset': offset
            });

            url = 'api/noms/buildings?' + apiQuery;

            return self.get(url);
        }
    });
    return BuildingRepository;
});
