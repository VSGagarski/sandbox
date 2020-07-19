using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Extensions.Configuration;

namespace EnvVariableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> argsList = new List<string>(args);
            argsList.ForEach(item => System.Console.WriteLine(item));
            Console.WriteLine("Hello World!");
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();


            var vars = Configuration.GetValue<string>("Host");//Environment.GetEnvironmentVariable("Hostds");

            // foreach (DictionaryEntry item in vars)
            // {
            // Console.WriteLine($"Key = {item.Key} Value = {item.Value}");

            // }
            System.Console.WriteLine(vars);
        }
    }
}
