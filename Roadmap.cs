using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Runtime.Serialization;

namespace TravelCards
{
    class Roadmap
    {
        public readonly List<City> cities = new List<City>();
        public readonly List<Road> roads = new List<Road>();

        public Roadmap() { }

        public void AddCity(City city)
        {
            foreach (City c in cities)
                if (city.cityName == c.cityName)
                    return;
            cities.Add(city);
        }

        public void AddRoad(Road road)
        {
            City cityFrom = GetCity(road.from.cityName), cityTo = GetCity(road.to.cityName);
            cityFrom.AddProceedingRoute(road.to);
            cityTo.AddIncomingRoute(road.from);
            roads.Add(road);
        }

        private City GetCity(string cityName)
        {
            foreach(City c in cities)
                if (c.cityName == cityName)
                    return c;
            return null;
        }
    }
}
