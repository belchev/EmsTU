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
