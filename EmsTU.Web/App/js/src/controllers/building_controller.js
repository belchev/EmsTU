define([
    //libs
    'jquery',
    'knockout',
    'q',

    //framework
    'framework/corium',

    //src
    'src/view_models/home_vm'
], function (
    $,
    ko,
    Q,
    Corium,
    HomeVM
    ) {
    'use strict';

    var BuildingController = Corium.Controller.extend({
        constructor: function () {
            Corium.Controller.prototype.constructor.call(this);
        },
        home: {
            dependencies: ['app#leftBuilding'],
            action: function (params, cancelationToken) {
                if (cancelationToken.isCanceled) {
                    return;
                }

                return Corium.app.rootView().pageView().right(new HomeVM());
            }
        },
        homeTest: {
            dependencies: ['app#leftBuilding'],
            action: function (params, cancelationToken) {
                if (cancelationToken.isCanceled) {
                    return;
                }

                return Corium.app.rootView().pageView().right(new HomeVM());
            }
        }

    });

    return BuildingController;
});
