using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;

namespace Library.Entities
{
    public class WeatherForecast
    {
        public enum Condition
        {
            [Description("Lluvia")]
            Rain,
            [Description("Sequía")]
            Dry,
            [Description("Óptimo")]
            Ideal,
            [Description("Normal")]
            Normal,
            [Description("Pico máximo de lluvia ")]
            MaximumRain
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Day { get; set; }
        
        public DateTime Date { get; set; }

        public Condition Prediction { get; set; }

        public bool IsMaxRainyPeriod { get; set; }

    }
}
