using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Library;
using Library.Entities;
using MongoDB.Driver;

namespace TriPlanetarySystem
{
    class Program
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        static void Main(string[] args)
        {
            //clear all database
            if(args.Length > 0 && args[0] == "-d")
                ClearDataBase();

            Execute();
        }

        private static void Execute()
        {
            //clear the database to avoid duplicated records
            ClearDataBase();

            var system = new Processor();

            Console.WriteLine("\n ------------------------------------------- \n");
            Console.WriteLine("Predicción del clima para los próximos 10 años \n");

            var days = Convert.ToInt32((DateTime.Now.AddYears(10) - DateTime.Now).TotalDays);
            var data = GenerateData(system);
            system.CalculateConditionsForAPeriod(data).ToList().ForEach(item => Console.WriteLine($"{item.Key} : {item.Value}"));

            Console.ReadKey();

        }

        private static void ClearDataBase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultMongoConnection"].ConnectionString;
            var dbName = ConfigurationManager.AppSettings["MongoDbName"].ToString();
            var mongoCollectionName = ConfigurationManager.AppSettings["MongoCollection"].ToString();

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(dbName);

            _database.DropCollection(mongoCollectionName);
        }
        
        private static List<WeatherForecast> GenerateData(Processor system)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultMongoConnection"].ConnectionString;
            var dbName = ConfigurationManager.AppSettings["MongoDbName"].ToString();

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(dbName);
            var _collection = _database.GetCollection<WeatherForecast>("weatherforecast");

            var predictions = system.GetAllWeatherForecast();
            predictions.ForEach(item => _collection.InsertOne(item));
            
            return predictions;
        }

    }
}
