namespace Experience.Models
{
    public class ExperienceSkillAdded
    {
        public ExperienceSkillAdded(string companyName, string skill)
        {
            CompanyName = companyName;
            Skill = skill;
        }

        public string CompanyName { get; private set; }
        public string Skill { get; private set; }
    }
}