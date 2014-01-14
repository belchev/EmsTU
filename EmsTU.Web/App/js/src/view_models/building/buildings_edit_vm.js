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
    'src/validation_utils',
    'src/repositories/building_repository',
    'src/view_models/upload_file_vm',
    'src/view_models/building/nom_pop_list_vm'
], function (
    $,
    ko,
    ko_mapping,
    Q,
    document,
    Corium,
    ValidationUtils,
    BuildingRepository,
    UploadFileVM,
    NomPopListVM
    ) {
    'use strict';

    var EditBuildingVM = Corium.Class.extend({
        constructor: function (building, inEditMode) {
            var self = this;

            self.templateId = 'templates:building:buildings_edit.html';
            self._innerNavigation = self._innerNavigation.bind(self);
            self._save = self._save.bind(self);
            self._cancel = self._cancel.bind(self);
            self._enterEditMode = self._enterEditMode.bind(self);
            self._exitEditMode = self._exitEditMode.bind(self);
            self._deleteFile = self._deleteFile.bind(self);
            self._chooseFile = self._chooseFile.bind(self);
            self._currentTab = ko.observable(Corium.app.services.tabCache.loadTab(Corium.navigation._navigator.getHash(), 'buildingMenu'));

            self._buildingRepository = new BuildingRepository();

            self._inEditMode = ko.observable(inEditMode);
            self._isInDialog = ko.observable(false);
            self._showErrors = ko.observable(false);
            self._saveButtonClicked = ko.observable(false);
            self._isActive = ko.observable(building.isActive ? '1' : '2');
            self._isDeleted = ko.observable(building.isDeleted ? '1' : '2');

            self._building = ko.observable();
            self._setBuilding(building);

            self._nameError = undefined;
            self._nameChanged = ko.observable(false);
            self._building().name.subscribe(function () {
                self._nameChanged(true);
            });
            self._nameError = ko.computed(function () {
                var name = self._building().name();

                if (self._saveButtonClicked() || self._nameChanged()) {
                    return !name;
                }
                else {
                    return false;
                }
            });
            self._addressError = undefined;
            self._addressChanged = ko.observable(false);
            self._building().address.subscribe(function () {
                self._addressChanged(true);
            });
            self._addressError = ko.computed(function () {
                var address = self._building().address();

                if (self._saveButtonClicked() || self._addressChanged()) {
                    return !address;
                }
                else {
                    return false;
                }
            });
            self._contactPhoneError = undefined;
            self._contactPhoneChanged = ko.observable(false);
            self._building().contactPhone.subscribe(function () {
                self._contactPhoneChanged(true);
            });
            self._contactPhoneError = ko.computed(function () {
                var contactPhone = self._building().contactPhone();

                if (self._saveButtonClicked() || self._contactPhoneChanged()) {
                    return !contactPhone;
                }
                else {
                    return false;
                }
            });
            self._yesNoOptions = ko.observableArray([
                { name: 'Да', value: '1' },
                { name: 'Не', value: '2' }
            ]);

            


            if (inEditMode) {
                self._enterEditMode();
            }
        },
        _chooseFile: function () {
            var self = this,
                building = self._building(),
                uploadFileVM = new UploadFileVM();

            Corium.dialogs.show({
                header: 'Добавяне на снимка',
                acceptText: 'Добави',
                cancelText: 'Отказ',
                accepting: function (event) {
                    event.preventDefault();
                    uploadFileVM.attach().then(function (result) {
                        if (result) {
                            building.imagePath(result);
                            building.hasLogo(true);
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _deleteFile: function () {
            var self = this;

            self._building().imagePath('app\\img\\nopic.jpg');
            self._building().hasLogo(false);
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
            var self = this;

            self._isActive.extend({ required: true });
            self._isDeleted.extend({ required: true });
            building.settlementId.extend({ required: true });
        },
        _save: function (inEditMode) {
            var self = this,
                isFormInvalid,
                building = self._building();

            self._saveButtonClicked(true);
            if (ValidationUtils.isValid(building)) {
                isFormInvalid = self._nameError() || self._contactPhoneError() || self._addressError();

                if (!isFormInvalid) {
                    building.isActive(self._isActive() === '1' ? true : false);
                    building.isDeleted(self._isDeleted() === '1' ? true : false);

                    self._buildingRepository.save(ko_mapping.toJS(building)).then(function (data) {
                        if (data.err) {
                            building.errorString(data.err);
                        } else {
                            Corium.navigation.navigateAction(
                                'building#edit',
                                { buildingId: data.buildingId, inEditMode: inEditMode === false });
                        }
                    });
                }
            } else {
                self._showErrors(true);
            }
        },
        _enterEditMode: function () {
            var self = this;

            self._inEditMode(true);
        },
        _exitEditMode: function () {
            var self = this;

            self._inEditMode(false);
        },
        _cancel: function () {
            Corium.navigation.navigateAction('building#search');
        }
    });
    return EditBuildingVM;
});
