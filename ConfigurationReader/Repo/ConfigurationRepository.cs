using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationReader;
using ConfigurationReader.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConfigurationReader
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ConfigurationContext _context = null;

        public ConfigurationRepository(Settings settings)
        {
            _context = new ConfigurationContext(settings);
        }

        public async Task<IEnumerable<ConfigurationEntitiy>> GetAllConfigurations()
        {
            try
            {
                return await _context.Configurations
                    .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ConfigurationEntitiy> GetConfiguration(int id)
        {
            try
            {

                return await _context.Configurations
                    .Find(config => config.ID == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task<ConfigurationEntitiy> AddConfiguration(ConfigurationEntitiy item)
        {
            try
            {
                int lastId = _context.Configurations.AsQueryable().Max(c => c.ID).Value;
                item.ID = lastId + 1;
                await _context.Configurations.InsertOneAsync(item);
                return _context.Configurations.Find(c => c.ID == item.ID).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RemoveConfiguration(int id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Configurations.DeleteOneAsync(
                        Builders<ConfigurationEntitiy>.Filter.Eq("ID", id));

                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ConfigurationEntitiy> UpdateConfiguration(ConfigurationEntitiy item)
        {

            try
            {

                await _context.Configurations.ReplaceOneAsync(
                    doc => doc.ID == item.ID, item);
                return GetConfiguration(item.ID.ToInt32()).Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RemoveAllConfigurations()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Configurations.DeleteManyAsync(new BsonDocument());
                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ConfigurationEntitiy>> GetConfigurationsOfGivenAppName(string name)
        {
            try
            {
                return await _context.Configurations
                    .Find(Builders<ConfigurationEntitiy>.Filter.Where(c=>c.ApplicationName.ToLower() == name.ToLower() && c.IsActive == 1)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
