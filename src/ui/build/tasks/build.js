let gulp = require('gulp');
let runSequence = require('run-sequence');
let to5 = require('gulp-babel');
let sourcemaps = require('gulp-sourcemaps');
let paths = require('../paths');
let compilerOptions = require('../babel-options');
let assign = Object.assign || require('object.assign');
let through = require('through2');
let path = require('path');
let url = require('url');
let concat = require('gulp-concat');
let moduleName = require('./../module-name.json').name;
let vinylPaths = require('vinyl-paths');
var del = require('del');
var Vinyl = require('vinyl');

var plumber = require('gulp-plumber');
var gutil = require('gulp-util');

var gulp_src = gulp.src;
gulp.src = function() {
  return gulp_src.apply(gulp, arguments)
    .pipe(plumber(function(error) {
      // Output an error message
      gutil.log(gutil.colors.red('Error (' + error.plugin + '): ' + error.message));
      // emit the end event, to properly end the task
      this.emit('end');
    })
  );
};


gulp.task('build-html', function() {
  return gulp.src(paths.html)
    .pipe(through.obj( function( file, enc, cb ) {
      let html = JSON.stringify(file.contents.toString());
      let fp = path.relative(file.cwd + "/src", file.path);
      file.contents = new Buffer(`define('text!${moduleName}/${fp}',['module'], function(module) { module.exports = ${html}; })`)
      file.path = file.path.replace(/\.\w+$/gi, '.html  .' + "js" )
      this.push( file );
      return cb();
    }))
    .pipe(gulp.dest(paths.temp));
});

gulp.task('build-css', function() {
  return gulp.src(paths.css)
    .pipe(gulp.dest(paths.temp))
});


gulp.task('build-amd', function() {
  return gulp.src(paths.source)
    .pipe(to5(assign({}, compilerOptions.amd())))
    .pipe(gulp.dest(paths.temp));
});

gulp.task('bundle', function() {
  return gulp.src(`${paths.temp}**/*.js`)
    .pipe(concat('bundle.js'))
    .pipe(gulp.dest(paths.output));
});

gulp.task('require-cfg', function(){
  let bundleName = `${moduleName}-bundle`;
  let bundlePath = {};
  bundlePath[bundleName] = `../${moduleName}/bundle`;
  let bundles = {};
  bundles[bundleName] = [];
  let cfg = {paths:bundlePath, bundles: bundles};

  return gulp.src(paths.source)
    .pipe(through.obj( function( file, enc, cb ) {

      let fp = path.relative(file.cwd + "/src", file.path).replace(/\.\w+$/gi, '');
      let moduleId = `${moduleName}/${fp}`
      cfg.bundles[bundleName].push(moduleId);
      return cb();
    }, function(cb){

        let jsFile = new Vinyl({
          path: 'bundle-cfg.js',
          contents: new Buffer('requirejs.config(' + JSON.stringify(cfg) + ');')
        });
        this.push(jsFile);
        cb();
    })).pipe(gulp.dest(paths.output));
})

gulp.task('tidy', function() {
  return gulp.src([paths.temp])
    .pipe(vinylPaths(del));
});


gulp.task('build', function(callback) {
  return runSequence(
    'clean',
    'build-amd',
    'build-html',
    'require-cfg',
    'bundle',
    'tidy',
    callback
  );
});
