define([
    //libs
    'jquery',
    'jquery.iframe-transport',
    'q',

    //framework
    'framework/corium_instance',
    'framework/class'
], function ($, jquery_iframe_transport, Q, Corium, Class) {
    'use strict';

    var Repository = Class.extend({
        constructor: function () {
        },
        get: function (url) {
            var self = this;
            return self.ajax({
                dataType: "json",
                url: url
            });
        },
        post: function (url, data) {
            var self = this;
            return self.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(data)
            });
        },
        put: function (url, data) {
            var self = this;
            return self.ajax({
                url: url,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(data)
            });
        },
        del: function (url) {
            var self = this;
            return self.ajax({
                url: url,
                type: 'DELETE'
            });
        },
        postFile: function (url, $fileInput) {
            var self = this;
            return self.ajax({
                url: url,
                type: 'POST',
                fileInput: $fileInput,
                dataType: 'iframe json'
            });
        },
        ajax: function (options) {
            return Q.resolve($.ajax(options)).then(undefined, function (jqXHR) {
                Corium.events.trigger('error.Corium', jqXHR);
                throw jqXHR;
            });
        }
    });
    return Repository;
});
