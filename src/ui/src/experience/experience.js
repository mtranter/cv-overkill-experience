import {inject} from 'aurelia-framework';
import {ExperienceService} from './service/experience-service'

@inject(ExperienceService)
export class Experience {
  constructor(experienceService){
    this.experienceService = experienceService;
  }
  attached() {
    return this.experienceService.getExperience().then(d => this.experience = d);
  }
}
