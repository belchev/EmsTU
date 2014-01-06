define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium',

    //src
    'src/repositories/user_repository'
], function ($, ko, Corium, UserRepository) {
    'use strict';

    var UserChangePassVM = Corium.Class.extend({
        constructor: function () {
            var self = this;
            self.templateId = 'templates:user_change_pass.html';

            self._oldPassword = ko.observable();
            self._oldPasswordChanged = ko.observable(false);
            self._passwordErrorFromServer = ko.observable(false);
            self._oldPassword.subscribe(function () {
                self._passwordErrorFromServer(false);
                self._oldPasswordChanged(true);
            });

            self._newPassword = ko.observable();
            self._newPasswordChanged = ko.observable(false);
            self._newPasswordError = ko.computed(function () {
                if (self._newPasswordChanged()) {
                    return (self._newPassword() || '').length < 8;
                }
                return false;
            });
            self._newPassword.subscribe(function () {
                self._newPasswordChanged(true);
            });

            self._secondPassword = ko.observable();
            self._secondPasswordChanged = ko.observable(false);
            self._secondPasswordError = ko.computed(function () {
                if (self._secondPasswordChanged()) {
                    return self._newPassword() !== self._secondPassword();
                }
                return false;
            });
            self._secondPassword.subscribe(function () {
                self._secondPasswordChanged(true);
            });

            self.save = self.save.bind(self);
        },
        save: function () {
            var self = this,
                userRepository = new UserRepository();

            self._newPasswordChanged(true);
            self._secondPasswordChanged(true);

            if (!self._newPasswordError() &&
                !self._secondPasswordError()
            ) {
                userRepository.changePassword(self._oldPassword(), self._newPassword())
                    .then(function () {
                        Corium.dialogs.hide();
                    }, function (jqXHR) {
                        if (jqXHR.status === 405) {
                            self._passwordErrorFromServer(true);
                        } else {
                            throw jqXHR;
                        }
                    });
            }
        }
    });

    return UserChangePassVM;
});
