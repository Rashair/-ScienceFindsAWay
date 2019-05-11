using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceFindsAWay.Models
{
    public class Skill
    {
        private string _categoryMedium;
        private string _categorySpecific;
        private string _categoryGeneral;

        public int SkillID { get; private set; }
        public string Name { get; set; }
        public string CategoryGeneral { get => _categoryGeneral; set => _categoryGeneral = value; }
        public string CategorySpecific { get => _categorySpecific; set => _categorySpecific = value; }
        public string CategoryMedium { get => _categoryMedium; set => _categoryMedium = value; }

        public Skill(int id, string n, string cg, string cs, string cm)
        {
            SkillID = id;
            Name = n;
            _categoryGeneral = cg;
            _categoryMedium = cm;
            _categorySpecific = cs;
        }

       
    }
}
