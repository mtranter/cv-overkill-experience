define('text!experience/index.html',['module'], function(module) { module.exports = "<template>\n  Hello From Remote plugins\n</template>\n"; })
define("experience/index", ["exports"], function (exports) {
  "use strict";

  Object.defineProperty(exports, "__esModule", {
    value: true
  });

  function _classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  }

  var Index = exports.Index = function Index() {
    _classCallCheck(this, Index);
  };
});
define('experience/plugin', ['exports'], function (exports) {
  'use strict';

  Object.defineProperty(exports, "__esModule", {
    value: true
  });
  exports.configure = configure;
  function configure(config) {
    var container = config.container;
    container.registerInstance('plugin.route', { route: ['experience'], name: 'experience', moduleId: 'experience/index' });
  }
});