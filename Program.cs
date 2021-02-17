using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Serilog;

namespace ScraperOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder(); //creates new instance of the IConfigurationBuilder in BuildConfig

            BuildConfig(builder); //passes in the new ConfigurationBuilder to be directed to the appsettings.json

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build()) //reads from the builder.Build which is the json file, where we set up all serilog config
                .Enrich.FromLogContext() //adds extra serilog features
                .WriteTo.File("log.txt", rollOnFileSizeLimit: true) //writes to a log txt file with maximum 1gb by default
                .CreateLogger();
        }

        static void BuildConfig(IConfigurationBuilder builder)

        {

            builder.SetBasePath(Directory.GetCurrentDirectory()) //sets up a place to talk to the json file in current directory, reload on change.
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        }
    }
}
