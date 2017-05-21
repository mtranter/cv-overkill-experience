using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Experience.Service.Models;
using Experience.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Experience.Service.Controllers
{
  [Route("experience")]
  public class ExperienceController : Controller
  {
    private readonly IExperienceRepository _experienceRepo;

    public ExperienceController(IExperienceRepository experienceRepo)
    {
      _experienceRepo = experienceRepo;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> Get(Guid id)
    {
      var experience = await _experienceRepo.GetExperience(id);
      if (experience == null)
      {
        return NotFound();
      }
      return Json(experience);
    }

    [HttpGet()]
    public Task<IEnumerable<Models.Experience>> List()
    {
      return _experienceRepo.ListAllAsync();
    }


    [HttpPut("{id:Guid}")]
    [Authorize(Policy = "IsMyGithub")]
    public async Task<ActionResult> UpdateExperience(Guid id, [FromBody]ExperienceDetails details)
    {
      var experience = await _experienceRepo.GetExperience(id);
      if (experience == null)
      {
        return NotFound();
      }
      experience.UpdateDetails(details);
      await _experienceRepo.UpdateExperience(experience);
      return Ok();
    }

    [Authorize(Policy = "IsMyGithub")]
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteExperience(Guid id)
    {
      var experience = await _experienceRepo.GetExperience(id);
      if (experience == null)
      {
        return NotFound();
      }
      await _experienceRepo.DeleteExperience(id);
      return Ok();
    }

    [HttpPost()]
    [Authorize(Policy = "IsMyGithub")]
    public async Task<ActionResult> CreateExperience([FromBody][Required]ExperienceDetails details)
    {
      if (details == null)
      {
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var experience = Models.Experience.Create(details.CompanyName, details.Role, details.Blurb,
        details.StartDate, details.EndDate);
      await _experienceRepo.InsertExperience(experience);
      return CreatedAtAction(nameof(Get), new {id = experience.Id}, experience);
    }

    [HttpPut("{id:Guid}/techs")]
    [Authorize(Policy = "IsMyGithub")]
    public async Task<ActionResult> AddTech(Guid id, [FromBody][Required]string details)
    {
      var experience = await _experienceRepo.GetExperience(id);
      if (experience == null)
      {
        return NotFound();
      }
      experience.AddSkill(details);
      await _experienceRepo.UpdateExperience(experience);
      return Ok();
    }

    [HttpPut("{id:Guid}/techs/{tech}")]
    [Authorize(Policy = "IsMyGithub")]
    public async Task<ActionResult> DeleteTech(Guid id, string tech)
    {
      var experience = await _experienceRepo.GetExperience(id);
      if (experience == null)
      {
        return NotFound();
      }
      experience.RemoveSkill(tech);
      await _experienceRepo.UpdateExperience(experience);
      return Ok();
    }
  }
}
