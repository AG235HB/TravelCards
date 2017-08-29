namespace TravelCards
{
    class Card
    {
        public string DeparturePoint { get; }
        public string ArrivalPoint { get; }

        public Card(string DP, string AP)
        {
            DeparturePoint = DP;
            ArrivalPoint = AP;
        }
    }
}
