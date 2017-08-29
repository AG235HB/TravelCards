using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCards
{
    class City
    {
        public readonly string cityName = String.Empty;
        public readonly List<City> citiesIn = new List<City>();
        public readonly List<City> citiesOut = new List<City>();

        public City(string name)
        { cityName = name; }

        public void AddIncomingRoute(City from)
        { citiesIn.Add(from); }

        public void AddProceedingRoute(City to)
        { citiesOut.Add(to); }
    }
}
