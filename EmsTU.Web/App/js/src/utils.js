define([
    //libs
    'lodash',
    'moment',

    //*globals
    'XMLSerializer',
    'DOMParser',
    'ActiveXObject'
], function (_, moment, XMLSerializer, DOMParser, ActiveXObject) {
    /*global decodeURIComponent, encodeURIComponent*/
    'use strict';

    var Utils;

    Utils = {
        Dom: {
            traverseNode: function (node, func) {
                func(node);
                node = node.firstChild;
                while (node) {
                    Utils.Dom.traverseNode(node, func);
                    node = node.nextSibling;
                }
            }
        },
        Xml: {
            toString: function (xml) {
                var s;
                if (xml.xml) {
                    //IE
                    //check for xml property first as IE9 has XMLSerializer
                    //but works incorrectly
                    s = xml.xml;
                } else if (XMLSerializer) {
                    //Modern browsers
                    s = (new XMLSerializer()).serializeToString(xml);
                } else {
                    throw new Error('Cannot parse xml.');
                }
                return s;
            },
            fromString: function (s) {
                var xml;
                if (DOMParser) {
                    //Modern browsers
                    xml = (new DOMParser()).parseFromString(s, 'text/xml');
                } else if (ActiveXObject) {
                    //IE8
                    xml = new ActiveXObject('Microsoft.XMLDOM');
                    xml.async = false;
                    xml.loadXML(s);
                } else {
                    throw new Error('Cannot parse xml.');
                }
                return xml;
            }
        },
        String: {
            format: function (format) {
                var args = arguments;
                return format.replace(/\{(\d+)\}/g, function (match, numberStr) {
                    var number = parseInt(numberStr, 10);
                    return args[number + 1] !== undefined ? args[number + 1] : match;
                });
            },
            random: function (length) {
                var text = [];
                var possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

                for (var i = 0; i < length; i++) {
                    text[i] = possible.charAt(Math.floor(Math.random() * possible.length));
                }

                return text.join('');
            },
            concat: function (arr) {
                var query = '';
                if (arr.length > 0) {
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].length) {
                            query += arr[i] + ' ';
                        }
                    }
                }

                if (query.length) {
                    query = query.substring(0, query.length - 1);
                }

                return query;
            }
        },
        Date: {
            parseInput: function (inputText) {
                var momentDate;

                inputText = inputText || '';
                inputText = inputText.trim();
                momentDate = moment(inputText, [
                    'DD.MM.YY',
                    'DD/MM/YY',
                    'DD-MM-YY',
                    'DD.MM.YYYY',
                    'DD/MM/YYYY',
                    'DD-MM-YYYY'
                ]);
                if (momentDate && momentDate.isValid()) {
                    return momentDate.toDate();
                }
                return undefined;
            },
            parseWithHourInput: function (inputText) {
                var momentDate;

                inputText = inputText || '';
                inputText = inputText.trim();
                momentDate = moment(inputText, [
                    'DD.MM.YY HH:mm',
                    'DD/MM/YY HH:mm',
                    'DD-MM-YY HH:mm',
                    'DD.MM.YYYY HH:mm',
                    'DD/MM/YYYY HH:mm',
                    'DD-MM-YYYY HH:mm'
                ]);
                if (momentDate && momentDate.isValid()) {
                    return momentDate.toDate();
                }
                return undefined;
            },
            parseAPI: function (jsonDateText) {
                return jsonDateText ? new Date(Date.parse(jsonDateText)) : undefined;
            },
            formatAPI: function (date) {
                return date ? moment(date).format('YYYY-MM-DDTHH:mm:ss') : undefined;
            },
            format: function (date) {
                return date ? moment(date).format('DD.MM.YYYY') : '';
            },
            formatWithHour: function (date) {
                //return date ? moment(date).format('DD.MM.YYYY HH:mm:ss') : '';
                return date ? moment(date).format('DD.MM.YYYY HH:mm') : '';
            }
        },
        Number: {
            parseFloat: function (s) {
                var f;

                s = s || '';
                f = parseFloat(s.trim().replace(',', '.'));

                return isNaN(f) ? undefined : f;
            },
            formatFloat: function (f, digits) {
                var s;

                if (typeof f === 'number') {
                    s = parseFloat(f.toFixed(digits)).toString().replace('.', ',');
                }

                return s;
            },
            parseInt: function (s) {
                var i;

                s = s || '';
                i = parseInt(s.trim(), 10);

                return isNaN(i) ? undefined : i;
            },
            formatInt: function (i) {
                var s;

                if (typeof i === 'number') {
                    s = parseInt(i, 10).toString();
                }

                return s;
            }
        },
        Uri: {
            parseQuery: function (query) {
                var queryString = query && query[0] === '?' ? query.substring(1) : (query || ''),
                    queryResult = {};

                queryString
                .split('&')
                .map(function (part) {
                    if (!part) {
                        return;
                    }

                    var splitPart = part.split('=');
                    return {
                        key: splitPart[0],
                        value: decodeURIComponent(splitPart[1])
                    };
                }).forEach(function (part) {
                    if (part) {
                        queryResult[part.key] = part.value;
                    }
                });

                return queryResult;
            },
            createQuery: function (queryObj) {

                var query = '';

                for (var prop in queryObj) {
                    if (queryObj.hasOwnProperty(prop) && queryObj[prop] !== undefined) {
                        query += prop + '=' + encodeURIComponent(queryObj[prop]) + '&';
                    }
                }

                if (query.length) {
                    query = query.substring(0, query.length - 1);
                }

                return query;
            }
        },
        Object: {
            conform: function (sourceObj, specObj) {
                if (_.isPlainObject(specObj)) {
                    if (!sourceObj) {
                        sourceObj = {};
                    }

                    if (_.isPlainObject(sourceObj)) {
                        _.forOwn(specObj, function (value, key) {
                            sourceObj[key] = Utils.Object.conform(sourceObj[key], value);
                        });
                    }
                } else if (_.isArray(specObj)) {
                    if (!sourceObj) {
                        sourceObj = [];
                    }

                    if (_.isArray(sourceObj)) {
                        _.forEach(sourceObj, function (value) {
                            Utils.Object.conform(value, specObj[0]);
                        });
                    }
                } else {
                    if (!sourceObj) {
                        sourceObj = specObj;
                    }
                }

                return sourceObj;
            },
            traverse: function (obj, callback) {
                if (_.isPlainObject(obj) ||
                    _.isArray(obj) ||
                    (_.isObject(obj) && !_.isString(obj) && !_.isRegExp(obj) && !_.isNumber(obj))
                ) {
                    _.forEach(obj, function (value) {
                        Utils.Object.traverse(callback(value) || value, callback);
                    });
                }
            }
        }
    };
    return Utils;
});
