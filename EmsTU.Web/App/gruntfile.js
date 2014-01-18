/*global module, require*/
module.exports = function (grunt) {
    'use strict';

    var _ = require('lodash');

    grunt.registerMultiTask('template', 'Interpolate templates using lodash.', function () {
        var options = this.options({
            templateData: {},
            templateSettings: {}
        });

        grunt.verbose.writeflags(options, 'Options');

        this.files.forEach(function (f) {
            var output = f.src.filter(function (filepath) {
                // Warn on and remove invalid source files (if nonull was set).
                if (!grunt.file.exists(filepath)) {
                    grunt.log.warn('Source file "' + filepath + '" not found.');
                    return false;
                } else {
                    return true;
                }
            })
            .map(function (filepath) {
                var src = grunt.file.read(filepath);
                var interpolated;

                try {
                    interpolated = _.template(src, options.templateData, options.templateSettings);
                } catch (e) {
                    grunt.log.error(e);
                    grunt.fail.warn(filepath + '" failed to compile.');
                }

                return interpolated;
            })
            .join('\n');

            if (output !== '') {
                grunt.file.write(f.dest, output);
                grunt.log.writeln('File "' + f.dest + '" created.');
            } else {
                grunt.log.warn('Destination not written because interpolated files were empty.');
            }
        });
    });

    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        jshint: {
            all: [
                'gruntfile.js',
                'js/main.js',
                'js/framework/**/*.js',
                'js/knockout_binding_handlers/**/*.js',
                'js/src/**/*.js'
            ],
            options: {
                es3: true,
                bitwise: true,
                curly: true,
                eqeqeq: true,
                forin: true,
                immed: true,
                indent: 4,
                latedef: true,
                newcap: true,
                noarg: true,
                noempty: true,
                nonew: true,
                regexp: true,
                undef: true,
                unused: true,
                strict: true,
                trailing: true,
                maxcomplexity: 15,
                maxlen: 150,
                globals: {
                    define: true
                }
            }
        },
        template: {
            ems_debug: {
                options: {
                    templateSettings: {
                        interpolate: /\/\*<%=([\s\S]+?)%>\*\//g,
                        evaluate: /\/\*<%([\s\S]+?)%>\*\//g
                    }
                },
                files: {
                    'build/debug/js/main.js': 'js/main.js'
                }
            }
        },
        requirejs: {
            ems_release: {
                options: {
                    baseUrl: 'js',
                    mainConfigFile: 'js/main.js',
                    out: 'build/release/js/app.js',
                    include: 'requireLib',
                    paths: {
                        requireLib: 'lib/require',
                        main: '../build/debug/js/main'
                    },
                    optimize: "uglify2",
                    preserveLicenseComments: false
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-requirejs');
    grunt.loadNpmTasks('grunt-contrib-jshint');

    grunt.registerTask('debug', ['jshint', 'template:ems_debug']);
    grunt.registerTask('release', ['jshint', 'template:ems_debug', 'requirejs:ems_release']);

    grunt.registerTask('default', ['debug']);
};
