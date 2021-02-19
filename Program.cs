using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ScraperOne
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder(); //creates new instance of the IConfigurationBuilder in BuildConfig

            BuildConfig(builder); //passes in the new ConfigurationBuilder to be directed to the appsettings.json

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build()) //reads from the builder.Build which is the json file, where we set up all serilog config
                .Enrich.FromLogContext() //adds extra serilog features
                .WriteTo.File("log.txt", rollOnFileSizeLimit: true) //writes to a log txt file with maximum 1gb by default
                .CreateLogger();

            Log.Logger.Information("Application starting"); //outputs to log when program runs!

            var host = Host.CreateDefaultBuilder() //loads IConfiguration from, DOTNET_, "appsettings.json"
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IInterface, Interface>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<Interface>(host.Services);
           await svc.Run();
        }

        private static void ConfigureServices(Action<object, object> p)
        {
            throw new NotImplementedException();
        }

        static void BuildConfig(IConfigurationBuilder builder)

        {

            builder.SetBasePath(Directory.GetCurrentDirectory()) //sets up a place to talk to the json file in current directory, reload on change.
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        }
    }
}
