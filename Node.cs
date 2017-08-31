using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCards
{
    public class Node
    {
        public readonly Node parent/* = new Node()*/;
        public readonly string name = String.Empty;
        
        public Node(Node Parent, string Name)
        {
            parent = Parent;
            name = Name;
        }

        public Node GetParent()
        { return parent; }
    }
}
