namespace Experience.Models
{
    public class ExperienceSkillRemoved
    {
        public ExperienceSkillRemoved(string companyName, string skill)
        {
            CompanyName = companyName;
            Skill = skill;
        }

        public string CompanyName { get; private set; }
        public string Skill { get; private set; }
    }
}