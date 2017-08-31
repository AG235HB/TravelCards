using System.Collections.Generic;

namespace TravelCards
{
    class SearchTree
    {
        public readonly List<Node> nodes = new List<Node>();

        public void AddNode(Node Node, string Name)
        {
            nodes.Add(new Node(Node, Name));
        }

        public void GetRoute(List<Card> cards, Node machNode)
        {
            Node node = machNode;
            while (node.parent != null)
            {
                cards.Add(new Card(node.GetParent().name, node.name));
                node = node.parent;
            }
        }
    }
}
