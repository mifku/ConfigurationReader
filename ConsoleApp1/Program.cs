using ConfigurationReader;
using System;
using System.Timers;

namespace ConsoleTest
{
    class Program
    {
        private static ConfigurationReader.ConfigurationReader cconfigReader;
        static void Main(string[] args)
        {
            
           
            cconfigReader = new ConfigurationReader.ConfigurationReader("consoletest" , "mongodb://127.0.0.1:27017/admin", 4000);
            
            do
            {

                Get();

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void Get()
        {
            try
            {
                Console.WriteLine($"{ DateTime.Now}  :     {cconfigReader.GetValue<int>("SiteName")}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

        }
    }
}
