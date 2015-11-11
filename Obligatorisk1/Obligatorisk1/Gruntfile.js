module.exports = function (grunt) {
    'use strict';
    grunt.initConfig({
        // read in the project settings from the package.json file into the pkg property
        pkg: grunt.file.readJSON('package.json'),
        less: {
            development: {
                options: {
                    paths: ["assets/css"]
                },
                files: {
                    "content/site.css": "content/Site.less"

                }
            }
        }

    });

    //Add all plugins that your project needs here
    grunt.loadNpmTasks('grunt-contrib-less');
};