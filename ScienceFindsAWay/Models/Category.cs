using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceFindsAWay.Models
{
    public class Category
    {
        public Category(int categoryID, string name, int categoryLevel)
        {
            CategoryID = categoryID;
            Name = name;
            CategoryLevel = categoryLevel;
        }

        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int CategoryLevel { get; set; }


    }
}
