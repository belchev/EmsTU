define([
    //libs
    'q',

    //framework
    'framework/corium'

    //src
    //'src/utils'
], function (Q, Corium) {
    'use strict';

    var FileRepository = Corium.Repository.extend({
        constructor: function () {
        },
        upload: function ($fileInput, type) {
            var self = this,
                url = 'file/' + type;

            return self.postFile(url, $fileInput);
        }
    });
    return FileRepository;
});
