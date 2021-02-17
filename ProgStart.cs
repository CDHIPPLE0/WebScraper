using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace ScraperOne
{
    public class ProgStart : IProgStart
    {
        private readonly ILogger<ProgStart> _log;
        private readonly IConfiguration _config;

        public ProgStart(ILogger<ProgStart> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            Console.WriteLine("Enter your key terms seperated by commas:");
            string keyTerms = Console.ReadLine();
            string[] searchTermsList = keyTerms.Split('.', ',');
            foreach (var substring in searchTermsList)
            {
            Console.WriteLine(substring);

            }
            string wait = Console.ReadLine();
        }
    }
}
