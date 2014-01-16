define([
    //libs
    'jquery',
    'knockout',
    'knockout.mapping',
    'q',
    'lodash',

    //*globals
    'document',

    //framework
    'framework/corium',

    //src
    'src/validation_utils',
    'src/repositories/building_repository',
    'src/view_models/upload_file_vm'
], function (
    $,
    ko,
    ko_mapping,
    Q,
    _,
    document,
    Corium,
    ValidationUtils,
    BuildingRepository,
    UploadFileVM
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
            self._deleteBuildingImage = self._deleteBuildingImage.bind(self);
            self._deleteMenuItemImage = self._deleteMenuItemImage.bind(self);
            self._buildingImage = self._buildingImage.bind(self);
            self._addMenuCat = self._addMenuCat.bind(self);
            self._deleteMenuCat = self._deleteMenuCat.bind(self);
            self._unDelete = self._unDelete.bind(self);
            self._loadSingleMenuCat = self._loadSingleMenuCat.bind(self);

            self._enterMenuEditMode = self._enterMenuEditMode.bind(self);
            self._exitMenuEditMode = self._exitMenuEditMode.bind(self);
            self._deleteMenuItem = self._deleteMenuItem.bind(self);
            self._addMenuItem = self._addMenuItem.bind(self);
            self._menuItemImage = self._menuItemImage.bind(self);

            self._currentTab = ko.observable(Corium.app.services.tabCache.loadTab(Corium.navigation._navigator.getHash(), 'buildingMenu'));

            self._buildingRepository = new BuildingRepository();

            self._inEditMode = ko.observable(inEditMode);
            self._inMenuEditMode = ko.observable(false);
            self._isInDialog = ko.observable(false);
            self._showErrors = ko.observable(false);
            self._saveButtonClicked = ko.observable(false);
            self._isActive = ko.observable(building.isActive ? '1' : '2');
            self._isDeleted = ko.observable(building.isDeleted ? '1' : '2');

            self._currMenu = ko.observable();
            self._currMenuCat = ko.observable();
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
        _addMenuItem: function () {
            var self = this,
                menu = {
                    menuCategoryId: self._currMenuCat().menuCategoryId(),
                    name: '',
                    imagePath: 'app\\img\\nopicsmall.jpg',
                    hasImage: false,
                    info: '',
                    size: '',
                    price: '',
                    isActive: true,
                    isDeleted: false,
                    isEdited: false,
                    isNew: true
                };

            self._enterMenuEditMode(ko_mapping.fromJS(menu));
        },
        _deleteMenuItem: function (target) {
            var self = this,
                idx;

            if (target.isNew() === true) {
                idx = self._currMenuCat().menus.indexOf(target);

                if (idx > -1) {
                    self._currMenuCat().menus.splice(idx, 1);
                }
            }
            else {
                target.isDeleted(true);
            }
        },
        _exitMenuEditMode: function (target) {
            var self = this;

            if (target.isNew() && !target.isEdited()) {
                target.isEdited(true);
                self._currMenuCat().menus.push(target);
            }

            self._inMenuEditMode(false);
        },
        _enterMenuEditMode: function (target) {
            var self = this;

            self._currMenu(target);

            self._inMenuEditMode(true);
        },
        _loadSingleMenuCat: function (target) {
            var self = this;

            self._currMenuCat(target);
        },
        _unDelete: function (target) {
            target.isDeleted(false);
        },
        _deleteMenuCat: function (target) {
            var self = this,
                idx;

            if (target.isNew() === true) {
                idx = self._building().menuCategories.indexOf(target);

                if (idx > -1) {
                    self._building().menuCategories.splice(idx, 1);
                }
            }
            else {
                target.isDeleted(true);
            }
        },
        _addMenuCat: function () {
            var self = this,
                menuCategory = {
                    buildingId: self._building().buildingId(),
                    name: '',
                    isActive: true,
                    isDeleted: false,
                    isEdited: false,
                    isNew: true,
                    menus: []
                };

            self._building().menuCategories.push(ko_mapping.fromJS(menuCategory));
        },
        _menuItemImage: function () {
            var self = this,
                uploadFileVM = new UploadFileVM();

            Corium.dialogs.show({
                header: 'Добавяне на снимка',
                acceptText: 'Добави',
                cancelText: 'Отказ',
                accepting: function (event) {
                    event.preventDefault();
                    uploadFileVM.attach('ImageMenuItem').then(function (result) {
                        if (result) {
                            self._currMenu().imagePath(result);
                            self._currMenu().hasImage(true);
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _buildingImage: function () {
            var self = this,
                building = self._building(),
                uploadFileVM = new UploadFileVM();

            Corium.dialogs.show({
                header: 'Добавяне на снимка',
                acceptText: 'Добави',
                cancelText: 'Отказ',
                accepting: function (event) {
                    event.preventDefault();
                    uploadFileVM.attach('ImageBuilding').then(function (result) {
                        if (result) {
                            building.imagePath(result);
                            building.hasImage(true);
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _deleteMenuItemImage: function () {
            var self = this;

            self._currMenu().imagePath('app\\img\\nopicsmall.jpg');
            self._currMenu().hasImage(false);
        },
        _deleteBuildingImage: function () {
            var self = this;

            self._building().imagePath('app\\img\\nopic.jpg');
            self._building().hasImage(false);
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

            self._currMenuCat = ko.observable(_.first(self._building().menuCategories())); //todo when have 0 ?
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
