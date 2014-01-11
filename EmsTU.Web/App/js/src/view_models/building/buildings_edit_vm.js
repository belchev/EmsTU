define([
    //libs
    'jquery',
    'knockout',
    'knockout.mapping',
    'q',

    //*globals
    'document',

    //framework
    'framework/corium',

    //src
    'src/repositories/building_repository'
], function (
    $,
    ko,
    ko_mapping,
    Q,
    document,
    Corium,
    BuildingRepository) {
    'use strict';

    var EditBuildingVM = Corium.Class.extend({
        constructor: function (building) {
            var self = this;

            self.templateId = 'templates:building:buildings_edit.html';
            self._innerNavigation = self._innerNavigation.bind(self);
            self._save = self._save.bind(self);
            self._cancel = self._cancel.bind(self);
            self._enterEditMode = self._enterEditMode.bind(self);
            self._exitEditMode = self._exitEditMode.bind(self);

            self._buildingRepository = new BuildingRepository();

            self._inEditMode = ko.observable(false);
            self._isInDialog = ko.observable(false);

            self._building = ko.observable();
            self._currentTab = ko.observable(Corium.app.services.tabCache.loadTab(Corium.navigation._navigator.getHash(), 'buildingInfo'));

            self._setBuilding(building);
        },
        _innerNavigation: function (tabName) {
            var self = this;

            self._currentTab(tabName);

            Corium.app.services.tabCache.saveTab(Corium.navigation._navigator.getHash(), tabName);
        },
        _setBuilding: function (building) {
            var self = this;

            building = ko_mapping.fromJS(building);
            self._addBuildingValidationExtenders(building);
            self._building(building);
        },
        _addBuildingValidationExtenders: function (building) {

        },
        _save: function () {


        },
        _enterEditMode: function () {
            var self = this;

            //self._tempCorr = $.extend(true, {}, ko_mapping.toJS(self._corr()));
            self._inEditMode(true);
        },
        _exitEditMode: function () {
            var self = this;

            //if (self._isNew()) {
            //    return self._cancel();
            //}

            //self._setCorr(self._tempCorr);
            self._inEditMode(false);
        },
        _cancel: function () {
            Corium.navigation.navigateAction('building#search');
        }
    });
    return EditBuildingVM;
});
