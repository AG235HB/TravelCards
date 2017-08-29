using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCards
{
    class Road
    {
        public readonly City from, to;

        public Road(City From, City To)
        {
            from = From;
            to = To;
        }
    }
}
