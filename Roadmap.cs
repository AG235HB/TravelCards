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
        SearchTree tree = new SearchTree();
        List<Card> resultCards = new List<Card>();

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

        private void GetRoutePoint(Node lastNode, City fromCity, City currentCity, City requredCity)
        {
            Node currentNode = new Node(lastNode, currentCity.cityName);
            tree.AddNode(lastNode, currentCity.cityName);
            //roads.Add(new Road(fromCity, currentCity));
            if (currentCity == requredCity)
            {
                //roads.Add(new Road(fromCity, currentCity));
                /*List<Card> */resultCards = new List<Card>();
                tree.GetRoute(resultCards, currentNode);
                return;
            }
            else if (currentCity.citiesOut.Count > 0)
            {
                foreach (City c in currentCity.citiesOut)
                {
                    //if (c == requredCity)
                    //    GetRoutePoint(currentNode, currentCity, c, requredCity);
                    //else
                        GetRoutePoint(currentNode, currentCity, c, requredCity);
                }
            }
            //else
            //MessageBox.Show("Невозможно найти путь");
            //return;
        }

        public List<Card> CalculateRoute(string start, string finish)
        {
            //City startCity = GetCity(start), finishCity = GetCity(finish);
            //List<Road> route = new List<Road>();
            //GetRoutePoint(route, null, startCity, finishCity);
            //return route;

            //tree.AddNode(null, start);
            GetRoutePoint(null, null, GetCity(start), GetCity(finish));
            return resultCards;
            //MessageBox.Show("");
        }

        public bool GetCities(string start, string finish)
        {
            bool machD = false, machA = false;
            foreach(City c in cities)
            {
                if (c == GetCity(start))
                    machD = true;
                if (c == GetCity(finish))
                    machA = true;
            }
            if (machD && machA)
                return true;
            else return false;
        }
    }
}
