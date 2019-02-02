using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConfigurationReader.Data.Entities;

namespace ConfigurationReader.Data
{
    public interface IDbProxy
    {
        Task<IEnumerable<ConfigurationEntity>> GetConfigurations(string connectionString, string serviceName, int connectionTimeout);
    }
}
