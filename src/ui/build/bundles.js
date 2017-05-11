module.exports = {
  "bundles": {
    "dist/app-bundle": {
      "includes": [
        "[**/**/*.js]",
        "[**/*.html!text]",
        "[**/*.css!text]"
      ],
      "options": {
        "inject": true,
        "minify": true,
        "depCache": false,
        "rev": false
      }
    }
  }
};
