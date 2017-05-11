System.config({
  defaultJSExtensions: true,
  transpiler: false,
  paths: {
    "*": "dist/*",
    "github:*": "jspm_packages/github/*"
  },
  map: {
    "text": "github:systemjs/plugin-text@0.0.8"
  },
  bundles: {
    "app-build.js": [
      "experience/experience.html!github:systemjs/plugin-text@0.0.8.js",
      "experience/experience.js",
      "experience/plugin.js",
      "experience/service/experience-service.js"
    ]
  }
});
