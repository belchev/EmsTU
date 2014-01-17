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

            self._buildingRepository = new BuildingRepository();

            self.templateId = 'templates:building:buildings_edit.html';
            self._innerNavigation = self._innerNavigation.bind(self);
            self._unDelete = self._unDelete.bind(self);
            self._cancel = self._cancel.bind(self);
            self._save = self._save.bind(self);
            self._enterEditMode = self._enterEditMode.bind(self);
            self._exitEditMode = self._exitEditMode.bind(self);
            self._addBuildingImage = self._addBuildingImage.bind(self);
            self._deleteBuildingImage = self._deleteBuildingImage.bind(self);
            self._enterMenuEditMode = self._enterMenuEditMode.bind(self);
            self._exitMenuEditMode = self._exitMenuEditMode.bind(self);
            self._loadSingleCat = self._loadSingleCat.bind(self);
            self._addMenuCat = self._addMenuCat.bind(self);
            self._deleteMenuCat = self._deleteMenuCat.bind(self);
            self._addMenuItem = self._addMenuItem.bind(self);
            self._deleteMenuItem = self._deleteMenuItem.bind(self);
            self._addMenuItemImage = self._addMenuItemImage.bind(self);
            self._deleteMenuItemImage = self._deleteMenuItemImage.bind(self);
            self._deleteAlbumPhoto = self._deleteAlbumPhoto.bind(self);
            self._addAlbumPhoto = self._addAlbumPhoto.bind(self);
            self._addAlbumCat = self._addAlbumCat.bind(self);
            self._deleteAlbumCat = self._deleteAlbumCat.bind(self);

            self._currentTab = ko.observable(Corium.app.services.tabCache.loadTab(Corium.navigation._navigator.getHash(), 'buildingInfo'));

            self._inEditMode = ko.observable(inEditMode);
            self._isInDialog = ko.observable(false);
            self._saveButtonClicked = ko.observable(false);
            self._showErrors = ko.observable(false);
            self._inMenuEditMode = ko.observable(false);
            self._isActive = ko.observable(building.isActive ? '1' : '2');
            self._isDeleted = ko.observable(building.isDeleted ? '1' : '2');
            self._currMenu = ko.observable();
            self._currMenuCat = ko.observable();
            self._currAlbum = ko.observable();

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

            self._addEvent = self._addEvent.bind(self);
            self._enterEventEditMode = self._enterEventEditMode.bind(self);
            self._deleteEventItemImage = self._deleteEventItemImage.bind(self);
            self._addEventItemImage = self._addEventItemImage.bind(self);
            self._deleteEvent = self._deleteEvent.bind(self);
            self._inEventEditMode = ko.observable(false);
            self._currEvent = ko.observable();
            self._eventIsActive = ko.observable();

            if (inEditMode) {
                self._enterEditMode();
            }
        },
        _deleteEvent: function (target) {
            var self = this,
                idx = self._building().events.indexOf(target);

            if (target.isNew() && idx > -1) {
                self._building().events.splice(idx, 1);
            }
            else {
                target.isDeleted(true);
            }
        },
        _exitEventEditMode: function (target) {
            var self = this;
            target.isActive(self._eventIsActive() === '1' ? true : false);

            if (target.isNew() && !target.isEdited()) {
                target.isEdited(true);
                self._building().events.push(target);
            }

            self._inEventEditMode(false);
        },
        _addEventItemImage: function () {
            var self = this,
                uploadFileVM = new UploadFileVM();

            Corium.dialogs.show({
                header: 'Добавяне на снимка',
                acceptText: 'Добави',
                cancelText: 'Отказ',
                accepting: function (event) {
                    event.preventDefault();
                    uploadFileVM.attach('ImageBuilding').then(function (result) {
                        if (result.imageThumbPath && result.imagePath) {
                            self._currEvent().imageThumbPath(result.imageThumbPath);
                            self._currEvent().imagePath(result.imagePath);
                            self._currEvent().hasImage(true);
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _deleteEventItemImage: function () {
            var self = this;

            self._currEvent().imageThumbPath('app\\img\\nopic.jpg');
            self._currEvent().hasImage(false);
        },
        _addEvent: function () {
            var self = this,
                event = {
                    buildingId: self._building().buildingId(),
                    name: '',
                    imagePath: 'app\\img\\nopic.jpg',
                    imageThumbPath: 'app\\img\\nopic.jpg',
                    hasImage: false,
                    info: '',
                    date: undefined,
                    isActive: false,
                    isDeleted: false,
                    isEdited: false,
                    isNew: true
                };

            self._enterEventEditMode(ko_mapping.fromJS(event));
        },
        _enterEventEditMode: function (target) {
            var self = this;

            if (!target.isNew()) {
                target.isEdited(true);
            }
            self._eventIsActive(target.isActive() ? '1' : '2');
            self._currEvent(target);
            self._inEventEditMode(true);
        },
        _deleteAlbumCat: function (target) {
            var self = this,
                idx = self._building().albums.indexOf(target);

            if (target.isNew() && idx > -1) {
                self._building().albums.splice(idx, 1);
            }
            else {
                target.isDeleted(true);
            }
        },
        _addAlbumCat: function () {
            var self = this,
                album = {
                    buildingId: self._building().buildingId(),
                    name: '',
                    isActive: true,
                    isDeleted: false,
                    isEdited: false,
                    isNew: true,
                    albumPhotos: []
                };

            self._building().albums.push(ko_mapping.fromJS(album));
        },
        _addAlbumPhoto: function () {
            var self = this,
                uploadFileVM = new UploadFileVM();

            Corium.dialogs.show({
                header: 'Добавяне на снимка',
                acceptText: 'Добави',
                cancelText: 'Отказ',
                accepting: function (event) {
                    event.preventDefault();
                    uploadFileVM.attach('AlbumPhoto').then(function (result) {
                        var albumPhoto = {
                            albumId: self._currAlbum().albumId(),
                            imageThumbPath: '',
                            imagePath: '',
                            isDeleted: false,
                            isEdited: false,
                            isNew: true
                        };

                        if (result.imageThumbPath && result.imagePath) {
                            albumPhoto.imagePath = result.imagePath;
                            albumPhoto.imageThumbPath = result.imageThumbPath;
                            self._currAlbum().albumPhotos.push(ko_mapping.fromJS(albumPhoto));
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _deleteAlbumPhoto: function (target) {
            var self = this;

            target.isDeleted(true);
        },
        _deleteMenuItemImage: function () {
            var self = this;

            self._currMenu().imagePath('app\\img\\nopicsmall.jpg');
            self._currMenu().hasImage(false);
        },
        _addMenuItemImage: function () {
            var self = this,
                uploadFileVM = new UploadFileVM();

            Corium.dialogs.show({
                header: 'Добавяне на снимка',
                acceptText: 'Добави',
                cancelText: 'Отказ',
                accepting: function (event) {
                    event.preventDefault();
                    uploadFileVM.attach('ImageMenuItem').then(function (result) {
                        if (result.imageThumbPath) {
                            self._currMenu().imagePath(result.imageThumbPath);
                            self._currMenu().hasImage(true);
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _deleteMenuItem: function (target) {
            var self = this,
                idx = self._currMenuCat().menus.indexOf(target);

            if (target.isNew() && idx > -1) {
                self._currMenuCat().menus.splice(idx, 1);
            }
            else {
                target.isDeleted(true);
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
        _deleteMenuCat: function (target) {
            var self = this,
                idx = self._building().menuCategories.indexOf(target);

            if (target.isNew() && idx > -1) {
                self._building().menuCategories.splice(idx, 1);
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
        _loadSingleCat: function (target, name) {
            var self = this;

            //todo load by one
            if (name === 'Album') {
                self._currAlbum(target);
            }
            else if (name === 'MenuCat') {
                self._currMenuCat(target);
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
        _deleteBuildingImage: function () {
            var self = this;

            self._building().imagePath('app\\img\\nopic.jpg');
            self._building().hasImage(false);
        },
        _addBuildingImage: function () {
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
                        if (result.imageThumbPath) {
                            building.imagePath(result.imageThumbPath);
                            building.hasImage(true);
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },
        _exitEditMode: function () {
            var self = this;

            self._inEditMode(false);
        },
        _enterEditMode: function () {
            var self = this;

            self._inEditMode(true);
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
        _cancel: function () {
            Corium.navigation.navigateAction('building#search');
        },
        _unDelete: function (target) {
            target.isDeleted(false);
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

            self._currMenuCat(_.first(self._building().menuCategories())); //todo when have 0 ?
            self._currAlbum = ko.observable(_.first(self._building().albums()));
        },
        _addBuildingValidationExtenders: function (building) {
            var self = this;

            self._isActive.extend({ required: true });
            self._isDeleted.extend({ required: true });
            building.settlementId.extend({ required: true });
        }
    });
    return EditBuildingVM;
});
