using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceFindsAWay.Models
{
    public class Place
    {
        public int Index { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public Place(int i, string n, string a, string d)
        {
            Index = i;
            Name = n;
            Address = a;
            Description = d;
        }
    }
}
