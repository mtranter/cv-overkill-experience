import {inject} from 'aurelia-framework';
import {ExperienceService} from './experience-service'

@inject(ExperienceService)
export class Experience {
  constructor(experienceService){
    this.experienceService = experienceService;
  }
  attached() {
    return this.experienceService.getExperiences().then(d => this.experiences = d);
  }
}
