define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'

    //src
    //'src/view_models/user_change_password_vm'
], function ($, ko, Corium
    //, UserChangePassVM
    ) {
    'use strict';

    var NavigationVM = Corium.Class.extend({
        constructor: function (items) {
            var self = this;

            function mapItems(items) {
                return items
                    .filter(function (item) {  //bim or dkh or anything else
                        if (item.visible === false) {
                            return false;
                        }

                        return true;
                    })
                    .filter(function (item) {
                        return (item.permissions || []).reduce(function (hasPermissions, permission) {
                            return hasPermissions && Corium.app.userContext.can(permission);
                        }, true);
                    }).map(function (item) {
                        var newItem = {
                            active: ko.observable(false),
                            href: item.href,
                            text: item.text,
                            icon: item.icon,
                            newTab: item.newTab,
                            items: item.items ? mapItems(item.items) : [],
                            aliases: item.aliases
                        };

                        if (item.execute) {
                            newItem.click = function () {
                                Corium.controllerCoordinator.executeAction(item.action, item.params);
                            };
                        } else {
                            newItem.click = function () {
                                return true;
                            };
                            if (item.href) {
                                newItem.href = item.href;
                            } else if (item.action) {
                                newItem.href =
                                    Corium.navigation.interpolateHref(item.action, item.params);
                            }
                        }

                        return newItem;
                    });
            }

            self.templateId = 'templates:navigation.html';
            self._items = mapItems(items);
            self._userFullName = Corium.app.userContext.userFullName;
            self._userUnitName = Corium.app.userContext.userUnitName;
            self._userUnitPosition = Corium.app.userContext.userUnitPosition;
            self._hashNavigated = self._hashNavigated.bind(self);

            Corium.events.on('navigated.NavigationService', self._hashNavigated);
        },
        dispose: function () {
            var self = this;

            Corium.events.off('navigated.NavigationService', self._hashNavigated);
        },
        _hashNavigated: function (event, args) {
            var self = this,
                i;
            for (i = 0; i < self._items.length; i++) {
                self._items[i].active(self._items[i].href === args.href ||
                    (self._items[i].aliases && self._items[i].aliases.indexOf(args.action) !== -1));
            }
        }
        //TODO move to a controller that calls it on a certain event
        //changePassword: function () {
        //    var userChangePassVM = new UserChangePassVM();
        //    Corium.dialogs.show({
        //        header: 'Промяна на паролата',
        //        acceptText: 'Промени',
        //        cancelText: 'Отказ',
        //        accepting: function (event) {
        //            event.preventDefault();
        //            userChangePassVM.save();
        //        },
        //        viewModel: userChangePassVM
        //    });
        //}
    });
    return NavigationVM;
});
