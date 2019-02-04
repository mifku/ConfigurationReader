using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace ConfigurationReader.Context
{
    public class ConfigurationContext
    {
        private readonly IMongoDatabase _database = null;

        public ConfigurationContext(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            //client.Settings.ConnectTimeout =  TimeSpan.FromMilliseconds(settings.TimeoutDurationInMs);
            if (client != null)
                _database = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<ConfigurationEntitiy> Configurations
        {
            get
            {
                return _database.GetCollection<ConfigurationEntitiy>("Configurations");
            }
        }
    }
}
