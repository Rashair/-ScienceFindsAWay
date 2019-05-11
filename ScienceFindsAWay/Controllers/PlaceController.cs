using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceFindsAWay.Models;
using Microsoft.Extensions.Configuration;
namespace ScienceFindsAWay.Controllers
{
    [Route("api/[controller]")]
    public class PlaceController : Controller
    {
        private IConfiguration Configuration { get; }

        public PlaceController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpGet("[action]")]
        public IEnumerable<Place> GetAllPlaces()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append("FROM Places");
            string sql = sb.ToString();
            return DbQuery(sql);
        }

        private IEnumerable<Place> DbQuery(string sqlQuery)
        {
            var places = new List<Place>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
            {
                connection.Open();
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var index = reader.GetInt32(reader.GetOrdinal("PlaceID"));
                            var name = reader.GetString(reader.GetOrdinal("Name"));
                            var address = reader.GetString(reader.GetOrdinal("Address"));
                            var description = reader.GetString(reader.GetOrdinal("Description"));
                            places.Add(new Place(index, name, address, description));
                        }
                    }
                }
            }
            return places;
        }
    }
}