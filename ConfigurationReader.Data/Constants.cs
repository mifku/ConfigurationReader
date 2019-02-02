using System;
using System.Configuration;


namespace ConfigurationReader.Data
{
   public static class Constants
   {
       public static string ApplicationNameColumn = ConfigurationManager.AppSettings["ApplicationNameColumnName"];
       public static string IsActiveColumn = ConfigurationManager.AppSettings["IsActiveColumnName"];
       public static string CollectionName = ConfigurationManager.AppSettings["CollectionName"];

   }
    public  enum ConfigurationActiveness
    {
        Active = 1,
        Passive = 0
    }
}
