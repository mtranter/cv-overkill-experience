using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Expereince.Services;
using Experience.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Experience
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
    public Task<IEnumerable<Experience.Models.Experience>> List()
    {
      return _experienceRepo.ListAllAsync();
    }

    [Authorize]
    [HttpPut("{id:Guid}")]
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
  }
}
