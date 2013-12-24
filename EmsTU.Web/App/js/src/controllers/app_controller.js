define([
    //libs
    'jquery',
    'knockout',

    //framework
    'framework/corium',

    //src
    'src/utils',
    'src/view_models/navigation_vm',
    'src/view_models/left_navigation_vm'
], function (
    //libs
    $,
    ko,

    //framework
    Corium,

    //src
    Utils,
    NavigationVM,
    LeftNavigationVM
) {
    'use strict';

    var AppController = Corium.Controller.extend({
        constructor: function () {
            var self = this;

            Corium.Controller.prototype.constructor.call(self);
        },
        root: {
            action: function () {
                if (Corium.app.rootView()) {
                    return;
                }

                Corium.app.rootView({
                    templateId: 'templates:root.html',
                    navigationView: new Corium.View(new NavigationVM(Corium.app.config.navigationData)),
                    pageView: new Corium.View(undefined)
                });
            }
        },
        leftBuilding: {
            dependencies: ['app#root'],
            action: function () {
                var pageVM = Corium.app.rootView().pageView();

                if (pageVM && pageVM.left && pageVM.right) {
                    if (pageVM.left()._type !== 'building') {
                        pageVM.left().updateVM(Corium.app.config.leftBuildingNavigationData, 'building');
                    }

                    return;
                }

                Corium.app.rootView().pageView({
                    templateId: 'templates:page.html',
                    left: new Corium.View(new LeftNavigationVM(Corium.app.config.leftBuildingNavigationData, 'building')),
                    right: new Corium.View(undefined)
                });
            }
        },
        unknown: {
            dependencies: ['app#root'],
            action: function () {
                Corium.app.rootView().pageView({ templateId: 'templates:unknown.html' });
            }
        }
    });
    return AppController;
});