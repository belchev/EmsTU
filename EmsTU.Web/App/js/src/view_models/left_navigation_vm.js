define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium'
], function ($, ko, Corium) {
    'use strict';

    var LeftNavigationVM = Corium.Class.extend({
        constructor: function (items, type) {
            var self = this;

            self.mappingFn = function mapItems(items) {
                return items.filter(function (item) {
                    return (item.permissions || []).reduce(function (hasPermissions, permission) {
                        return hasPermissions && Corium.app.userContext.can(permission);
                    }, true);
                }).map(function (item) {
                    var newItem = {
                        active: ko.observable(false),
                        action: item.action,
                        params: item.params,
                        href: item.href,
                        text: item.text,
                        icon: item.icon,
                        newTab: item.newTab,
                        items: item.items ? mapItems(item.items) : []
                    };

                    if (item.href) {
                        newItem.href = item.href;
                    } else if (item.action) {
                        newItem.href =
                            Corium.navigation.interpolateHref(item.action, item.params);
                    }

                    return newItem;
                });
            };

            self.templateId = 'templates:left_navigation.html';
            self.updateVM = self.updateVM.bind(self);

            self._items = ko.observableArray(self.mappingFn(items));
            self._type = type;
            self._userFullName = Corium.app.userContext.userFullName;
            self._hashNavigated = self._hashNavigated.bind(self);

            Corium.events.on('navigated.NavigationService', self._hashNavigated);
        },
        dispose: function () {
            var self = this;

            Corium.events.off('navigated.NavigationService', self._hashNavigated);
        },
        _hashNavigated: function (event, args) {
            var self = this,
                i,
                item;

            function isActive(item) {
                if (item.action === args.action) {
                    if (!item.params || !item.params.query || !item.params.query.dp) {
                        return !args.params || !args.params.query || !args.params.query.dp;
                    } else {
                        return args.params && args.params.query && item.params.query.dp === args.params.query.dp;
                    }
                }

                return false;
            }

            for (i = 0; i < self._items().length; i++) {
                item = self._items()[i];
                item.active(isActive(item));
            }
        },
        updateVM: function (items, type) {
            var self = this;

            self._items(self.mappingFn(items));
            self._type = type;
        }
    });
    return LeftNavigationVM;
});
