using System;
using System.Collections.Generic;
using System.Linq;
using Expereince.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Experience.Models
{
  public class Experience
  {
    [BsonElement]
    private readonly List<string> _skills;

    [BsonId]
    [BsonElement]
    public Guid Id { get; private set; }

    [BsonElement]
    public string CompanyName { get; private set; }

    [BsonElement]
    public string Role { get; private set; }

    [BsonElement]
    public string Blurb { get; private set; }

    [BsonElement]
    public DateTime StartDate { get; private set; }

    [BsonElement]
    public DateTime? EndDate { get; private set; }


    public IEnumerable<string> Skills => _skills.AsEnumerable();

    public static Experience Create(string companyName, string role, string blurb, DateTime startDate, DateTime? endDate)
    {
      return new Experience(companyName, role, blurb, startDate, endDate, new List<string>());
    }

    [BsonConstructor]
    private Experience()
    {

    }

    private Experience(string companyName, string role, string blurb, DateTime startDate, DateTime? endDate, List<string> skills)
    {
      Id = Guid.NewGuid();
      CompanyName = companyName;
      Role = role;
      Blurb = blurb;
      StartDate = startDate;
      EndDate = endDate;
      _skills = skills;
    }

    public void UpdateDetails(ExperienceDetails details)
    {
      this.CompanyName = details.CompanyName;
      this.Role = details.Role;
      this.Blurb = details.Blurb;
      this.StartDate = details.StartDate;
      this.EndDate = details.EndDate;

      DomainEvents.Publish(new ExpereinceDetailsUpdated(
        this.CompanyName,
        this.Role,
        this.Blurb,
        this.StartDate,
        this.EndDate));
    }

    public void AddSkill(string skill)
    {
      if (!Skills.Any(s => s.Equals(skill, StringComparison.OrdinalIgnoreCase)))
      {
        _skills.Add(skill);
        DomainEvents.Publish(new ExperienceSkillAdded(CompanyName, skill));
      }
    }

    public void RemoveSkill(string skill)
    {
      if (Skills.Any(s => s.Equals(skill, StringComparison.OrdinalIgnoreCase)))
      {
        _skills.Remove(skill);
        DomainEvents.Publish(new ExperienceSkillRemoved(CompanyName, skill));
      }
    }
  }
}