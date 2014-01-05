define([
    //libs
    'jquery',
    'knockout',
    'q',

    //framework
    'framework/corium',

    //src
    'src/validation_utils',
    'src/repositories/user_repository'
], function ($, ko, Q, Corium, ValidationUtils, UserRepository) {
    'use strict';

    var EditUserVM = Corium.Class.extend({
        constructor: function (user) {
            var self = this;

            self.templateId = 'templates:edit_user.html';
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

            //self._checkedRoles = undefined;
            //self._roles = roles;

            self._roleId = ko.observable();

            self.save = self.save.bind(self);
            self.cancel = self.cancel.bind(self);

            self._showErrors = ko.observable(false);

            if (user) {
                self._user = user;
                self._user.hasPassword = true;
                self._roleId(user.roleId);
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
            }

            self._setPassword = ko.observable(self._user.hasPassword);

            //self._checkedRoles = ko.observableArray(self._user.roles.map(function (value) {
            //    return value.roleId.toString();
            //}));

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
                        //self._user.roles = self._roles.filter(function (role) {
                        //    var checkedRoles = self._checkedRoles(),
                        //        checkedRoleId,
                        //        index;

                        //    for (index = 0; index < checkedRoles.length; index++) {
                        //        checkedRoleId = parseInt(checkedRoles[index], 10);

                        //        if (role.roleId === checkedRoleId) {
                        //            return true;
                        //        }
                        //    }
                        //    return false;
                        //});
                        self._user.roleId = self._roleId();

                        userRepository.save(self._user).then(function () {
                            Corium.navigation.navigateAction('user#search');
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