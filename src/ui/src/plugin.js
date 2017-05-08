export function configure(config){
  let container = config.container;
  container.registerInstance('plugin.route',  { route: ['experience'],       name: 'experience',       moduleId: 'experience/index' })
  container.registerInstance('plugin.widget.homepage.component', {viewModel: 'experience/experience', view:'experience/experience.html'});
  //config.globalResources('./experience')
}
