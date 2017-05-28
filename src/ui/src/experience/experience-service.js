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
  addTech(experienceId, tech) {
    return this.http.fetch(`http://api.marktranter.com/experience/${experienceId}/techs`, {
      method: 'PUT',
      body: JSON.stringify(tech)
    });
  }
  removeTech(experienceId, tech) {
    return this.http.fetch(`http://api.marktranter.com/experience/${experienceId}/techs/${tech}`, {
      method: 'DELETE'
    });
  }
  updateExperience(experience) {
    return this.http.fetch(`http://api.marktranter.com/experience/${experience.id}`, {
      method: 'PUT',
      body: JSON.stringify(experience)
    });
  }
}
