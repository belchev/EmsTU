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
            
            url = 'api/noms/getPopNoms?' + apiQuery;

            return self.get(url);
        }
    });
    return NomRepository;
});
