define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function ($, ko, Corium) {
    'use strict';

    var NomenclaturesVM = Corium.Class.extend({
        constructor: function (nomList) {
            var self = this;
            self.templateId = 'templates:nomenclature:nomtypes.html';

            self._nomList = ko.observable(nomList.noms);
        }
    });
    return NomenclaturesVM;
});