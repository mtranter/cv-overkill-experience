import {HttpClient} from 'aurelia-fetch-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export class ExperienceService {
  constructor(http){
    this.http = http;
  }
  getExperiences(){
    return this.http.fetch('http://api.marktranter.com/experience/').then(d => d.json());
  }
  addExperience(experience) {
    return this.http.fetch('http://api.marktranter.com/experience/', {
      method: 'POST',
      body: JSON.stringify(experience)
    });
  }
}
