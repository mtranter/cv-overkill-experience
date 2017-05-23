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
  addExperiecne(x) {
    return this.http.fetch('http://api.marktranter.com/experience/', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(experience)
    });
  }
}
