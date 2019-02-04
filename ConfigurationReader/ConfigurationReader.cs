using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace ConfigurationReader
{
    public class ConfigurationReader
    {
        private static string _applicationName, _connectionString;
        private static int _refreshTimerIntervalInMs;
        private static Dictionary<string, string> _configurations;
        private static bool _firstInit = false;
        private static Timer _timer;
        private static ConfigurationRepository _configurationRepository;
        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            string callingApp = Assembly.GetCallingAssembly().GetName().Name;
            _applicationName = applicationName;
            _connectionString = connectionString;
            _configurations = new Dictionary<string, string>();
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
            Settings setting = new Settings();
            setting.TimeoutDurationInMs = _refreshTimerIntervalInMs / 2;
            setting.ConnectionString = _connectionString;
            setting.Database = "admin";
            _configurationRepository = new ConfigurationRepository(setting);

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMilliseconds(_refreshTimerIntervalInMs);


            if (callingApp.ToLower() == applicationName.ToLower())
            {
                _timer = new System.Threading.Timer((e) =>
                {
                    //Console.WriteLine(Process.GetCurrentProcess().Threads.Count);
                    SyncConfigurationListToDb();
                }, null, startTimeSpan, periodTimeSpan);
            }
            else
            {
                _firstInit = true;
            }

           
            
        }

        private  void DataUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SyncConfigurationListToDb();
        }

        private  async void SyncConfigurationListToDb()
        {
            try
            {
                var list =await _configurationRepository.GetConfigurationsOfGivenAppName(_applicationName);
                _configurations.Clear();
                foreach (var configurationEntity in list)
                {
                    if (!_configurations.ContainsKey(configurationEntity.Name))
                    {
                        _configurations.Add(configurationEntity.Name, configurationEntity.Value);
                    }
                    else
                    {
                        _configurations[configurationEntity.Name] = configurationEntity.Value;
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                _firstInit = true;
            }
        }

        public T GetValue<T>(string key)
        {

            try
            {
                SpinWait.SpinUntil(() => _firstInit == true,5000);
                {
                    
                    return (T)Convert.ChangeType(_configurations[key], typeof(T));
                }
                
            }
            catch (Exception e)
            {
                throw;

            }

        }
    }
}
