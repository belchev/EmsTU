define([
    //framework
    'framework/corium',

    //src
    'src/utils'
], function (Corium, Utils) {
    'use strict';

    var UserRepository = Corium.Repository.extend({
        constructor: function () {
        },
        getUsers: function (username, fullname, showactive) {
            var self = this,
                apiQuery,
                urlQuery;

            apiQuery = Utils.Uri.createQuery({
                'username': username,
                'fullname': fullname,
                'showactive': showactive
            });

            urlQuery = 'api/users?' + apiQuery;
            return self.get(urlQuery);
        },
        getUsersByUsername: function (username) {
            var self = this,
                apiQuery,
                urlQuery;

            apiQuery = Utils.Uri.createQuery({
                'username': username,
                'exact': true
            });

            urlQuery = 'api/users?' + apiQuery;
            return self.get(urlQuery);
        },
        getUser: function (id) {
            var self = this,
                userId = parseInt(id, 10),
                url = 'api/users/' + userId;

            return self.get(url);
        },
        changePassword: function (oldPass, newPass) {
            var self = this,
                url,
                passwords = {
                    newPassword: newPass,
                    oldPassword: oldPass
                };
            url = 'api/user/changepassword';

            return self.post(url, passwords);
        },
        save: function (user) {
            var self = this,
                id = user.userId,
                url,
                promise;

            if (id) {
                url = 'api/users/' + id;
                promise = self.put(url, user);
            } else {
                url = 'api/users';
                promise = self.post(url, user);
            }

            return promise;
        }
    });
    return UserRepository;
});
