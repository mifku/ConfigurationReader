using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader
{
    public interface IConfigurationRepository
    {
        Task<IEnumerable<ConfigurationEntitiy>> GetAllConfigurations();

        Task<IEnumerable<ConfigurationEntitiy>> GetConfigurationsOfGivenAppName(string name);

        Task<ConfigurationEntitiy> GetConfiguration(int id);

        // add new Configuration document
        Task<ConfigurationEntitiy> AddConfiguration(ConfigurationEntitiy item);

        // remove a single document / Configuration
        Task<bool> RemoveConfiguration(int id);

        // update just a single document / Configuration
        Task<ConfigurationEntitiy> UpdateConfiguration(ConfigurationEntitiy item);

        Task<bool> RemoveAllConfigurations();

    }
}
