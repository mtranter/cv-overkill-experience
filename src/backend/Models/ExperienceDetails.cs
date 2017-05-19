using System;
using System.ComponentModel.DataAnnotations;

namespace Experience.Models
{
    public class ExperienceDetails
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Blurb { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}