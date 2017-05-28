import {inject} from 'aurelia-framework';
import {ExperienceService} from './../experience-service'

@inject(ExperienceService)
export class Edit {
  constructor(experience){
    this.experienceSvc = experience;
  }
  editExperience(experience) {
    this.editingExperience = experience;
  }
  saveExperience(experience) {
    if(experience.id){
      this.experienceSvc.updateExperience(experience).then(() =>{ this.clearEditing(); this.refresh(); });
    } else {
      this.experienceSvc.addExperience(experience).then(() => { this.clearEditing(); this.refresh(); });
    }
  }
  refresh() {
    return this.experienceSvc.getExperiences().then(e => {
      this.experiences = e;
    });
  }
  addTech(experience, tech){
    return this.experienceSvc.addTech(experience.id, tech).then(() => experience.techs.push(tech));
  }
  removeTech(experience, tech){
    return this.experienceSvc.removeTech(experience.id, tech).then(() => experience.techs = experience.techs.filter(t => t != tech));
  }
  clearEditing(){
    this.editingExperience = undefined;
  }
  attached() {
    return this.refresh();
  }
}
