/*global require, window*/
require.config({
    name: 'main',
    baseUrl: 'app/js',
    paths: {
        templates: '../templates',

        //libraries
        jquery: 'lib/jquery-1.9.1',
        knockout: 'lib/knockout-2.2.1.debug',
        moment: 'lib/moment',
        es5shim: 'lib/es5-shim',
        chosen: 'lib/chosen.jquery',
        'datepicker.core': 'lib/bootstrap-datepicker',
        'datepicker': 'lib/bootstrap-datepicker.bg',
        q: 'lib/q',
        spin: 'lib/spin',
        bootstrap: 'lib/bootstrap',
        'jquery.iframe-transport': 'lib/jquery.iframe-transport',
        'jquery.ui.widget': 'lib/jquery.ui.widget',
        'jquery.fileupload': 'lib/jquery.fileupload',

        'lodash': 'lib/lodash.compat',
        signals: 'lib/signals',
        crossroads: 'lib/crossroads',
        hasher: 'lib/hasher',

        lightbox: 'lib/jquery.lightbox-0.5',

        //knockout plugins
        'knockout.mapping': 'lib/knockout_plugins/knockout.mapping-2.4.1.debug',

        //require plugins
        domReady: 'lib/require_plugins/domReady',

        //knockout binding handlers
        'element_handler_factory': 'knockout_binding_handlers/element_handler_factory',
        'converted_binding_handler': 'knockout_binding_handlers/converted_binding_handler',
        'read_only_handler': 'knockout_binding_handlers/read_only_handler',
        'bootstrap_handler': 'knockout_binding_handlers/bootstrap_handler',
        'chosen_handler': 'knockout_binding_handlers/chosen_handler',
        'date_picker_handler': 'knockout_binding_handlers/date_picker_handler',
        'jqeury_ui_widget_handler': 'knockout_binding_handlers/jqeury_ui_widget_handler',
        'promised_click_handler': 'knockout_binding_handlers/promised_click_handler'
    },
    shim: {
        'lightbox': {
            deps: ['jquery'],
            exports: 'jQuery.fn.lightBox',
            init: function ($) {
                'use strict';
                $('.album-photo a').lightBox();
            }
        },
        'bootstrap': {
            deps: ['jquery', 'document'],
            exports: 'jQuery.fn.dropdown',
            init: function ($, document) {
                'use strict';
                $(document).off('.data-api');
                var bsTab = $.fn.tab;

                $(document).on('click.tab.data-api', '[data-toggle="tab"], [data-toggle="pill"]', function (e) {
                    e.preventDefault();
                    bsTab.call($(this), 'show');
                });

                $(document).on('click.collapse.data-api', '[data-toggle=collapse]', function (e) {
                    var $this = $(this),
                        href,
                        target = $this.attr('data-target') ||
                        e.preventDefault() ||
                        (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, ''), //strip for ie7
                        option = $(target).data('collapse') ? 'toggle' : $this.data();
                    $this[$(target).hasClass('in') ? 'addClass' : 'removeClass']('collapsed');
                    $(target).collapse(option);
                });

                return {
                    affix: $.fn.affix.noConflict(),
                    alert: $.fn.alert.noConflict(),
                    button: $.fn.button.noConflict(),
                    carousel: $.fn.carousel.noConflict(),
                    //collapse: $.fn.collapse.noConflict(),
                    dropdown: $.fn.dropdown.noConflict(),
                    modal: $.fn.modal.noConflict(),
                    popover: $.fn.popover.noConflict(),
                    scrollspy: $.fn.scrollspy.noConflict(),
                    tab: $.fn.tab.noConflict(),
                    tooltip: $.fn.tooltip.noConflict(),
                    typeahead: $.fn.typeahead.noConflict()
                };
            }
        },
        'datepicker.core': {
            deps: ['jquery'],
            exports: 'jQuery.fn.datepicker'
        },
        'datepicker': {
            deps: ['jquery', 'datepicker.core'],
            exports: 'jQuery.fn.datepicker.dates.bg',
            init: function ($, datepicker) {
                'use strict';

                function DatepickerConstructor($element, options) {
                    datepicker.Constructor.call(
                        this,
                        $element,
                        $.extend(
                            {},
                            datepicker.defaults,
                            options,
                            { language: 'bg', weekStart: 1 }));
                }
                DatepickerConstructor.prototype = datepicker.Constructor.prototype;
                return DatepickerConstructor;
            }
        },
        'chosen': {
            deps: ['jquery'],
            exports: 'Chosen',
            init: function ($) {
                /*global Chosen*/
                'use strict';

                function ChosenConstructor($element, options) {
                    options = $.extend(
                        {},
                        options,
                        {
                            placeholder_text: ' ',
                            no_results_text: 'Няма започващи с'
                        });
                    Chosen.call(this, $element[0], options);
                }
                ChosenConstructor.prototype = Chosen.prototype;
                return ChosenConstructor;
            }
        }
    },
    enforceDefine: true,
    //the number of seconds to wait before giving up on loading a script, debugging purposes only
    waitSeconds: 100
});

