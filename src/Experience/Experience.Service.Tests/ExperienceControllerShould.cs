using System;
using System.Linq;
using Xunit;
using Experience.Service.Controllers;
using NSubstitute;
using Experience.Service.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Experience.Service.Models;
using DomainExperience = Experience.Service.Models.Experience;
using System.Linq.Expressions;

namespace Experience.Service.Tests
{
    public class ExperienceControllerShould
    {
        [Fact]
        public async Task ReturnAListOfAvailableExperiences()
        {
            var mockExp = Experience.Service.Models.Experience.Create("My Co", "Boss", "Some blurb", DateTime.Now, null);
            var repo = Substitute.For<IExperienceRepository>();
            repo.ListAllAsync().Returns(c => new[]{mockExp});

            var sut = new ExperienceController(repo);
            var actual = await sut.List();

            Assert.Equal(1, actual.Count());
            Assert.Equal(mockExp.Id, actual.First().Id);
        }

        [Fact]
        public async Task ReturnASingleExperiences()
        {
            var mockExp = Experience.Service.Models.Experience.Create("My Co", "Boss", "Some blurb", DateTime.Now, null);
            var repo = Substitute.For<IExperienceRepository>();
            repo.GetExperience(Arg.Is(mockExp.Id)).Returns(mockExp);

            var sut = new ExperienceController(repo);
            var actual = (JsonResult)(await sut.Get(mockExp.Id));

            Assert.Equal(mockExp.Id, ((Experience.Service.Models.Experience)actual.Value).Id);
        }

        
        [Fact]
        public async Task ReturnNotFoundForUnknownId()
        {
            var repo = Substitute.For<IExperienceRepository>();

            var sut = new ExperienceController(repo);
            var actual = await sut.Get(Guid.NewGuid());
            
            Assert.IsType(typeof(NotFoundResult), actual);
        }

        [Fact]
        public async Task SaveNewExperience()
        {
            var repo = Substitute.For<IExperienceRepository>();
            repo.InsertExperience(Arg.Any<Experience.Service.Models.Experience>()).Returns(c => Task.CompletedTask);

            var expected = new ExperienceDetails {
                    CompanyName = "Company", 
                    Role = "Role",
                    Blurb = "Blurb",
                    StartDate = new DateTime(2016,1,1),
                    EndDate = new DateTime(2016,7,1)
                };
            var sut = new ExperienceController(repo);
            var actual = await sut.CreateExperience(expected);
            
            Assert.IsType(typeof(CreatedAtActionResult), actual);
            repo.Received(1).InsertExperience(Arg.Is<DomainExperience>(CompareToDetails(expected)));
        }

        [Fact]
        public async Task UpdateExistingExperience()
        {
            var repo = Substitute.For<IExperienceRepository>();
            var mockExp = Experience.Service.Models.Experience.Create("My Co", "Boss", "Some blurb", DateTime.Now, null);
            repo.UpdateExperience(Arg.Any<Experience.Service.Models.Experience>()).Returns(c => Task.CompletedTask);
            repo.GetExperience(Arg.Is(mockExp.Id)).Returns(mockExp);

            var expected = new ExperienceDetails {
                    CompanyName = "Company", 
                    Role = "Role",
                    Blurb = "Blurb",
                    StartDate = new DateTime(2016,1,1),
                    EndDate = new DateTime(2016,7,1)
                };
            var sut = new ExperienceController(repo);
            var actual = await sut.UpdateExperience(mockExp.Id, expected);
            
            Assert.IsType(typeof(OkResult), actual);
            repo.Received(1).UpdateExperience(Arg.Is<DomainExperience>(CompareToDetails(expected)));
        }

         [Fact]
        public async Task Return404ForNotFoundExperience()
        {
            var repo = Substitute.For<IExperienceRepository>();

            var sut = new ExperienceController(repo);
            var actual = await sut.UpdateExperience(Guid.NewGuid(), null);
            
            Assert.IsType(typeof(NotFoundResult), actual);
        }

        private Expression<Predicate<DomainExperience>> CompareToDetails(ExperienceDetails expected)
        {
            return e => 
                e.CompanyName == expected.CompanyName && e.Role == expected.Role && e
                .Blurb == expected.Blurb && e.StartDate == expected.StartDate && e.EndDate == expected.EndDate;
        }
    }
}
