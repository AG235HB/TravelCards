using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCards
{
    public class Node
    {
        public readonly string DepCity;
        public readonly List<Node> ArrCities = new List<Node>();

        public Node(string dep_city)
        {
            DepCity = dep_city;
        }

        public void AddCity(Node ArrivalNode)
        {
            ArrCities.Add(ArrivalNode);
        }

        public void AddCity(string arr_city)
        {
            if(ArrCities != null)
                foreach (Node n in ArrCities)
                    if (arr_city == n.DepCity)
                        return;
            Node subNode = new Node(arr_city);
            ArrCities.Add(subNode);
            //ArrCities.Add(new Node(arr_city));
        }
        

    }
}
