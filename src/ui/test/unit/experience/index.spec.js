import {StageComponent} from 'aurelia-testing';
import {bootstrap} from 'aurelia-bootstrapper';
import {Container} from 'aurelia-dependency-injection';
import {HttpClient} from 'aurelia-fetch-client';

class HttpStub {
  companyName = '';
  fetch(){
    let that = this;
    return Promise.resolve({
      json: function(){
        return Promise.resolve([{
          companyName: that.companyName
        }]);
      }
    });
  }
}

class ShortDateValueConverter {
  toValue(a){ return a;}
}

class SortValueConverter {
  toValue(a){ return a;}
}

describe('IndexComponent', () => {
  let component;
  let viewModel;
  let svc;

  beforeEach(() => {
    svc = new HttpStub();

    component = StageComponent
      .withResources('experience/experience')
      .inView('<experience></experience>');

      component.bootstrap(aurelia => {
        aurelia.use.standardConfiguration();

        aurelia.container.registerInstance(HttpClient, svc);
        aurelia.resources.registerValueConverter('shortDate',ShortDateValueConverter);
        aurelia.resources.registerValueConverter('sort',SortValueConverter);
      });
  });

  it('should render experience', done => {
    let testName = svc.companyName = 'Trizzle Ltd';
    component.manuallyHandleLifecycle().create(bootstrap)
    .then(() => component.bind())
    .then(() => {
      const nameElement = document.querySelector('article h3');
      expect(nameElement).toBe(null);
    })
    .then(() => component.attached())
    .then(() => {
      const nameElement = document.querySelector('article h3');
      expect(nameElement.innerHTML).toBe(testName);
    })
    .then(done)
    .catch(e => { console.log(e.message); })
  });

  afterEach(() => {
    component.dispose();
  });
});
