namespace TravelCards
{
    class CardSearch
    {
        string _A, _D;

        public CardSearch(string depPoint, string arrPoint)
        {
            _D = depPoint;
            _A = arrPoint;
        }

        public bool CardMatch(Card card)
        {
            if ((card.DeparturePoint == _D) && (card.ArrivalPoint == _A))
                return true;
            else
                return false;
        }
    }
}
