define([
    //libs
    'knockout',

    //*globals
    'document'
], function (ko, document) {
    /*jshint maxlen: 9999, eqeqeq: false, regexp: false */
    //added jshint exceptions for the knockoutjs code
    'use strict';

    var KoUtils,
        commentNodesHaveTextProperty = document.createComment('test').text === '<!--test-->',
        //copied from knockoutjs
        startCommentRegex = commentNodesHaveTextProperty ? /^<!--\s*ko(?:\s+(.+\s*\:[\s\S]*))?\s*-->$/ : /^\s*ko(?:\s+(.+\s*\:[\s\S]*))?\s*$/,
        //copied from knockoutjs
        endCommentRegex =   commentNodesHaveTextProperty ? /^<!--\s*\/ko\s*-->$/ : /^\s*\/ko\s*$/;

    KoUtils = {
        isStartComment: function (node) {
            //copied from knockoutjs
            return (node.nodeType == 8) && (commentNodesHaveTextProperty ? node.text : node.nodeValue).match(startCommentRegex);
        },
        isEndComment: function (node) {
            //copied from knockoutjs
            return (node.nodeType == 8) && (commentNodesHaveTextProperty ? node.text : node.nodeValue).match(endCommentRegex);
        }
    };
    return KoUtils;
});