// Update the require configuration with a separate method in debug
// The optimizer will read just the first occurrence of require.config()
// and use that as the config in the specified mainConfigFile and skip
// the options bellow.
require.config({
    baseUrl: window.ems.rootUrl + 'app/js',
    urlArgs: "bust=" + (new Date()).getTime()
});

define('window', [], function () {
    /*global window*/
    'use strict';
    return window;
});
define('document', ['domReady!'], function (document) {
    'use strict';
    return document;
});
define('alert', [], function () {
    /*global alert*/
    'use strict';
    return alert;
});
define('console', [], function () {
    /*global console*/
    'use strict';
    if (typeof console !== 'undefined') {
        return console;
    }
    return undefined;
});
define('XMLSerializer', [], function () {
    /*global XMLSerializer*/
    'use strict';
    if (typeof XMLSerializer !== 'undefined') {
        return XMLSerializer;
    }
    return undefined;
});
define('DOMParser', [], function () {
    /*global DOMParser*/
    'use strict';
    if (typeof DOMParser !== 'undefined') {
        return DOMParser;
    }
    return undefined;
});
define('ActiveXObject', [], function () {
    /*global ActiveXObject*/
    'use strict';
    if (typeof ActiveXObject !== 'undefined') {
        return ActiveXObject;
    }
    return undefined;
});


