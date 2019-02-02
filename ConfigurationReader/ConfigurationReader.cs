using ConfigurationReader.Data.Proxies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
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
        private static int _repeatCount = 0;
        private static Timer _timer;
        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            _applicationName = applicationName;
            _connectionString = connectionString;
            _configurations = new Dictionary<string, string>();
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;


            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMilliseconds(_refreshTimerIntervalInMs);
            _timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine(Process.GetCurrentProcess().Threads.Count);
                SyncConfigurationListToDb();
            }, null, startTimeSpan, periodTimeSpan);
        }

        private  void DataUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SyncConfigurationListToDb();
        }

        private  async void SyncConfigurationListToDb()
        {
            var proxy = new MongoDbProxy();
            _repeatCount++;
            try
            {
                var list = await proxy.GetConfigurations(_connectionString, _applicationName,_refreshTimerIntervalInMs/2);
                Console.WriteLine(DateTime.Now.ToString() + " list await " + _repeatCount);
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
                Console.WriteLine("exception syncconfig " + _repeatCount);
            }
            finally
            {
                _firstInit = true;
            }
        }

        public T GetValue<T>(string key)
        {
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff") +" Get Value before spin wait");
            try
            {
                SpinWait.SpinUntil(() => _firstInit == true,5000);
                {
                    Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff")  +" Get Value after spin wait");
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
