define([
    //libs
    'hasher',

    //framework
    'framework/class'
], function (hasher, Class) {
    'use strict';

    var Navigator = Class.extend({
        constructor: function () {
        },
        init: function (initCallback, initCbThisArg, changedCallback, changedCbThisArg) {
            hasher.initialized.add(initCallback, initCbThisArg);
            hasher.changed.add(changedCallback, changedCbThisArg);
        },
        dispose: function () {
            hasher.changed.removeAll();
            hasher.initialized.removeAll();
            hasher.stop();
        },
        start: function () {
            hasher.init();
        },
        setUrl: function (url, replace, silent) {
            if (silent) {
                hasher.changed.active = false;
            }
            if (replace) {
                hasher.replaceHash(url);
            } else {
                hasher.setHash(url);
            }
            if (silent) {
                hasher.changed.active = true;
            }
        },
        toHref: function (url) {
            return '#' + hasher.prependHash + url + hasher.appendHash;
        },
        getHash: function () {
            return hasher.getHash();
        }
    });
    return Navigator;
});
