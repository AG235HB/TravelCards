using System.Collections.Generic;

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
            if (currentCity == requredCity)
            {
                resultCards = new List<Card>();
                tree.GetRoute(resultCards, currentNode);
                return;
            }
            else if (currentCity.citiesOut.Count > 0)
                foreach (City c in currentCity.citiesOut)
                    GetRoutePoint(currentNode, currentCity, c, requredCity);
        }

        public List<Card> CalculateRoute(string start, string finish)
        {
            GetRoutePoint(null, null, GetCity(start), GetCity(finish));
            return resultCards;
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
