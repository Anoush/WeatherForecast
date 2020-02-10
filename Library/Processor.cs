using Data;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Processor
    {
        #region Properties & Fields
        private readonly List<Planet> planets = new List<Planet>();

        public List<Planet> Planets => planets;

        public DateTime MaxRainyPeriod => _maxRainyPeriod;

        private DateTime _maxRainyPeriod { get; set; }

        #endregion

        #region Public Methods
        public Dictionary<string, int> CalculateConditionsForAPeriod(int days)
        {
            List<WeatherForecast> predictions = GetWeatherForecastByDays(days);
            return new Dictionary<string, int>
            {
                { WeatherForecast.Weather.Rain.GetEnumDescription(), predictions.ToArray().Where(x => x.Prediction == WeatherForecast.Weather.Rain).Count() },
                { WeatherForecast.Weather.Dry.GetEnumDescription(), predictions.ToArray().Where(x => x.Prediction == WeatherForecast.Weather.Dry).Count() },
                { WeatherForecast.Weather.Rain.GetEnumDescription(), predictions.ToArray().Where(x => x.Prediction == WeatherForecast.Weather.Ideal).Count() }
            };
        }

        public List<WeatherForecast> GetAllWeatherForecast()
        {
            var days = Convert.ToInt32((DateTime.Now.AddYears(10) - DateTime.Now).TotalDays);
            return GetWeatherForecastByDays(days);
        }

        #endregion

        #region Private Methods
        private List<WeatherForecast> GetWeatherForecastByDays(int days)
        {
            var predictions = new List<WeatherForecast>();

            double maxTriangleArea = 0;

            for (int i = 1; i <= days; i++)
            {
                var date = DateTime.Today.AddDays(i).Date;
                Planets.ForEach(planet => planet.UpdatePosition(date));

                if (IsSunWithinPlanetsTriangle(out var area))
                {
                    predictions.Add(BuildWeatherObject(i, date, WeatherForecast.Weather.Rain));
                    if (area > maxTriangleArea) _maxRainyPeriod = date;
                }

                if (ArePlanetsAligned(out var slope))
                {
                    slope = 0;
                    if (ArePlanetsAlignedWithTheSun(slope))
                        predictions.Add(BuildWeatherObject(i, date, WeatherForecast.Weather.Dry));
                    else
                        predictions.Add(BuildWeatherObject(i, date, WeatherForecast.Weather.Ideal));
                }
            }
           // Save(predictions);
            

            return predictions;
        }

        public WeatherForecast CalculateConditionsForADay(int day)
        {
            var date = DateTime.Today.AddDays(day).Date;
            Planets.ForEach(planet => planet.UpdatePosition(date));

            if (IsSunWithinPlanetsTriangle(out var area))
                return BuildWeatherObject(day, date, WeatherForecast.Weather.Rain);

            if (ArePlanetsAligned(out var slope))
            {
                slope = 0;
                if (ArePlanetsAlignedWithTheSun(slope)) return BuildWeatherObject(day, date, WeatherForecast.Weather.Dry);
            }

            return BuildWeatherObject(day, date, WeatherForecast.Weather.Ideal);
        }

        private static WeatherForecast BuildWeatherObject(int day, DateTime date, WeatherForecast.Weather weather)
        {
            return new WeatherForecast
            {
                Date = date,
                Day = day,
                Prediction = weather
            };
        }

        /// <summary>
        /// It calculates the distance between the planets and gets the triangule area. 
        /// Then calculates the triangule area of each planet with the sun.
        /// If the addition of the 3 internal areas (each planet with the sun) is equal to the external triangule, then it contains the sun.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private bool IsSunWithinPlanetsTriangle(out double area)
        {
            var sideAB = MathUtility.CalculateDistanceBetweenCoordinates(Planets[0].Position.x, Planets[0].Position.y, Planets[1].Position.x, Planets[1].Position.y);
            var sideBC = MathUtility.CalculateDistanceBetweenCoordinates(Planets[1].Position.x, Planets[1].Position.y, Planets[2].Position.x, Planets[2].Position.y);
            var sideCA = MathUtility.CalculateDistanceBetweenCoordinates(Planets[2].Position.x, Planets[2].Position.y, Planets[0].Position.x, Planets[0].Position.y);

            var planetTriangleTotalArea = MathUtility.CalculateTriangleArea(sideAB, sideBC, sideCA);
            var internalTrianglesArea = CalculateInternalTriangles(sideAB, sideBC, sideCA);

            area = planetTriangleTotalArea;
            return planetTriangleTotalArea == internalTrianglesArea;
        }

        public double CalculateInternalTriangles(double sideAB, double sideBC, double sideCA)
        {
            var centerALentgh = MathUtility.CalculateDistanceBetweenCoordinates(0, 0, Planets[0].Position.x, Planets[0].Position.y);
            var centerBLentgh = MathUtility.CalculateDistanceBetweenCoordinates(0, 0, Planets[1].Position.x, Planets[1].Position.y);
            var centerCLentgh = MathUtility.CalculateDistanceBetweenCoordinates(0, 0, Planets[2].Position.x, Planets[2].Position.y);

            var centerABArea = MathUtility.CalculateTriangleArea(sideAB, centerALentgh, centerBLentgh);
            var centerBCArea = MathUtility.CalculateTriangleArea(sideBC, centerBLentgh, centerCLentgh);
            var centerCAArea = MathUtility.CalculateTriangleArea(sideCA, centerALentgh, centerCLentgh);

            return centerABArea + centerBCArea + centerCAArea;
        }
        
        private bool ArePlanetsAligned(out double slope)
        {
            var planetsAreAligned = CalculatePlanetsAlignment(out var slopeToCompare);
            slope = slopeToCompare;
            return planetsAreAligned;
        }

        /// <summary>
        /// If planets are all aligned between them, then I take one to compare with the sun
        /// </summary>
        /// <param name="slope">I use the planet line slope to apply the interception formula</param>
        /// <returns></returns>
        private bool ArePlanetsAlignedWithTheSun(double slope)
        {
            return MathUtility.LineIntercepsOrigin(Planets[0].Position.x, Planets[0].Position.y, slope);
        }

        private bool CalculatePlanetsAlignment(out double slope)
        {
            //defines the first slope value I get comparing the two first planets
            slope = 0;

            for (int i = 0; i < Planets.Count - 1; i++)
            {
                var linearSlope = MathUtility.CalculateLinearEquation(Planets[i].Position.x, Planets[i].Position.y, Planets[i+1].Position.x, Planets[i+1].Position.y);

                //if is the first comparision, I should set the slope value with the next planet 
                if (slope == 0)
                    slope = linearSlope;

                //if there is a at least one difference comparing two slopes, it means planets are not aligned.
                if (slope != linearSlope)
                    return false;
            }

            return true;
        }

        //private void Save(List<WeatherForecast> weatherForecastsBusinessObject)
        //{
        //    var data = new Persistence();
        //    var dataObjectList = new List<WeatherForecastObject>();
        //    weatherForecastsBusinessObject.ForEach(item =>
        //    dataObjectList.Add(new WeatherForecastObject { Date = item.Date.ToString("dd/MM/yyyy"), Day = item.Day, Prediction = item.Prediction.GetEnumDescription()}));
        //    data.PersistData(dataObjectList);
        //}

        #endregion

    }
}
