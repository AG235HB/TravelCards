using System;

namespace TravelCards
{
    public class Node
    {
        public readonly Node parent;
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
