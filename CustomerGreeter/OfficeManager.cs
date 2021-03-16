using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerGreeter
{
    public class OfficeManager
    {
        private ILogger _logger;

        public OfficeManager()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<OfficeManager>();
        }

        public List<Customer> GetCustomersWithinDistance(List<Customer> customers, int distance)
        {
            var customersWithinDistance = new List<Customer>();
            var calculator = new Calculator();
            foreach(var customer in customers)
            {
                var distanceFromOffice = calculator.CalculateDistanceFromOffice(customer.Latitude, customer.Longitude);
                if (distanceFromOffice <= distance)
                    customersWithinDistance.Add(customer);
            }

            _logger.LogInformation($"Returning {customersWithinDistance.Count} customers which are located within {distance} km from the office");
            return customersWithinDistance.OrderBy(c => c.User_Id).ToList();
        }
    }
}
