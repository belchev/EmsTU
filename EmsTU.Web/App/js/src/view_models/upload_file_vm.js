define([
    //libs
    'jquery',
    'knockout',
    'knockout.mapping',
    'q',

    //*globals
    'document',

    //framework
    'framework/corium',

    //src
    'src/validation_utils',
    'src/repositories/file_repository'
], function ($, ko, ko_mapping, Q, document, Corium, ValidationUtils, FileRepository) {
    'use strict';

    var UploadFileVM = Corium.Class.extend({
        constructor: function () {
            var self = this;

            self.templateId = 'templates:upload_file.html';

            self._hasFileError = ko.observable(false);
            self._showErrors = ko.observable(false);

            self._fileRepository = new FileRepository();
        },
        attach: function (type) {
            var self = this,
                $fileInput = $('#file_input', document),
                fileName = $fileInput.val(),
                promise;

            self._hasFileError(!fileName);

            if (ValidationUtils.isValid(self) && fileName) {
                promise =
                    self._fileRepository
                    .upload($fileInput, type)
                    .then(function (result) {
                        return result;
                    });
            } else {
                self._showErrors(true);
                promise = Q.resolve(false);
            }
            return promise;
        }
    });
    return UploadFileVM;
});
