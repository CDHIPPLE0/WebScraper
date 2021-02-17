using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogInformation("Run number {runNumber} ", i);
            }
        }
    }
}
