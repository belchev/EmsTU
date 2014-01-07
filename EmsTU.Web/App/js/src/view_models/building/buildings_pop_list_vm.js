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

    var BuildingsPopListVM = Corium.Class.extend({
        constructor: function () {
            var self = this;

            self.templateId = 'templates:building:buildings_pop_list.html';
            //self._add = self._add.bind(self);
            self._search = self._search.bind(self);
            self.getSelected = self.getSelected.bind(self);

            self._buildingRepository = new BuildingRepository();
            //self._docCorrRepository = new BuildingRepository();


            self._addDelegate = ko.observable();
            //filters
            self._name = ko.observable();
            //self._email = ko.observable();
            //list
            self._buildings = ko.observableArray([]);
            self._resultCount = ko.observable(0);

            self._pager = ko.observable();

            self._selectedBuildings = ko.observableArray([]);

            ko.computed(function () {
                var buildings = self._buildings(),
                    match;

                ko.utils.arrayForEach(buildings, function (building) {
                    if (building.isSelected() === true) {
                        match = ko.utils.arrayFirst(self._selectedBuildings(), function (item) {
                            return building.buildingId() === item.buildingId;
                        });
                        if (!match) {
                            self._selectedBuildings.push(ko_mapping.toJS(building));
                        }
                    }
                    else {
                        match = ko.utils.arrayFirst(self._selectedBuildings(), function (item) {
                            return building.buildingId() === item.buildingId;
                        });
                        if (match) {
                            self._selectedBuildings.remove(match);
                        }
                    }
                });

            });
        },
        //_add: function () {
        //    var self = this,
        //        editCorrVM;

        //    self._corrRepository.newCorr().then(function (corr) {
        //        editCorrVM = new EditCorrVM(corr);
        //        editCorrVM._isInDialog(true);

        //        Corium.dialogs.show({
        //            header: 'Нов кореспондент',
        //            acceptText: 'Запис и избор',
        //            cancelText: 'Отказ',
        //            width: 800,
        //            height: 500,
        //            accepting: function (event) {
        //                event.preventDefault();
        //                editCorrVM._save().then(function (corr) {
        //                    if (corr) {
        //                        self._addDelegate()(corr);
        //                        Corium.dialogs.hide();
        //                    }
        //                });
        //            },
        //            viewModel: editCorrVM
        //        });
        //    });
        //},
        _search: function (offsetValue) {
            var self = this,
                limit = 10,
                offset = offsetValue || 0;

            self._buildings([]);

            return self._buildingRepository.search(self._name(), limit, offset).then(function (result) { // self._email(),
                var buildings = result.buildings,
                    buildingsCount = result.buildingsCount,
                    numberOfPages = Math.ceil(buildingsCount / limit),
                    currentPage = offset / limit + 1,
                    segmentLength = Math.min(7, numberOfPages),
                    radius = (segmentLength + 1) / 2,
                    startPage = Math.min(Math.max(currentPage - radius, 0),
                    numberOfPages - segmentLength) + 1,
                    endPage = startPage + segmentLength,
                    page,
                    pager = {},
                    match;

                pager.pages = [];
                for (page = startPage; page < endPage; page++) {
                    pager.pages.push({
                        pageNumber: page,
                        isCurrentPage: page === currentPage
                    });
                }

                pager.isPaged = numberOfPages > 1;
                pager.noPrevPage = currentPage === 1;
                pager.prevPage = currentPage - 1;
                pager.firstPage = 1;
                pager.noNextPage = currentPage === numberOfPages;
                pager.nextPage = currentPage + 1;
                pager.lastPage = numberOfPages;
                pager.itemsPerPage = limit;

                self._pager(ko_mapping.fromJS(pager));

                self._resultCount(buildingsCount);

                buildings.map(function (item) {
                    var mappedItem = ko_mapping.fromJS(item);

                    match = ko.utils.arrayFirst(self._selectedBuildings(), function (selectedItem) {
                        return mappedItem.buildingId() === selectedItem.buildingId;
                    });

                    if (match) {
                        mappedItem.isSelected(true);
                    }

                    self._buildings.push(mappedItem);
                });
            });
        },
        goToPage: function (page) {
            var self = this;

            if (self._pager().noPrevPage() && page < 1 ||
                self._pager().noNextPage() && page > self._pager().lastPage()) {
                return;
            }

            return self._search((page - 1) * self._pager().itemsPerPage());
        },
        getSelected: function () {
            var self = this,
                result = $.extend(true, [], self._selectedBuildings());

            self._selectedBuildings([]);

            return result;
        }
    });
    return BuildingsPopListVM;
});
