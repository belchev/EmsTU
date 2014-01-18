define([
    //libs
    'jquery',
    'knockout',
    'knockout.mapping',

    //framework
    'src/utils',
    'framework/corium',

    //src
    'src/repositories/nom_repository'
], function ($, ko, ko_mapping, Utils, Corium, NomRepository) {
    'use strict';

    var NomenclaturesVM = Corium.Class.extend({
        constructor: function (nomTypeId, nomenclatures, searchName, searchAlias, searchIsActive, limit, offset, nomenclaturesCount) {
            var self = this,
              numberOfPages = Math.ceil(nomenclaturesCount / limit),
              currentPage = offset / limit + 1,
              segmentLength = Math.min(7, numberOfPages),
              radius = (segmentLength + 1) / 2,
              startPage = Math.min(Math.max(currentPage - radius, 0),
                  numberOfPages - segmentLength) + 1,
              endPage = startPage + segmentLength,
              page;

            self.templateId = 'templates:nomenclature:nom_list.html';
            self._nomRepository = new NomRepository();

            self.editNom = self.editNom.bind(self);
            self.newNom = self.newNom.bind(self);
            self.deleteNom = self.deleteNom.bind(self);
            self.search = self.search.bind(self);

            self._nomenclatures = ko.observableArray([]);
            self.errorString = ko.observable();

            nomenclatures.map(function (item) {
                self._nomenclatures.push(ko_mapping.fromJS(item));
            });

            self._nomTypeId = ko.observable(nomTypeId);
            self._name = ko.observable(searchName);
            self._alias = ko.observable(searchAlias);
            self._isActive = ko.observable(searchIsActive);

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
        editNom: function (nom) {
            var nomId = nom.nomId();

            Corium.navigation.navigateAction('nomenclature#editNom', { nomId: nomId });
        },
        newNom: function () {
            var self = this;

            Corium.navigation.navigateAction('nomenclature#newNom', { nomTypeId: self._nomTypeId()});
        },
        deleteNom: function (nom) {
            var self = this,
                nomId = nom.nomId();

            self._nomRepository.deleteNom(nomId).then(function (data) {
                if (data.err) {
                    self.errorString(data.err);
                } else {
                    Corium.navigation.navigateAction('nomenclature#searchNoms', { nomTypeId: self._nomTypeId() });
                }
            });
        },
        search: function () {
            var self = this;

            Corium.navigation.navigateAction(
                'nomenclature#searchNoms',
                {
                    nomTypeId: self._nomTypeId(),
                    query: {
                        'n': self._name(),
                        'a': self._alias(),
                        'ia': self._isActive()
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
                'nomenclature#searchNoms',
                {
                    nomTypeId: self._nomTypeId(),
                    query: {
                        'n': self._name(),
                        'a': self._alias(),
                        'ia': self._isActive(),
                        'o': (page - 1) * self._pager.itemsPerPage
                    }
                });
        }
    });

    return NomenclaturesVM;
});