using System;
using System.ComponentModel;

namespace Library.Entities
{
    public class WeatherForecast
    {
        public enum Weather
        {
            [Description("Lluvia")]
            Rain,
            [Description("Sequ�a")]
            Dry,
            [Description("�ptimo")]
            Ideal
        }

        public int Day { get; set; }
        
        public DateTime Date { get; set; }

        public Weather Prediction { get; set; }

        

    }
}
