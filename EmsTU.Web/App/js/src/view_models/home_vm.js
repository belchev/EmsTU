define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function (
    $,
    ko,
    Corium
    ) {
    'use strict';

    var HomeVM = Corium.Class.extend({
        constructor: function () {
            var self = this;

            self.templateId = 'templates:home.html';
        }
    });

    return HomeVM;
});