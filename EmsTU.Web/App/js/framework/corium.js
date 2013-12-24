define([
    //libs
    'lodash',
    'jquery',

    //*globals
    'window',

    //framework
    'framework/corium_instance',
    'framework/class',
    'framework/controller',
    'framework/controller_coordinator',
    'framework/view',
    'framework/list_view',
    'framework/repository',
    'framework/navigation_service',
    'framework/dialog_service',
    'framework/view_handler'
], function (
    //libs
    _,
    $,

    //*globals
    window,

    //framework
    Corium,
    Class,
    Controller,
    ControllerCoordinator,
    View,
    ListView,
    Repository,
    NavigationService,
    DialogService,
    ViewHandler
) {
    'use strict';

    _.extend(Corium, {
        Class: Class,
        Controller: Controller,
        Repository: Repository,
        View: View,
        ListView: ListView,
        ViewHandler: ViewHandler,
        app: {},
        events: $({}),
        init: function (config) {
            var self = this;

            self.navigation = new NavigationService();
            self.navigation.init(config.navigation);

            self.controllerCoordinator = new ControllerCoordinator();
            self.controllerCoordinator.init(config.controllers);

            self.dialogs = new DialogService(config.dialogId);
        },
        start: function () {
            var self = this;
            self.navigation.start();
        },
        reload: function () {
            window.location.reload(true);
        }
    });

    return Corium;
});
