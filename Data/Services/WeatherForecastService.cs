using Data.Models;
using Library.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;

namespace Data.Services
{
    public class WeatherForecastService
    {
        private readonly IMongoCollection<WeatherForecast> _weatherForecast;
        
        public WeatherForecastService(IWeatherForecastDatabaseSettings settings)
        {
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _weatherForecast = database.GetCollection<WeatherForecast>(settings.WeatherForecastCollectionName);
        }

        public List<WeatherForecast> Get() =>
           _weatherForecast.Find(item => true).ToList();

        public WeatherForecast Get(string day) =>
        _weatherForecast.Find(item => item.Day == int.Parse(day)).FirstOrDefault();

        public WeatherForecast Create(WeatherForecast item)
        {
            _weatherForecast.InsertOne(item);
            return item;
        }

        public void Update(string id, WeatherForecast itemIn) =>
            _weatherForecast.ReplaceOne(item => item.Id == id, itemIn);

        public void Remove(WeatherForecast itemIn) =>
            _weatherForecast.DeleteOne(item => item.Id == itemIn.Id);

        public void Remove(string id) =>
            _weatherForecast.DeleteOne(item => item.Id == id);
    }
}
