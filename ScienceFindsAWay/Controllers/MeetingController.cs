using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScienceFindsAWay.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScienceFindsAWay.Controllers
{
    [Route("api/[controller]")]
    public class MeetingController : Controller
    {
        private IConfiguration Configuration { get; }

        public MeetingController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IEnumerable<Meeting> DbQuery(string sql)
        {
            var placeControler = new PlaceController(this.Configuration);
            var categoryControler = new CategoryController(this.Configuration);
            var userControler = new UserController(this.Configuration);
            var userList = new List<Meeting>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetInt32(reader.GetOrdinal("MeetingID"));
                            var name = reader.GetString(reader.GetOrdinal("Name"));
                            var date = reader.GetDateTime(reader.GetOrdinal("Date"));
                            var place =placeControler.GetPlaceById(reader.GetInt32(reader.GetOrdinal("Place")));
                            var categories = categoryControler.GetCategoriesByMeetingId(id);
                            var participants = userControler.GetUsersByMeetingId(id);

                            userList.Add(new Meeting(id,name,date,place,categories,participants.ToList()));
                        }
                    }
                }
            }
            return userList;

        }

        [HttpGet("[action]")]
        public IEnumerable<Meeting> GetAllMeetings()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append("FROM Meetings");
            string sql = sb.ToString();

            return DbQuery(sql);
        }
    }
}
