using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConfigurationReader.Data.Clients;
using ConfigurationReader.Data.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ConfigurationReader.Data.Proxies
{
    public class MongoDbProxy :IDbProxy
    {
        public async Task<IEnumerable<ConfigurationEntity>> GetConfigurations(string connectionString, string serviceName , int timeoutTime)
        {

            try
            {
                var mongoUrl = new MongoUrl(connectionString);
                IMongoDatabase db = MongoDbClient.GetClient.GetDatabase(mongoUrl.DatabaseName);
                var builder = Builders<ConfigurationEntity>.Filter;
                var filter = builder.Eq(Constants.ApplicationNameColumn,serviceName) & builder.Eq(Constants.IsActiveColumn , ConfigurationActiveness.Active);
                IMongoCollection<ConfigurationEntity> configCollection = db.GetCollection<ConfigurationEntity>(Constants.CollectionName);
                Console.WriteLine(DateTime.Now.ToString() + " return collection");
                return await configCollection.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("exception getconfig ");
                throw e;
            }

        }

      
    }
}
