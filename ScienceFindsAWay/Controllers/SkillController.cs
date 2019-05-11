using System.Collections.Generic;
using System.Data.SqlClient;
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
                            var name = reader.GetString(reader.GetOrdinal("Name"));
                            var generalID = reader.GetInt32(reader.GetOrdinal("CategoryGeneral"));
                            var mediumID = reader.GetInt32(reader.GetOrdinal("CategoryMedium"));
                            var specificID = reader.GetInt32(reader.GetOrdinal("CategorySpecific"));
                            var con = new CategoryController(this.Configuration);
                            places.Add(new Skill(index, name, con.GetCategoryName(generalID), con.GetCategoryName(mediumID), con.GetCategoryName(specificID)));
                        }
                    }
                }
            }
            return places;
        }

    }
}