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
            self._removeNomType = self._removeNomType.bind(self);
            self._getBuildingTypes = self._getBuildingTypes.bind(self);
            self._editBuildingTypes = self._editBuildingTypes.bind(self);
            self._getKitchenTypes = self._getKitchenTypes.bind(self);
            self._editKitchenTypes = self._editKitchenTypes.bind(self);
            self._getMusicTypes = self._getMusicTypes.bind(self);
            self._editMusicTypes = self._editMusicTypes.bind(self);
            self._getOccasionTypes = self._getOccasionTypes.bind(self);
            self._editOccasionTypes = self._editOccasionTypes.bind(self);
            self._getPaymentTypes = self._getPaymentTypes.bind(self);
            self._editPaymentTypes = self._editPaymentTypes.bind(self);
            self._getExtras = self._getExtras.bind(self);
            self._editExtras = self._editExtras.bind(self);
            self._currentTab = ko.observable(Corium.app.services.tabCache.loadTab(Corium.navigation._navigator.getHash(), 'buildingInfo'));

            self._buildingRepository = new BuildingRepository();

            self._inEditMode = ko.observable(inEditMode);
            self._isInDialog = ko.observable(false);
            self._showErrors = ko.observable(false);
            self._saveButtonClicked = ko.observable(false);
            self._isActive = ko.observable(building.isActive ? '1' : '2')
            self._isDeleted = ko.observable(building.isDeleted ? '1' : '2')

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

            self._deleteFile = self._deleteFile.bind(self);


            if (inEditMode) {
                self._enterEditMode();
            }
        },
        _deleteFile: function () {
            var self = this;

            self._building().imagePath('app\\img\\nopic.jpg');
            self._building().hasLogo(false);
        },
        _editExtras: function () {
            var self = this,
                nomPopListVM = new NomPopListVM('Extras');

            Corium.dialogs.show({
                header: 'Избор на екстра',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = nomPopListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._building().extras(), function (item) {
                            return resultItem.nomId === item.nomId();
                        });

                        if (!match) {
                            self._building().extras.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: nomPopListVM
            });
        },
        _getExtras: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    extras = self._building().extras();

                ko.utils.arrayForEach(extras, function (extra) {
                    if (extra.isDeleted() === false) {
                        result.push(extra);
                    }
                });

                return result;
            });
        },
        _editPaymentTypes: function () {
            var self = this,
                nomPopListVM = new NomPopListVM('PaymentTypes');

            Corium.dialogs.show({
                header: 'Избор на тип плащане',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = nomPopListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._building().paymentTypes(), function (item) {
                            return resultItem.nomId === item.nomId();
                        });

                        if (!match) {
                            self._building().paymentTypes.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: nomPopListVM
            });
        },
        _getPaymentTypes: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    paymentTypes = self._building().paymentTypes();

                ko.utils.arrayForEach(paymentTypes, function (paymentType) {
                    if (paymentType.isDeleted() === false) {
                        result.push(paymentType);
                    }
                });

                return result;
            });
        },
        _editOccasionTypes: function () {
            var self = this,
                nomPopListVM = new NomPopListVM('OccasionTypes');

            Corium.dialogs.show({
                header: 'Избор на повод',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = nomPopListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._building().occasionTypes(), function (item) {
                            return resultItem.nomId === item.nomId();
                        });

                        if (!match) {
                            self._building().occasionTypes.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: nomPopListVM
            });
        },
        _getOccasionTypes: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    occasionTypes = self._building().occasionTypes();

                ko.utils.arrayForEach(occasionTypes, function (occasionType) {
                    if (occasionType.isDeleted() === false) {
                        result.push(occasionType);
                    }
                });

                return result;
            });
        },
        _editMusicTypes: function () {
            var self = this,
                nomPopListVM = new NomPopListVM('MusicTypes');

            Corium.dialogs.show({
                header: 'Избор на тип музика',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = nomPopListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._building().musicTypes(), function (item) {
                            return resultItem.nomId === item.nomId();
                        });

                        if (!match) {
                            self._building().musicTypes.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: nomPopListVM
            });
        },
        _getMusicTypes: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    musicTypes = self._building().musicTypes();

                ko.utils.arrayForEach(musicTypes, function (musicType) {
                    if (musicType.isDeleted() === false) {
                        result.push(musicType);
                    }
                });

                return result;
            });
        },
        _editKitchenTypes: function () {
            var self = this,
                nomPopListVM = new NomPopListVM('KitchenTypes');

            Corium.dialogs.show({
                header: 'Избор на тип кухня',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = nomPopListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._building().kitchenTypes(), function (item) {
                            return resultItem.nomId === item.nomId();
                        });

                        if (!match) {
                            self._building().kitchenTypes.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: nomPopListVM
            });
        },
        _getKitchenTypes: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    kitchenTypes = self._building().kitchenTypes();

                ko.utils.arrayForEach(kitchenTypes, function (kitchenType) {
                    if (kitchenType.isDeleted() === false) {
                        result.push(kitchenType);
                    }
                });

                return result;
            });
        },
        _editBuildingTypes: function () {
            var self = this,
                nomPopListVM = new NomPopListVM('BuildingTypes');

            Corium.dialogs.show({
                header: 'Избор на тип заведение',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = nomPopListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._building().buildingTypes(), function (item) {
                            return resultItem.nomId === item.nomId();
                        });

                        if (!match) {
                            self._building().buildingTypes.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: nomPopListVM
            });
        },
        _getBuildingTypes: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    buildingTypes = self._building().buildingTypes();

                ko.utils.arrayForEach(buildingTypes, function (buildingType) {
                    if (buildingType.isDeleted() === false) {
                        result.push(buildingType);
                    }
                });

                return result;
            });
        },
        _removeNomType: function (target) {
            target.isDeleted(true);
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
