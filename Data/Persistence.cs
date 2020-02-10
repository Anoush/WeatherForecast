using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Data
{
    public class Persistence
    {
        const string databaseName = "weatherforecastdb";
        public void PersistData<T>(List<T> weatherForecastList)
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<T>("weatherforecast");
            weatherForecastList.ForEach(item => collection.InsertOne(item));
        }

        //public List<T> GetAll<T>()
        //{

        //}

        //public T GetById<T>()
        //{

        //}


    }
}
