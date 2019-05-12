using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        public IEnumerable<Skill> DbQuery(string sqlQuery)
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
                            var name = reader.GetString(reader.GetOrdinal("Name"));
                            var generalID = reader.GetInt32(reader.GetOrdinal("CategoryGeneral"));
                            var mediumID = reader.GetInt32(reader.GetOrdinal("CategoryMedium"));
                            var specificID = reader.GetInt32(reader.GetOrdinal("CategorySpecific"));

                            var con = new CategoryController(this.Configuration);
                            var generalName = (con.GetCategoryName(generalID) as JsonResult).Value as string;
                            var mediumName = (con.GetCategoryName(mediumID) as JsonResult).Value as string;
                            var specificName = (con.GetCategoryName(specificID) as JsonResult).Value as string;

                            places.Add(new Skill(index, name, generalName, mediumName, specificName));
                            Debug.WriteLine(places.First().CategoryGeneral);
                        }
                    }
                }
            }
            return places;
        }

        public IActionResult GetUserSkills(int id)
        {
            return Json(DbQuery($"SELECT * FROM Skill s JOIN UserSkillMerge usm ON s.SkillID=usm.SkillID WHERE usm.UserID={id}"));
        }
    }
}