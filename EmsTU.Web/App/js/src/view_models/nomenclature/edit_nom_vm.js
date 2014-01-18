define([
    //libs
    'jquery',
    'knockout',
    'knockout.mapping',
    'q',

    //framework
    'framework/corium',

    //src
    'src/validation_utils',
    'src/repositories/nom_repository'
], function (
    $,
    ko,
    ko_mapping,
    Q,
    Corium,
    ValidationUtils,
    NomRepository) {
    'use strict';

    var EditNomVM = Corium.Class.extend({
        constructor: function (nom) {
            var self = this;

            self.templateId = 'templates:nomenclature:edit_nom.html';
            self._nomRepository = new NomRepository();

            self.save = self.save.bind(self);
            self.cancel = self.cancel.bind(self);

            self._showErrors = ko.observable(false);

            self._isEdit = nom.nomId > 0;
            self._nom = ko.observable();
            self.errorString = ko.observable();

            self._setNom(nom);
        },
        _setNom: function (nom) {
            var self = this;

            nom = ko_mapping.fromJS(nom);
            self._addNomValidationExtenders(nom);
            self._nom(nom);
        },
        _addNomValidationExtenders: function (nom) {
            nom.name.extend({ required: true });
            nom.alias.extend({ required: true });
        },
        save: function () {
            var self = this,
                nom = self._nom();

            if (ValidationUtils.isValid(nom)) {
                self._nomRepository.save(ko_mapping.toJS(nom)).then(function (data) {
                    if (data.err) {
                        self.errorString(data.err);
                    } else {
                        Corium.navigation.navigateAction('nomenclature#searchNoms', { nomTypeId: nom.nomTypeId() });
                    }
                });
            } else {
                self._showErrors(true);
            }
        },
        cancel: function () {
            var self = this;

            Corium.navigation.navigateAction('nomenclature#searchNoms', { nomTypeId: self._nom().nomTypeId() });
        }
    });
    return EditNomVM;
});