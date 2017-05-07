export function configure(config){
  let container = config.container;
  container.registerInstance('plugin.route',  { route: ['experience'],       name: 'experience',       moduleId: 'experience/index' })
}
