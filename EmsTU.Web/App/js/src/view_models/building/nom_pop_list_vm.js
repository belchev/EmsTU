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
    'src/repositories/nom_repository'
], function (
    $,
    ko,
    ko_mapping,
    Q,
    document,
    Corium,
    NomRepository) {
    'use strict';

    var NomsPopListVM = Corium.Class.extend({
        constructor: function (type) {
            var self = this;

            self.templateId = 'templates:building:nom_pop_list.html';
            self._search = self._search.bind(self);
            self.getSelected = self.getSelected.bind(self);

            self._nomRepository = new NomRepository();
            self._addDelegate = ko.observable();
            self._type = ko.observable(type);

            //filters
            self._name = ko.observable();

            //list
            self._noms = ko.observableArray([]);
            self._resultCount = ko.observable(0);

            self._pager = ko.observable();

            self._selectedNoms = ko.observableArray([]);

            ko.computed(function () {
                var noms = self._noms(),
                    match;

                ko.utils.arrayForEach(noms, function (nom) {
                    if (nom.isSelected() === true) {
                        match = ko.utils.arrayFirst(self._selectedNoms(), function (item) {
                            return nom.nomId() === item.nomId;
                        });
                        if (!match) {
                            self._selectedNoms.push(ko_mapping.toJS(nom));
                        }
                    }
                    else {
                        match = ko.utils.arrayFirst(self._selectedNoms(), function (item) {
                            return nom.nomId() === item.nomId;
                        });
                        if (match) {
                            self._selectedNoms.remove(match);
                        }
                    }
                });

            });
        },
        _search: function (offsetValue) {
            var self = this,
                limit = 10,
                offset = offsetValue || 0;

            self._noms([]);

            return self._nomRepository.search(self._type(), self._name(), limit, offset).then(function (result) {
                var noms = result.noms,
                    nomsCount = result.nomsCount,
                    numberOfPages = Math.ceil(nomsCount / limit),
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

                self._resultCount(nomsCount);

                noms.map(function (item) {
                    var mappedItem = ko_mapping.fromJS(item);

                    match = ko.utils.arrayFirst(self._selectedNoms(), function (selectedItem) {
                        return mappedItem.nomId() === selectedItem.nomId;
                    });

                    if (match) {
                        mappedItem.isSelected(true);
                    }

                    self._noms.push(mappedItem);
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
                result = $.extend(true, [], self._selectedNoms());

            self._selectedNoms([]);

            return result;
        }
    });
    return NomsPopListVM;
});
