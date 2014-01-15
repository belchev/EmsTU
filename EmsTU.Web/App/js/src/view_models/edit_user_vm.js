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
    'src/repositories/user_repository',
    'src/view_models/building/buildings_pop_list_vm'
], function ($, ko, ko_mapping, Q, Corium, ValidationUtils, UserRepository, BuildingListVM) {
    'use strict';

    var EditUserVM = Corium.Class.extend({
        constructor: function (user, details) {
            var self = this;

            self.templateId = 'templates:edit_user.html';
            self._addBuilding = self._addBuilding.bind(self);
            self._editBuildings = self._editBuildings.bind(self);
            self._getBuildings = self._getBuildings.bind(self);
            self._removeBuilding = self._removeBuilding.bind(self);
            self.save = self.save.bind(self);
            self.cancel = self.cancel.bind(self);

            self._isEdit = !!user;
            self._user = undefined;

            self._password = ko.observable();
            self._passwordError = undefined;
            self._passwordChanged = ko.observable(false);
            self._setPassword = undefined;

            self._secondPassword = ko.observable();
            self._secondPasswordError = undefined;
            self._secondPasswordChanged = ko.observable(false);
            self._email = ko.observable();
            self._emailError = undefined;
            self._emailChanged = ko.observable(false);
            self._username = ko.observable();
            self._usernameError = undefined;
            self._usernameExistsError = ko.observable(false);
            self._usernameChanged = ko.observable(false);

            self._saveButtonClicked = ko.observable(false);

            self._roleId = ko.observable();
            self._buildingRqId = ko.observable();

            self._showErrors = ko.observable(false);

            self._buildings = ko.observableArray([]);

            if (user) {
                self._user = user;
                self._user.hasPassword = true;
                self._roleId(user.roleId);

                user.buildings.map(function (value) {
                    self._buildings.push(ko_mapping.fromJS(value));
                });
            } else {
                self._user = {
                    userId: undefined,
                    username: '',
                    fullname: '',
                    email: '',
                    password: '',
                    hasPassword: true,
                    notes: '',
                    isActive: false,
                    role: undefined
                };

                if (details) {
                    self._buildingRqId(details.buildingRequestId());
                    self._user.username = details.userName();
                    self._user.fullname = details.contactName();
                    self._user.email = details.email();
                    self._roleId(2); //todo get with alias
                    self._user.isActive = true;
                }
            }

            self._setPassword = ko.observable(self._user.hasPassword);
            self._username(self._user.username);
            self._email(self._user.email);

            self._username.subscribe(function () {
                self._usernameChanged(true);
                self._usernameExistsError(false);
            });
            self._password.subscribe(function () {
                self._passwordChanged(true);
            });
            self._secondPassword.subscribe(function () {
                self._secondPasswordChanged(true);
            });
            self._email.subscribe(function () {
                self._emailChanged(true);
            });

            self._emailError = ko.computed(function () {
                var email = self._email(),
                    re = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

                if (self._saveButtonClicked() || self._emailChanged()) {
                    return re.test(email) === false;
                }
                else {
                    return false;
                }
            });
            self._usernameError = ko.computed(function () {
                var username = self._username();

                if (self._saveButtonClicked() || self._usernameChanged()) {
                    return (/^[а-яА-Яa-zA-Z0-9_\.]{5,}$/).test(username) === false;
                }
                else {
                    return false;
                }
            });
            self._passwordError = ko.computed(function () {
                var password = self._password() || '',
                    shouldValidatePassword = self._setPassword() &&
                        (!self._isEdit || (self._isEdit && (password || !self._user.hasPassword)));

                if (shouldValidatePassword &&
                    password.length < 5 &&
                    (self._saveButtonClicked() || self._passwordChanged())) {
                    return true;
                } else {
                    return false;
                }
            });
            self._secondPasswordError = ko.computed(function () {
                var password = self._password() || '',
                    secondPass = self._secondPassword() || '';

                if (password !== secondPass &&
                    (self._saveButtonClicked() || self._secondPasswordChanged())) {
                    return true;
                }
                else {
                    return false;
                }
            });

            self._addUserValidationExtenders();
        },
        _addBuilding: function () {
            var self = this,
                building = {
                    buildingId: 1,
                    name: '',
                    isSelected: true,
                    isNew: true,
                    isDeleted: false
                };

            self._buildings().push(ko_mapping.fromJS(building));
        },
        _editBuildings: function () {
            var self = this,
                buildingListVM = new BuildingListVM();

            buildingListVM._addDelegate(self._addBuilding);

            Corium.dialogs.show({
                header: 'Избор на заведения',
                acceptText: 'Избор',
                cancelText: 'Отказ',
                width: 800,
                height: 500,
                accepting: function (event) {
                    event.preventDefault();

                    var result = buildingListVM.getSelected();
                    ko.utils.arrayForEach(result, function (resultItem) {
                        var match = ko.utils.arrayFirst(self._buildings(), function (item) {
                            return resultItem.buildingId === item.buildingId();
                        });

                        if (!match) {
                            self._buildings.push(ko_mapping.fromJS(resultItem));
                        } else {
                            if (match.isDeleted() === true) {
                                match.isDeleted(false);
                            }
                        }
                    });

                    Corium.dialogs.hide();
                },
                viewModel: buildingListVM
            });
        },
        _getBuildings: function () {
            var self = this;

            return ko.computed(function () {
                var result = [],
                    buildings = self._buildings();

                ko.utils.arrayForEach(buildings, function (building) {
                    if (building.isDeleted() === false) {
                        result.push(building);
                    }
                });

                return result;
            });
        },
        _removeBuilding: function (target) {
            target.isDeleted(true);
        },
        _addUserValidationExtenders: function () {
            var self = this;

            self._roleId.extend({ required: true });
        },
        save: function () {
            var self = this,
                userRepository = new UserRepository(),
                username = self._username(),
                isFormInvalid,
                usernameExistsPromise;

            self._saveButtonClicked(true);
            if (ValidationUtils.isValid(self)) {
                if (self._isEdit) {
                    usernameExistsPromise = Q.resolve(false);
                } else {
                    usernameExistsPromise = userRepository.getUsersByUsername(username)
                        .then(function (users) {
                            return users.length > 0;
                        });
                }

                usernameExistsPromise.then(function (usernameExists) {
                    if (usernameExists && username !== '') {
                        self._usernameExistsError(true);
                    }

                    isFormInvalid = (self._setPassword() &&
                        (self._passwordError() || self._secondPasswordError())) ||
                        self._usernameError() || self._usernameExistsError() ||
                        self._emailError();

                    if (!isFormInvalid) {
                        self._user.username = self._username();
                        self._user.email = self._email();
                        self._user.password = self._setPassword() ?
                            self._password() || '' :
                            '';
                        self._user.hasPassword = self._setPassword();

                        self._user.buildings = ko_mapping.toJS(self._buildings);
                        self._user.roleId = self._roleId();

                        userRepository.save(self._user, self._buildingRqId()).then(function (bRq) {
                            if (bRq.bRq) {
                                Corium.navigation.navigateAction('building#requests');
                            }
                            else {
                                Corium.navigation.navigateAction('user#search');
                            }
                        });
                    }
                });
            }
            else {
                self._showErrors(true);
            }
        },
        cancel: function () {
            Corium.navigation.navigateAction('user#search');
        }
    });
    return EditUserVM;
});