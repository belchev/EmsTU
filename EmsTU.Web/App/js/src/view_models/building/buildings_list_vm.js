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
    'src/utils'
], function (
    $,
    ko,
    ko_mapping,
    Q,
    document,
    Corium,
    Utils
    ) {
    'use strict';

    var BuildingsListVM = Corium.Class.extend({
        constructor: function (
            buildings,
            buildingsCount,
            msg,
            buildingPage,
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
                numberOfPages = Math.ceil(buildingsCount / limit),
                currentPage = offset / limit + 1,
                segmentLength = Math.min(7, numberOfPages),
                radius = (segmentLength + 1) / 2,
                startPage = Math.min(Math.max(currentPage - radius, 0),
                    numberOfPages - segmentLength) + 1,
                endPage = startPage + segmentLength,
                page;

            self.templateId = 'templates:building:buildings_list.html';

            self.search = self.search.bind(self);
            self.goToPage = self.goToPage.bind(self);
            self.reset = self.reset.bind(self);
            self.editBuilding = self.editBuilding.bind(self);

            self._buildingPage = ko.observable(buildingPage);
            self._name = ko.observable(name);
            self._buildingTypeId = ko.observable(buildingTypeId);
            self._kitchenTypeId = ko.observable(kitchenTypeId);
            self._musicTypeId = ko.observable(musicTypeId);
            self._occasionTypeId = ko.observable(occasionTypeId);
            self._extraId = ko.observable(extraId);

            self._resultCount = buildingsCount;
            self._msgString = ko.observable(msg);

            //buildings.map(function (value) {
            //    self._buildings.push(ko_mapping.fromJS(value));
            //});

            self._buildings = ko.observableArray(buildings.map(function (value) {
                var mappedBuilding = ko_mapping.fromJS(value);
                return mappedBuilding;
            }));

            self._pager = {};
            self._pager.pages = [];
            for (page = startPage; page < endPage; page++) {
                self._pager.pages.push({
                    pageNumber: page,
                    isCurrentPage: page === currentPage
                });
            }

            self._pager.isPaged = numberOfPages > 1;
            self._pager.noPrevPage = currentPage === 1;
            self._pager.prevPage = currentPage - 1;
            self._pager.firstPage = 1;
            self._pager.noNextPage = currentPage === numberOfPages;
            self._pager.nextPage = currentPage + 1;
            self._pager.lastPage = numberOfPages;
            self._pager.itemsPerPage = limit;
        },
        search: function () {
            var self = this;

            Corium.navigation.navigateAction(
                'building#search',
                {
                    query: {
                        'bp': self._buildingPage(),
                        'n': self._name(),
                        'bt': self._buildingTypeId(),
                        'kt': self._kitchenTypeId(),
                        'mt': self._musicTypeId(),
                        'ot': self._occasionTypeId(),
                        'e': self._extraId()
                    }
                });
        },
        goToPage: function (page) {
            var self = this,
                query;

            if (self._pager.noPrevPage && page < 1 ||
                self._pager.noNextPage && page > self._pager.lastPage) {
                return;
            }

            query = Utils.Uri.createQuery();

            Corium.navigation.navigateAction(
                'building#search',
                {
                    query: {
                        'bp': self._buildingPage(),
                        'n': self._name(),
                        'bt': self._buildingTypeId(),
                        'kt': self._kitchenTypeId(),
                        'mt': self._musicTypeId(),
                        'ot': self._occasionTypeId(),
                        'e': self._extraId(),
                        'limit': self._pager.itemsPerPage,
                        'offset': (page - 1) * self._pager.itemsPerPage
                    }
                });
        },
        reset: function () {
            Corium.navigation.navigateAction('building#search');
        },
        editBuilding: function (details) {
            var buildingId = details.buildingId();

            Corium.navigation.navigateAction('building#edit', { buildingId: buildingId });
        }
    });
    return BuildingsListVM;
});
