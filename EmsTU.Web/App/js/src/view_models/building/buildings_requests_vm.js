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

    var BuildingsRqsVM = Corium.Class.extend({
        constructor: function (
            buildingRequests,
            buildingRequestsCount,
            msg,
            buildingName,
            contactName,
            userName,
            limit,
            offset
            ) {
            var self = this,
                numberOfPages = Math.ceil(buildingRequestsCount / limit),
                currentPage = offset / limit + 1,
                segmentLength = Math.min(7, numberOfPages),
                radius = (segmentLength + 1) / 2,
                startPage = Math.min(Math.max(currentPage - radius, 0),
                    numberOfPages - segmentLength) + 1,
                endPage = startPage + segmentLength,
                page;

            self.templateId = 'templates:building:buildings_requests_list.html';

            self.search = self.search.bind(self);
            self.goToPage = self.goToPage.bind(self);
            self.reset = self.reset.bind(self);
            self._newBuilding = self._newBuilding.bind(self);
            self._newUser = self._newUser.bind(self);

            self._buildingName = ko.observable(buildingName);
            self._contactName = ko.observable(contactName);
            self._userName = ko.observable(userName);

            self._resultCount = buildingRequestsCount;
            self._msgString = ko.observable(msg);


            self._buildingRequests = ko.observableArray(buildingRequests.map(function (value) {
                var mappedBuildingRq = ko_mapping.fromJS(value);
                return mappedBuildingRq;
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
                'building#requests',
                {
                    query: {
                        'bn': self._buildingName(),
                        'cn': self._contactName(),
                        'un': self._userName()
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
                'building#requests',
                {
                    query: {
                        'bn': self._buildingName(),
                        'cn': self._contactName(),
                        'un': self._userName(),
                        'limit': self._pager.itemsPerPage,
                        'offset': (page - 1) * self._pager.itemsPerPage
                    }
                });
        },
        reset: function () {
            Corium.navigation.navigateAction('building#requests');
        },
        _newBuilding: function (details) {
            Corium.navigation.navigateAction('building#newBuilding', { details: details });
        },
        _newUser: function (details) {
            Corium.navigation.navigateAction('user#newUser', { details: details });
        }
    });
    return BuildingsRqsVM;
});
