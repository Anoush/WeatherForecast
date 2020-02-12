using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WebApi.Models
{
    public class ApiResultMessages
    {
        public enum Messages
        {
            [Description("There are no data available, please trying executing /WeatherForecast/GenerateWeatherForecast")]
            NoDataFound,

            [Description("There was an error trying to clean up the data base")]
            CleanUpError,

            [Description("The data was successfully cleaned.")]
            CleanUpSuccess,

            [Description("There was an error executing the service. Please try again")]
            ServiceError
        }

        public enum Statuses
        {
            [Description("Error")]
            Error,

            [Description("Success")]
            Success,

            [Description("Warning")]
            Warning

        }

        public string Status { get; set; }
        public string Message { get; set; }

    }
}
