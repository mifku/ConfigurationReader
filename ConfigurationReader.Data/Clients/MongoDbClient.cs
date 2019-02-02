using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace ConfigurationReader.Data.Clients
{

    public  class MongoDbClient
    {
        private static MongoClient _client;
        private static object lockObject = new object();
        private MongoDbClient()
        {

        }

        public static MongoClient GetClient
        {
            get
            {
                if (_client == null)
                {
                    lock (lockObject)
                    {
                        if (_client == null)
                        {
                            _client = new MongoClient();
                        }
                    }
                }
                return _client;
            }
        }

        public static void ConfigureClient(string connectionString,int? connectTimeout)
        {
            var mongoUrl = new MongoUrl(connectionString);
            MongoDbClient.GetClient.Settings.Server = mongoUrl.Server;
            MongoDbClient.GetClient.Settings.ConnectTimeout = connectTimeout == null ? mongoUrl.ConnectTimeout : TimeSpan.FromMilliseconds((int)connectTimeout);
        }

    }
}
