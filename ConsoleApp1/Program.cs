using ConfigurationReader;
using System;
using System.Timers;

namespace ConsoleApp1
{
    class Program
    {
        private static ConfigurationReader.ConfigurationReader cconfigReader;
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            cconfigReader = new ConfigurationReader.ConfigurationReader("SERVICE-A", "mongodb://127.0.0.1:27017/admin", 4000);
            
            do
            {

                Get();

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void Get()
        {
            try
            {
                Console.WriteLine($"{ DateTime.Now}  :     {cconfigReader.GetValue<string>("SiteName")}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

        }
    }
}
