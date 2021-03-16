using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomerGreeter
{
    public class TextWriter
    {
        private ILogger _logger;

        public TextWriter()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<TextWriter>();
        }
        public void WriteToFile(List<Customer> customers)
        {
            _logger.LogInformation($"Writing {customers.Count} entries to output file");
            try
            {
                var orderedCustomers = customers.OrderBy(c => c.User_Id).ToList();
                using (var streamFile = File.OpenWrite(AppDomain.CurrentDomain.BaseDirectory + "/output.txt"))
                {
                    foreach (var customer in orderedCustomers)
                    {
                        var entry = FormatEntry(customer.User_Id, customer.Name);
                        var entryBytes = Encoding.UTF8.GetBytes(entry);
                        streamFile.Write(entryBytes, 0, entryBytes.Length);
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to write entries to file. Error: {e}");
                throw;
            }
        }

        private string FormatEntry(int userId, string userName)
        {
            return $"Username: {userName}. User Id: {userId}" + Environment.NewLine;
        }

    }
}
