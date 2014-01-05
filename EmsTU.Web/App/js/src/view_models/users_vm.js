define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function ($, ko, Corium) {
    'use strict';

    var UsersVM = Corium.Class.extend({
        constructor: function (users, searchUsername, searchFullname, showActive) {
            var self = this;

            self.templateId = 'templates:users.html';
            self.editUser = self.editUser.bind(self);
            self.newUser = self.newUser.bind(self);
            self.search = self.search.bind(self);

            self._users = ko.observableArray(users.map(function (value) {
                return {
                    userId: value.userId,
                    username: value.username,
                    fullname: value.fullname,
                    role: value.role.name,
                    isActive: value.isActive
                };
            }));

            self._username = ko.observable(searchUsername);
            self._fullname = ko.observable(searchFullname);
            self._hasUsers = ko.observable(!!users);
            self._showActive = ko.observable(showActive);
        },
        editUser: function (user) {
            var userId = user.userId;

            Corium.navigation.navigateAction('user#edit', { userId: userId });
        },
        newUser: function () {
            Corium.navigation.navigateAction('user#newUser');
        },
        search: function () {
            var self = this,
                username = self._username() || '',
                fullname = self._fullname() || '';

            Corium.navigation.navigateAction(
                'user#search',
                {
                    query: {
                        'username': username,
                        'fullname': fullname,
                        'active': self._showActive()
                    }
                });
        }
    });

    return UsersVM;
});