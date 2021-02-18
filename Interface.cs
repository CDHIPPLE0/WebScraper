using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
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
        private readonly string endPath = @"&purveyor-input=all";

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
            var urlHit = list[0] + startPath + searchString + endPath;
            GetHtml("urlHit");
            string wait = Console.ReadLine();
        }
        private void GetHtml(string v)
        {
            throw new NotImplementedException();
        }

    }
}
