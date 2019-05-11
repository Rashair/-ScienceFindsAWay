using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScienceFindsAWay.Models;

namespace ScienceFindsAWay.Controllers
{
    [Route("api/[controller]")]
    public class SkillController : Controller
    {
        private IConfiguration Configuration { get; }

        public SkillController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IEnumerable<Skill> DbQuery(string sqlQuery)
        {
            var places = new List<Skill>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = reader.GetInt32(reader.GetOrdinal("SkillID"));
                            var general = reader.GetInt32(reader.GetOrdinal("CategoryGeneral"));
                            var medium = reader.GetInt32(reader.GetOrdinal("CategoryMedium"));
                            var specific = reader.GetInt32(reader.GetOrdinal("CategorySpecific"));
                            //convert categoryid to name
                            places.Add(new Skill(index, general, medium, specific));
                        }
                    }
                }
            }
            return places;
        }

    }
}