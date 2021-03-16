using Microsoft.Extensions.Logging;
using System;

namespace CustomerGreeter
{
    public class Calculator
    {
        private readonly Tuple<double, double> _officeLocation = Tuple.Create(53.339428,-6.257664);
        private Tuple<double, double> _officeLocationInRadians;
        private const double RADIUS = 6371;
        private ILogger _logger;

        public Calculator()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Calculator>();
        }

        /// <summary>
        /// Calculates the distance of a location with a specified latitude and longitude from the office location
        /// Assumes the office location is provided
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public double CalculateDistanceFromOffice(double latitude, double longitude)
        {
            _logger.LogInformation($"Calculating distance of latitude: {latitude} and longitude: {longitude} from office location");

            double distance = 0;
            try
            {
                ConvertOfficeLocationToRadians(_officeLocation);
                var latR = ConvertToRadians(latitude);
                var longR = ConvertToRadians(longitude);
                var latitudeDifference = latR - _officeLocationInRadians.Item1;
                var longitudeDifference = longR - _officeLocationInRadians.Item2;
                var h = Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2) +
                    Math.Cos(_officeLocation.Item1) * Math.Cos(latR) *
                    Math.Sin(longitudeDifference / 2) * Math.Sin(longitudeDifference / 2);

               
                distance = 2 * RADIUS * Math.Sin(Math.Sqrt(h));
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Failed to calculate distance from office location. Error: {e}");
                throw;
            }
            return distance;
        }

      

        private double ConvertToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private void ConvertOfficeLocationToRadians(Tuple<double, double> location)
        {
            var latitude = ConvertToRadians(location.Item1);
            var longitude = ConvertToRadians(location.Item2);
            _officeLocationInRadians = Tuple.Create(latitude, longitude);
        }
    }
}
