define('text!experience/experience.html',['module'], function(module) { module.exports = "<template>\n  This is my experience\n  <ul>\n    <li repeat.for=\"item of experience\">${item.company}</li>\n  </ul>\n</template>\n"; })
define('experience/experience', ['exports'], function (exports) {
  'use strict';

  Object.defineProperty(exports, "__esModule", {
    value: true
  });

  function _classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  }

  var Experience = exports.Experience = function Experience() {
    _classCallCheck(this, Experience);

    this.experience = [{
      company: 'Northdoor'
    }, {
      company: 'Priocept'
    }];
  };
});
define('text!experience/index.html',['module'], function(module) { module.exports = "<template>\n  Hello From Experience\n</template>\n"; })
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
    config.globalResources('./experience');
  }
});