define([
    //libs
    'jquery',
    'knockout',
    'es5shim', //POLUTES THE GLOBAL SPACE!!
    'lodash',

    //*globals
    'document',

    //framework
    'framework/corium',

    //knockout binding handlers
    'converted_binding_handler',
    'read_only_handler',
    'bootstrap_handler',
    'chosen_handler',
    'date_picker_handler',
    'jqeury_ui_widget_handler',
    'promised_click_handler',

    //src
    'src/validation_extenders',
    'src/utils',

    //todo
    'src/controllers/app_controller',
    'src/controllers/user_controller',
    'src/controllers/building_controller',

    'src/nomenclature_cache_service',
    'src/nomenclature_context_handler',
    'src/tab_cache_service'
], function (
    //libs
    $,
    ko,
    es5shim,
    _,

    //*globals
    document,

    //framework
    Corium,

    //knockout binding handlers
    ConvertedBindingHandler,
    ReadOnlyHandler,
    BootstrapHandler,
    ChosenHandler,
    DatePickerHandler,
    JQueryUIWidgetHandler,
    PromisedClickHandler,

    //src
    ValidationExtenders,
    Utils,
    //todo
    AppController,
    UserController,
    BuildingController,

    NomenclatureCacheService,
    NomenclatureContextHandler,
    TabCacheService
) {
    'use strict';

    //enforce usage through a define call
    $.noConflict(); //leave the jQuery variable as it is used by KnockoutJS
    _.noConflict();

    ko.bindingHandlers.view = new Corium.ViewHandler();
    ko.virtualElements.allowedBindings.view = true;

    ko.bindingHandlers.chosen = new ChosenHandler();
    ko.bindingHandlers.datePicker =
    new DatePickerHandler({
        format: 'dd.mm.yyyy',
        readConverterFunc: Utils.Date.parseAPI,
        writeConverterFunc: Utils.Date.formatAPI
    });
    ko.bindingHandlers.readOnly = new ReadOnlyHandler();
    ko.bindingHandlers.dateText = new ConvertedBindingHandler('text', Utils.Date.format);
    ko.bindingHandlers.dateWithHourText =
        new ConvertedBindingHandler('text', Utils.Date.formatWithHour);
    ko.bindingHandlers.dateValue =
        new ConvertedBindingHandler('value', Utils.Date.format,
        function (strInput) {
            return Utils.Date.formatAPI(Utils.Date.parseInput(strInput));
        });
    ko.bindingHandlers.dateWithHourValue =
        new ConvertedBindingHandler('value', Utils.Date.formatWithHour,
        function (strInput) {
            return Utils.Date.formatAPI(Utils.Date.parseWithHourInput(strInput));
        });
    ko.bindingHandlers.coordinateValue =
        new ConvertedBindingHandler('value',
            function (f) {
                return Utils.Number.formatFloat(f, 4);
            },
            Utils.Number.parseFloat
        );
    ko.bindingHandlers.intValue =
        new ConvertedBindingHandler('value',
            function (i) {
                return Utils.Number.formatInt(i);
            },
            Utils.Number.parseInt
        );
    ko.bindingHandlers.decimalValue =
        new ConvertedBindingHandler('value',
            function (f) {
                return Utils.Number.formatFloat(f, 2);
            },
            Utils.Number.parseFloat
        );
    ko.bindingHandlers.trimmedValue =
        new ConvertedBindingHandler('value',
            function (s) {
                return s;
            },
            function (s) {
                return (s || '').trim() || undefined;
            }
        );

    ko.bindingHandlers.nomenclatureContext = new NomenclatureContextHandler(false, false);
    ko.bindingHandlers.childNomenclatureContext = new NomenclatureContextHandler(true, false);
    ko.bindingHandlers.multiNomenclatureContext = new NomenclatureContextHandler(false, true);
    ko.bindingHandlers.bootstrap = new BootstrapHandler();
    ko.bindingHandlers.jqueryUI = new JQueryUIWidgetHandler();
    ko.bindingHandlers.promisedClickEms = new PromisedClickHandler({
        lines: 10,
        length: 5,
        width: 2,
        radius: 4,
        color: '#000'
    });
    ko.bindingHandlers.promisedClickWhite = new PromisedClickHandler({
        lines: 10,
        length: 5,
        width: 2,
        radius: 4,
        color: '#fff'
    });
    ko.bindingHandlers.promisedClickWhiteMini = new PromisedClickHandler({
        lines: 10,
        length: 3,
        width: 2,
        radius: 2,
        color: '#fff'
    });
    ko.bindingHandlers.promisedClickBlackMini = new PromisedClickHandler({
        lines: 10,
        length: 3,
        width: 2,
        radius: 2,
        color: '#000'
    });

    (new ValidationExtenders()).init();

    Corium.app.userContext = {
        appName: window.ems.config.app,
        userFullName: window.ems.config.userFullName,
        hasPassword: window.ems.config.userHasPassword,
        //todo
        can: function () {
            var action, object, ao;
            if (arguments.length === 1) {
                ao = arguments[0].split('#');
                action = ao[1];
                object = ao[0];
            }

            if (arguments.length === 2) {
                action = arguments[0];
                object = arguments[1];
            }

            return window.ems.config.permissions.indexOf(object + '#*') !== -1 ||
                window.ems.config.permissions.indexOf(object + '#' + action) !== -1;
        }
    };

    Corium.app.permanentViewModels = [];

    Corium.app.config = {
        navigationData: [
            {
                text: 'Заведения',
                action: 'building#search',
                icon: 'icon-tasks',
                aliases: [
                    'building#search',
                    'building#edit'
                ]
            }, {
                text: 'Ново заведение',
                action: 'building#newBuilding',
                //params: { docEntryTypeId: 1 }, //todo remove
                icon: 'icon-plus',
                permissions: ['sys#admin'],
                aliases: [
                    'building#newBuilding'
                ]
            }, {
                text: 'Чакащи заявки',
                action: 'building#requests',
                icon: 'icon-time',
                permissions: ['sys#admin'],
                aliases: [
                    'building#requests'
                ]
            }, {
                text: 'Администриране',
                icon: 'icon-wrench',
                permissions: ['sys#admin'],
                items: [
                    { text: 'Потребители', action: 'user#search' }
                ],
                aliases: [
                    'user#newUser',
                    'user#edit',
                    'user#search'
                ]
            },
            {
                text: 'Помощ',
                items: [
                    { text: 'Табло за съобщения', action: 'building#home' }
                ],
                aliases: [
                    'building#home'
                ]
            }
        ],
        leftBuildingNavigationData: [
            {
                text: 'Последни',
                action: 'building#search'
            },
            {
                text: 'Всички',
                action: 'building#homeTest'
            }
        ]
    };

    Corium.app.services = {
        tabCache: new TabCacheService({}, 5 * 60 /*cacheTimeoutSeconds*/),
        nomenclatureCache: new NomenclatureCacheService({
            /* Special */
            'yesNoOptions': 'api/noms/yesNoOptions', //todo maybe replace with something else

            'districts': 'api/noms/districts',
            'municipalities': 'api/noms/municipalities?districtId={0}',
            'settlements': 'api/noms/settlements?municipalityId={0}',

            'buildingTypes': 'api/noms/noms?type=BuildingTypes',
            'kitchenTypes': 'api/noms/noms?type=KitchenTypes',
            'musicTypes': 'api/noms/noms?type=MusicTypes',
            'occasionTypes': 'api/noms/noms?type=OccasionTypes',
            'extras': 'api/noms/noms?type=Extras',
            'paymentTypes': 'api/noms/noms?type=PaymentTypes',

            'roles': 'api/noms/roles'
        }, 5 * 60 /*cacheTimeoutSeconds*/)
    };

    Corium.init({
        controllers: {
            app: AppController,
            user: UserController,
            building: BuildingController
            //corr: CorrController,
            //dkhNomenclature: DkhNomenclatureController,
        },
        navigation: {
            rootAction: 'building#home',
            rescueAction: 'app#unknown',
            routes: [{
                pattern: 'unknown',
                action: 'app#unknown'
            }, {
                pattern: 'u/n',
                action: 'user#newUser'
            }, {
                pattern: 'u/{userId}',
                action: 'user#edit'
            }, {
                pattern: 'u/:?query:',
                action: 'user#search'
            },
            //buildings
            {
                pattern: 'b/:?query:',
                action: 'building#search'
            }, {
                pattern: 'b/n',
                action: 'building#newBuilding'
            }, {
                pattern: 'b/{buildingId}',
                action: 'building#edit'
            }, {
                pattern: 'ht',
                action: 'building#homeTest'
            }, {
                pattern: 'h',
                action: 'building#home'
            },
            //buildingRqs
            {
                pattern: 'rq/:?query:',
                action: 'building#requests'
            }
            ]
        },
        dialogId: '#dialog'
    });

    Corium.events.one('navigated.NavigationService', function () {
        //hide initial loader
        $('#initial-loader', document).remove();
    });

    Corium.events.on('error.Corium', function (event, error) {
        if (error.status === 403) {
            Corium.navigation.navigateAction('app#unknown');
        } else if (error.status === 401) {
            Corium.reload();
        } else if (error.status === 200) {
            if (!!error.responseText.match("Вход в системата")) {
                if (window) {
                    window.location = '~/login';
                }
            }
        } else {
            if (console) {
                console.log(error);
            }
        }
    });

    Corium.app.rootView = new Corium.View();
    ko.applyBindings(Corium.app, $('#root', document)[0]);

    Corium.start();
});
