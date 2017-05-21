using System;
using System.Collections.Generic;
using System.Linq;
using Experience.Service.Models;
using Experience.Service.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Experience.Service.Models
{
  public class Experience
  {
    [BsonElement] private List<string> _techs;

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


    public IEnumerable<string> Techs => _techs.AsEnumerable();

    public static Experience Create(string companyName, string role, string blurb, DateTime startDate, DateTime? endDate)
    {
      return new Experience(companyName, role, blurb, startDate, endDate, new List<string>());
    }

    [BsonConstructor]
    private Experience()
    {
    }

    private Experience(string companyName, string role, string blurb, DateTime startDate, DateTime? endDate, List<string> techs)
    {
      Id = Guid.NewGuid();
      CompanyName = companyName;
      Role = role;
      Blurb = blurb;
      StartDate = startDate;
      EndDate = endDate;
      _techs = techs;
    }

    public void UpdateDetails(ExperienceDetails details)
    {
      this.CompanyName = details.CompanyName;
      this.Role = details.Role;
      this.Blurb = details.Blurb;
      this.StartDate = details.StartDate;
      this.EndDate = details.EndDate;

      DomainEvents.Publish(new ExpereinceDetailsUpdated(
        this.Id,
        this.CompanyName,
        this.Role,
        this.Blurb,
        this.StartDate,
        this.EndDate));
    }

    public void AddSkill(string skill)
    {
      _techs = _techs ?? new List<string>();
      if (!Techs.Any(s => s.Equals(skill, StringComparison.OrdinalIgnoreCase)))
      {
        _techs.Add(skill);
        DomainEvents.Publish(new ExperienceSkillAdded(Id,CompanyName, skill));
      }
    }

    public void RemoveSkill(string skill)
    {
      if (_techs == null)
      {
        return;
      }
      if (Techs.Any(s => s.Equals(skill, StringComparison.OrdinalIgnoreCase)))
      {
        _techs.Remove(skill);
        DomainEvents.Publish(new ExperienceSkillRemoved(Id, CompanyName, skill));
      }
    }
  }
}