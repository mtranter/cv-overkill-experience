import {HttpClient} from 'aurelia-fetch-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export class ExperienceService {
  constructor(http){
    this.http = http;
  }
  getExperience(){
    return this.http.fetch('experience/').then(d => d.json());
  }
}
