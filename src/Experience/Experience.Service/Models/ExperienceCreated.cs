using System;
using Experience.Service.Services.EventBus;
using MongoDB.Bson.Serialization.Attributes;

    [EventStreamName("experience.events")]
    public class ExperienceCreated
    {
        public ExperienceCreated(Guid experienceid, string companyName, string role, string blurb, DateTime startDate, DateTime? endDate)
        {
            ExperienceId = experienceid;
            CompanyName = companyName;
            Role = role;
            Blurb = blurb;
            StartDate = startDate;
            EndDate = endDate;
        }

        [BsonElement]
        public Guid ExperienceId { get; private set; }

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