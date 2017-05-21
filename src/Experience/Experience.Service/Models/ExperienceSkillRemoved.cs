using System;
using Experience.Service.Services.EventBus;

namespace Experience.Service.Models
{
    [EventStreamName("experience.skill.changed")]
    public class ExperienceSkillRemoved
    {
        public ExperienceSkillRemoved(Guid experienceId,string companyName, string skill)
        {
            ExperienceId = experienceId;
            CompanyName = companyName;
            Skill = skill;
        }

        public Guid ExperienceId { get; private set; }
        public string CompanyName { get; private set; }
        public string Skill { get; private set; }
    }
}