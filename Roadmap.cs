using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.GraphModel;
using System.Runtime.Serialization;
using System.Windows;

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

        public City GetCity(string cityName)
        {
            foreach(City c in cities)
                if (c.cityName == cityName)
                    return c;
            return null;
        }

        private void GetRoutePoint(List<Road> roads, City fromCity, City currentCity, City requredCity)
        {
            roads.Add(new Road(fromCity, currentCity));
            if (currentCity == requredCity)
            {
                //roads.Add(new Road(fromCity, currentCity));
                return;
            }
            else if (currentCity.citiesOut.Count > 0)
            {
                foreach (City c in currentCity.citiesOut)
                {
                    if (c == requredCity)
                        GetRoutePoint(roads, currentCity, c, requredCity);
                    else
                        GetRoutePoint(roads, currentCity, c, requredCity);
                }
            }
            else
                //MessageBox.Show("Невозможно найти путь");
                return;
        }

        public List<Road> CalculateRoute(string start, string finish)
        {
            City startCity = GetCity(start), finishCity = GetCity(finish);
            List<Road> route = new List<Road>();
            GetRoutePoint(route, null, startCity, finishCity);
            return route;
        }
    }
}
