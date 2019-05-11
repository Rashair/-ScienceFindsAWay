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
        [HttpGet("[action]")]
        public IEnumerable<string> GetAllPlaces()
        {
            var l = new List<string>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT *");
                sb.Append("FROM Places");
                string sql = sb.ToString();
 
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l.Add(reader.GetString(1));
                        }
                    }
                }
            }
            return l;
        }

        private IConfiguration Configuration { get; }

        public PlaceController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}