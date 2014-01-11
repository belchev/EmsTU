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
    'src/view_models/upload_file_vm',
    'src/repositories/building_repository'

    ////src
    //'src/utils'
], function (
    window,
    $,
    ko,
    ko_mapping,
    Q,
    document,
    Corium,
    UploadFileVM,
    BuildingRepository
    ) {
    'use strict';

    var NewBuildingVM = Corium.Class.extend({
        constructor: function (newBuilding) {
            var self = this;

            self.templateId = 'templates:building:buildings_new.html';
            self._save = self._save.bind(self);
            self._cancel = self._cancel.bind(self);
            self._chooseFile = self._chooseFile.bind(self);
            self._deleteFile = self._deleteFile.bind(self);

            self._building = ko.observable(ko_mapping.fromJS(newBuilding));

            self._isInDialog = ko.observable(false);

            self._buildingRepository = new BuildingRepository();
        },
        _cancel: function () {
            Corium.navigation.navigateAction('building#search');
        },
        _deleteFile: function () {
            var self = this;

            self._building().imagePath(undefined);
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
                            Corium.dialogs.hide();
                        }
                    });
                },
                viewModel: uploadFileVM
            });
        },

        _save: function () {
            //var self = this;

            return;
        }
    });

    return NewBuildingVM;
});
