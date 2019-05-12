using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScienceFindsAWay.Models;

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
                            var placeJson = placeControler.GetPlaceById(reader.GetInt32(reader.GetOrdinal("PlaceID")));
                            var categoriesJson = categoryControler.GetCategoriesByMeetingId(id);
                            var participantsJson = userControler.GetUsersByMeetingId(id);

                            var place = (placeJson as JsonResult).Value as Place;
                            var categories = (categoriesJson as JsonResult).Value as List<Category>;
                            var participants = (participantsJson as JsonResult).Value as List<User>;

                            userList.Add(new Meeting(id,name,date,place,categories.ToList(),participants.ToList()));
                        }
                    }
                }
            }
            return userList;

        }

        [HttpGet("[action]")]
        public IActionResult GetAllMeetings()
        {
            return Json(DbQuery("SELECT * FROM Meetings"));
        }

        [HttpGet("[action]")]
        public IActionResult GetAllUserMeetings(int id)
        {
            string sql = "SELECT * " +
                "FROM Meetings m " +
                "JOIN  MeetingUserMerge mum ON m.MeetingID=mum.MeetingID " +
                $"WHERE mum.UserID={id}";

            return Json(DbQuery(sql));
        }
    }
}
