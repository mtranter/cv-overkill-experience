using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Experience.Models
{
    public class ExpereinceDetailsUpdated
    {
        public ExpereinceDetailsUpdated(string companyName, string role, string blurb, DateTime startDate, DateTime? endDate)
        {
            CompanyName = companyName;
            Role = role;
            Blurb = blurb;
            StartDate = startDate;
            EndDate = endDate;
        }

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
    }
}