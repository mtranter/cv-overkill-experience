import {inject} from 'aurelia-framework';
import ExperienceService from './../experience-service'

@inject(ExperienceService)
export class Edit {
  constructor(experience){
    this.experienceSvc = experience;
  }
  startAddExperience() {
    this.newExperience = {};
  }
  addExperience(experience) {
    this.experienceSvc.addExperience(experience).then(refresh);
  }
  refresh() {
    return this.experienceSvc.getExperiences().then(e => this.experiences = e);
  }
  attached() {
    return this.refresh();
  }
}
