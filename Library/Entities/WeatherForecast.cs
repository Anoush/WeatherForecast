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
            [Description("Sequ�a")]
            Dry,
            [Description("�ptimo")]
            Ideal,
            [Description("Normal")]
            Normal,
            [Description("Pico m�ximo de lluvia ")]
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
