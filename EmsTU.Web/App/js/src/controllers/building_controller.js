define([
    //libs
    'jquery',
    'knockout',
    'q',

    //framework
    'framework/corium',

    //src
    'src/repositories/building_repository',

    'src/view_models/building/buildings_list_vm',
    'src/view_models/building/buildings_new_vm',
    'src/view_models/home_vm'
], function (
    $,
    ko,
    Q,
    Corium,
    BuildingRepository,
    BuildingsVM,
    NewBuildingVM,
    HomeVM
    ) {
    'use strict';

    var BuildingController = Corium.Controller.extend({
        constructor: function () {
            Corium.Controller.prototype.constructor.call(this);
        },
        newBuilding: {
            dependencies: ['app#leftBuilding'],
            action: function (params, cancelationToken) {
                var buildingRepository = new BuildingRepository();

                return buildingRepository.newBuilding()
                    .then(function (newBuilding) {
                        if (cancelationToken.isCanceled) {
                            return;
                        }

                        return Corium.app.rootView().pageView().right(new NewBuildingVM(newBuilding));
                    });

            }
        },
        search: {
            dependencies: ['app#leftBuilding'],
            action: function (params, cancelationToken) {
                var query = params.query || {},
                    buildingRepository = new BuildingRepository(),
                    limit = query.limit ? parseInt(query.limit, 10) : 10,
                    offset = query.offset ? parseInt(query.offset, 10) : 0;

                return buildingRepository
                    .getBuildings(
                        query.n,
                        query.bt,
                        query.kt,
                        query.mt,
                        query.ot,
                        query.e,
                        limit,
                        offset
                    )
                    .then(function (b) {
                        var buildings = b.buildings,
                            buildingsCount = b.buildingsCount,
                            msg = b.msg;

                        if (cancelationToken.isCanceled) {
                            return;
                        }

                        return Corium.app.rootView().pageView().right(new BuildingsVM(
                            buildings,
                            buildingsCount,
                            msg,
                            query.n,
                            query.bt,
                            query.kt,
                            query.mt,
                            query.ot,
                            query.e,
                            limit,
                            offset
                            ));
                    });
            }
        },
        home: {
            dependencies: ['app#root'],
            action: function (params, cancelationToken) {
                if (cancelationToken.isCanceled) {
                    return;
                }

                return Corium.app.rootView().pageView(new HomeVM());
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
