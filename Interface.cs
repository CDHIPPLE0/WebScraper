using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ScraperOne
{
    public partial class Interface : IInterface
    {
        private readonly ILogger<Interface> _log;
        private readonly IConfiguration _config;
        private readonly string filePathToURL = Environment.CurrentDirectory;
        private readonly string startPath = @"/search/sss?query=";
        private readonly string endPath = @"&purveyor-input=all&srchType=T";
         
        public Interface(ILogger<Interface> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}: Version{1} by {2}", _config.GetValue<string>("appName"), _config.GetValue<int>("version"), _config.GetValue<string>("Author"));
            Console.ResetColor();
            Console.WriteLine("Enter your key terms seperated by commas:");
            string keyTerms = Console.ReadLine();
            var searchTermsList = Regex.Split(keyTerms, @"\s*,\s*");
            var searchString = string.Join('+',searchTermsList);
            string[] list = File.ReadAllLines(filePathToURL + @"\siteListCl.txt");
            foreach (string query in list) 
            {
            var thisListItem = query;
            var urlHit = thisListItem + startPath + searchString + endPath;
            var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv: 85.0) Gecko / 20100101 Firefox / 85.0");
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                try
                {
                    var html = httpClient.GetStringAsync(urlHit);
                    Console.WriteLine(html.Result);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            string wait = Console.ReadLine();
        }

    }
}
