define([
    //globals
    'window',

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
    'src/utils',
    'src/validation_utils',
    'src/view_models/upload_file_vm',
    'src/repositories/building_repository'
], function (
    window,
    $,
    ko,
    ko_mapping,
    Q,
    document,
    Corium,
    Utils,
    ValidationUtils,
    UploadFileVM,
    BuildingRepository
    ) {
    'use strict';

    var NewBuildingVM = Corium.Class.extend({
        constructor: function (newBuilding, details) {
            var self = this;

            self.templateId = 'templates:building:buildings_new.html';
            self._save = self._save.bind(self);
            self._cancel = self._cancel.bind(self);
            self._buildingImage = self._buildingImage.bind(self);
            self._deleteBuildingImage = self._deleteBuildingImage.bind(self);

            self._building = ko.observable(ko_mapping.fromJS(newBuilding));

            self._isInDialog = ko.observable(false);
            self._showErrors = ko.observable(false);
            self._saveButtonClicked = ko.observable(false);

            self._settlementId = ko.observable();
            self._buildingRqId = ko.observable();

            self._contactPhone = ko.observable();
            self._contactPhoneError = undefined;
            self._contactPhoneChanged = ko.observable(false);

            self._name = ko.observable();
            self._nameError = undefined;
            self._nameChanged = ko.observable(false);

            self._address = ko.observable();
            self._addressError = undefined;
            self._addressChanged = ko.observable(false);

            self._contactPhone.subscribe(function () {
                self._contactPhoneChanged(true);
            });
            self._contactPhoneError = ko.computed(function () {
                var contactPhone = self._contactPhone();

                if (self._saveButtonClicked() || self._contactPhoneChanged()) {
                    return !contactPhone;
                }
                else {
                    return false;
                }
            });

            self._address.subscribe(function () {
                self._addressChanged(true);
            });
            self._addressError = ko.computed(function () {
                var address = self._address();

                if (self._saveButtonClicked() || self._addressChanged()) {
                    return !address;
                }
                else {
                    return false;
                }
            });

            self._name.subscribe(function () {
                self._nameChanged(true);
            });
            self._nameError = ko.computed(function () {
                var name = self._name();

                if (self._saveButtonClicked() || self._nameChanged()) {
                    return !name;
                }
                else {
                    return false;
                }
            });

            self._buildingRepository = new BuildingRepository();
            self._addBuildingValidationExtenders();


            if (details) {
                self._buildingRqId(details.buildingRequestId());
                self._name(details.buildingName());
                self._building().webSite(details.webSite());
                self._building().contactName(details.contactName());
            }
        },
        _addBuildingValidationExtenders: function () {
            var self = this;

            self._settlementId.extend({ required: true });
        },
        _cancel: function () {
            Corium.navigation.navigateAction('building#search');
        },
        _deleteBuildingImage: function () {
            var self = this;

            self._building().imagePath(undefined);
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
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },

        _save: function () {
            var self = this,
                isFormInvalid,
                building = self._building();

            self._saveButtonClicked(true);

            if (ValidationUtils.isValid(self)) {
                isFormInvalid = self._nameError() || self._contactPhoneError() || self._addressError();

                if (!isFormInvalid) {
                    building.name(self._name());
                    building.contactPhone(self._contactPhone());
                    building.address(self._address());

                    building.settlementId(self._settlementId());

                    self._buildingRepository.save(ko_mapping.toJS(building), self._buildingRqId()).then(function (bRq) {
                        if (bRq.bRq) {
                            Corium.navigation.navigateAction('building#requests');
                        }
                        else {
                            Corium.navigation.navigateAction('building#search');
                        }
                    });
                }
            }
            else {
                self._showErrors(true);
            }

            return;
        }
    });

    return NewBuildingVM;
});
