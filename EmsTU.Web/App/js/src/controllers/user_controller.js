define([
    //libs
    'jquery',
    'knockout',
    'q',

    //framework
    'framework/corium',

    //src
    'src/view_models/users_vm',
    'src/view_models/edit_user_vm',
    'src/repositories/user_repository'
], function ($, ko, Q, Corium, UsersVM, EditUserVM, UserRepository) {
    'use strict';

    var UserController = Corium.Controller.extend({
        constructor: function () {
            Corium.Controller.prototype.constructor.call(this);
        },
        search: {
            dependencies: ['app#root'],
            action: function (params, cancelationToken) {
                var query = params.query || {},
                    userRepository = new UserRepository();

                return userRepository
                    .getUsers(query.username, query.fullname, query.active)
                    .then(function (users) {
                        if (cancelationToken.isCanceled) {
                            return;
                        }

                        Corium.app.rootView()
                            .pageView(new UsersVM(users,
                            query.username,
                            query.fullname,
                            query.active));
                    });
            }
        },
        edit: {
            dependencies: ['app#root'],
            action: function (params, cancelationToken) {
                var userRepository = new UserRepository();

                return userRepository
                    .getUser(params.userId)
                    .then(function (user) {
                        if (cancelationToken.isCanceled) {
                            return;
                        }

                        Corium.app.rootView().pageView(new EditUserVM(user));
                    });

            }
        },
        newUser: {
            dependencies: ['app#root'],
            action: function () {
                Corium.app.rootView().pageView(new EditUserVM());
            }
        }
    });

    return UserController;
});
