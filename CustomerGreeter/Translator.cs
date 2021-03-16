using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerGreeter
{
    public class Translator
    {
        private ILogger _logger;
        public Translator()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<TextParser>();
        }

        /// <summary>
        /// Returns a list of Customers from a collection of strings
        /// Assumes the strings are JSON formatted
        /// </summary>
        /// <param name="entries"></param>
        public List<Customer> TranslateEntriesToCustomers(string[] entries)
        {
            var customers = new List<Customer>();
            _logger.LogInformation("Building the customer list from string entries");

            foreach (var entry in entries)
            {
                try
                {
                    var customer = JsonConvert.DeserializeObject<Customer>(entry);
                    customers.Add(customer);
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"Failed to deserialize entry. Customer will not be added to output. Error: {e}");
                    continue;  //if one entry fails, we log error but continue with the rest of the entries
                }
            }
            return customers;
        }
    }
}
