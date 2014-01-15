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
        getBuildingRqs: function (
            buildingName,
            contactName,
            userName,
            limit,
            offset
            ) {
            var self = this,
               apiQuery,
               urlQuery;

            apiQuery = Utils.Uri.createQuery({
                'buildingName': buildingName,
                'contactName': contactName,
                'userName': userName,
                'limit': limit,
                'offset': offset
            });

            urlQuery = 'api/buildings/requests?' + apiQuery;
            return self.get(urlQuery);
        },
        newMenuCategory: function(id) {
            var self = this,
                  buildingId = parseInt(id, 10),
                  url = 'api/buildings/' + buildingId + '/menuCategory/new';

            return self.get(url);
        },
        getBuilding: function (id) {
            var self = this,
                buildingId = parseInt(id, 10),
                url = 'api/buildings/' + buildingId;

            return self.get(url);
        },
        save: function (buildingData, buildingRqId) {
            var self = this,
                rqId = parseInt(buildingRqId, 10),
                id = buildingData.buildingId,
                url;
                

            if (id) {
                url = 'api/buildings/' + id;
                return self.put(url, buildingData);
            } else {
                url = 'api/buildings/' + rqId;
                return self.post(url, buildingData);
            }
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
