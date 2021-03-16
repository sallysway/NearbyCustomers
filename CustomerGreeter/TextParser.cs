using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace CustomerGreeter
{
    public class TextParser
    {
        private ILogger _logger;

        public TextParser()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<TextParser>();
        }

        /// <summary>
        /// Returns a list of lines from the file at a given path. 
        /// Empty lines are ignored
        /// </summary>
        /// <param name="path"></param>
        public string[] ReadEntriesInFile()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "../CustomerGreeter/customers.txt";
            _logger.LogInformation($"Reading file from path {path} ...");
            var entries = new string[0];
            try
            {
                entries = File.ReadAllLines(path).
                    Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to read file from path {path} due to error: {e}");
                throw;
            }

            _logger.LogInformation($"Returning {entries.Length} entries from file: {path}");
            return entries;
        }
    }
}
