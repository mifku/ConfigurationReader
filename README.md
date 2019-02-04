# ConfigurationReader

ConfigurationReader is a class library that provides you congifurations from a db by your applicationname and db connectionstring.
You can call ConfigurationReader

new ConfigurationReader.ConfigurationReader(string applicationName , string dbConnString, int RefreshIntervalInMs);

then all you need to do to get your configuration is

calling GetValue function of ConfigurationReader like this

configReader.GetValue<string>("SiteName")

ConfigurationReader refreshes name given application's active configurations in every given interval time. 
And it try to parse value of config to your desired type. Throws exception if couldnt.
It will return you last succesful list of configurations. And keep working if cannot connect to given db.

