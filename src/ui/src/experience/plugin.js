export function configure(config){
  let container = config.container;
  //container.registerInstance('plugin.route',  { route: ['experience'],       name: 'experience',       moduleId: 'experience/index' })
  container.registerInstance('plugin.widget.homepage.component', {
    title:'Experience',
    href:'#experience',
    name:'experience',
    viewModel: 'experience/experience',
    view:'experience/experience.html'
  });
  //config.globalResources('./experience')
}
