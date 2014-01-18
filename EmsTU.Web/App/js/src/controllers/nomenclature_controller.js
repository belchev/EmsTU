define([
    //libs
    'jquery',
    'knockout',
    'q',

    //framework
    'framework/corium',

    //src
    'src/repositories/nom_repository',
    'src/view_models/nomenclature/nomtypes_vm',
    'src/view_models/nomenclature/nomenclatures_vm',
    'src/view_models/nomenclature/edit_nom_vm'

], function (
    $,
    ko,
    Q,
    Corium,
    NomRepository,
    NomTypesVM,
    NomenclaturesVM,
    EditNomVM

    ) {
    'use strict';

    var NomenclatureController = Corium.Controller.extend({
        constructor: function () {
            Corium.Controller.prototype.constructor.call(this);
        },
        nomtypes: {
            dependencies: ['app#root'],
            action: function (params, cancelationToken) {
                var nomRepository = new NomRepository();

                return nomRepository.getNomTypes()
                .then(function (nomTypes) {

                    if (cancelationToken.isCanceled) {
                        return;
                    }

                    Corium.app.rootView().pageView(new NomTypesVM(nomTypes));
                });
            }
        },
        searchNoms: {
            dependencies: ['app#root'],
            action: function (params, cancelationToken) {
                var query = params.query || {},
                    nomRepository = new NomRepository(),
                    nomTypeId = params.nomTypeId,
                    limit = query.limit ? parseInt(query.limit, 10) : 10,
                    offset = query.o ? parseInt(query.o, 10) : 0;

                return nomRepository
                    .getNoms(nomTypeId, query.n, query.a, query.ia, limit, offset)
                    .then(function (data) {
                        var nomenclatures = data.returnValue,
                            nomenclaturesCount = data.totalCount;
                        if (cancelationToken.isCanceled) {
                            return;
                        }

                        Corium.app.rootView()
                            .pageView(new NomenclaturesVM(nomTypeId,
                            nomenclatures,
                            query.n,
                            query.a,
                            query.ia,
                            limit,
                            offset,
                            nomenclaturesCount));
                    });
            }
        },
        editNom: {
            dependencies: ['app#root'],
            action: function (params, cancelationToken) {
                var nomRepository = new NomRepository();

                return nomRepository.getNom(params.nomId).then(function (nom) {
                    if (cancelationToken.isCanceled) {
                        return;
                    }

                    Corium.app.rootView().pageView(new EditNomVM(nom));
                });
            }
        },
        newNom: {
            dependencies: ['app#root'],
            action: function (params) {
                var newNom = {
                    nomId: 0,
                    nomTypeId: params.nomTypeId,
                    name: '',
                    alias: '',
                    isActive: true
                };

                return Corium.app.rootView().pageView(new EditNomVM(newNom));
            }
        }
    });

    return NomenclatureController;
});
