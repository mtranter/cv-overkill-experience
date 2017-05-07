const gulp = require('gulp');
const jasmine = require('gulp-jasmine');
const paths = require('../paths')
gulp.task('test', () =>
  gulp.src(paths.unitTests)
      // gulp-jasmine works on filepaths so you can't have any plugins before it
      .pipe(jasmine())
);
