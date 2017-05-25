import {inject} from 'aurelia-framework';
import {ExperienceService} from './../experience-service'

@inject(ExperienceService)
export class Edit {
  constructor(experience){
    this.experienceSvc = experience;
  }
  startAddExperience(experience) {
    this.editingExperience = experience;
  }
  saveExperience(experience) {
    if(experience.id){
      this.experienceSvc.updateExperience(experience).then(this.refresh);
    } else {
      this.experienceSvc.addExperience(experience).then(() => { this.clearEditing(); this.refresh(); };
    }
  }
  refresh() {
    return this.experienceSvc.getExperiences().then(e => {
      this.experiences = e
    });
  }
  clearEditing(){

  }
  attached() {
    return this.refresh();
  }
}
