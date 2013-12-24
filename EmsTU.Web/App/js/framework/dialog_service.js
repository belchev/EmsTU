define([
    //libs
    'q',
    'jquery',
    'knockout',
    'bootstrap',

    //framework
    'framework/view',
    'framework/class'
], function (Q, $, ko, Bootstrap, View, Class) {
    'use strict';

    var DialogService = Class.extend({
        constructor: function (dialogId) {
            var self = this;

            self._dialogId = dialogId;
            self._acceptClicked = undefined;
            self._modal = undefined;
            self._$modal = undefined;
            self._$modalBody = undefined;
            self._header = undefined;
            self._acceptText = undefined;
            self._acceptDisabled = undefined;
            self._cancelText = undefined;
            self._accepting = undefined;
            self._accepted = undefined;
            self._canceled = undefined;
            self._view = undefined;
            self._hidingDeferred = undefined;
            self._sizeComputed = undefined;
            self._headerComputed = undefined;
            self._acceptTextComputed = undefined;
            self._acceptDisabledComputed = undefined;
            self._cancelTextComputed = undefined;

            self._accept = self._accept.bind(self);
            self._hidden = self._hidden.bind(self);
        },
        show: function (options) {
            var self = this,
                width = options.width || 530,
                height = options.height || 250,
                bodyPadding = 15;//from the bootstrap css

            if (!self._modal) {
                self._init();
            }

            if (self._modal.isShown) {
                self.hide().then(function () {
                    self.show(options);
                });
                return;
            }

            self._sizeComputed = ko.computed(function () {
                var w = ko.utils.unwrapObservable(width),
                    h = ko.utils.unwrapObservable(height);

                self._$modal.css({
                    'width': w + (2 * bodyPadding),
                    'margin-left': 0 - ((w + (2 * bodyPadding)) / 2)
                });
                self._$modalBody.css({
                    'width': w,
                    'height': h,
                    'max-height': h
                });
            });

            self._headerComputed = ko.computed(function () {
                self._header(ko.utils.unwrapObservable(options.header));
            });
            self._acceptTextComputed = ko.computed(function () {
                self._acceptText(ko.utils.unwrapObservable(options.acceptText));
            });
            self._acceptDisabledComputed = ko.computed(function () {
                self._acceptDisabled(ko.utils.unwrapObservable(options.acceptDisabled));
            });
            self._cancelTextComputed = ko.computed(function () {
                self._cancelText(ko.utils.unwrapObservable(options.cancelText));
            });

            self._accepting = options.accepting;
            self._accepted = options.accepted;
            self._canceled = options.canceled;
            self._view(options.viewModel || options);

            self._modal.show();
        },
        hide: function () {
            var self = this,
                deferred = Q.defer(),
                promise = deferred.promise;

            if (!self._modal || !self._modal.isShown) {
                deferred.resolve(true);
                return promise;
            }

            self._sizeComputed.dispose();
            self._headerComputed.dispose();
            self._acceptTextComputed.dispose();
            self._acceptDisabledComputed.dispose();
            self._cancelTextComputed.dispose();

            self._hidingDeferred = deferred;
            self._modal.hide();

            return promise;
        },
        _init: function () {
            var self = this,
                dialogVM;

            self._header = ko.observable();
            self._acceptText = ko.observable();
            self._acceptDisabled = ko.observable();
            self._cancelText = ko.observable();
            self._view = new View();

            dialogVM = {
                header: self._header,
                acceptText: self._acceptText,
                acceptDisabled: self._acceptDisabled,
                cancelText: self._cancelText,
                view: self._view,
                accept: self._accept
            };

            self._$modal = $(self._dialogId);
            self._$modalBody = self._$modal.find('.modal-body');
            ko.applyBindings(dialogVM, self._$modal[0]);

            self._modal = new Bootstrap.modal.Constructor(self._$modal[0], {
                backdrop: 'static',
                keyboard: false
            });
            self._$modal.on('hidden', self._hidden);
        },
        _accept: function () {
            var self = this,
                event = $.Event('accepting.DialogService');

            if (self._accepting && typeof self._accepting === 'function') {
                self._accepting(event);
            }

            if (!event.isDefaultPrevented()) {
                self._acceptClicked = true;
                self.hide();
            }
        },
        _hidden: function () {
            var self = this,
                event;

            if (self._acceptClicked) {
                if (self._accepted && typeof self._accepted === 'function') {
                    event = $.Event('accepted.DialogService');
                    self._accepted(event);
                }
            } else {
                if (self._canceled && typeof self._canceled === 'function') {
                    event = $.Event('canceled.DialogService');
                    self._canceled(event);
                }
            }

            if (self._hidingDeferred) {
                self._hidingDeferred.resolve(true);
                self._hidingDeferred = undefined;
            }
            self._acceptClicked = false;
            self._header(undefined);
            self._acceptText(undefined);
            self._cancelText(undefined);
            self._accepting = undefined;
            self._accepted = undefined;
            self._canceled = undefined;
            self._view(undefined);
        }
    });
    return DialogService;
});
