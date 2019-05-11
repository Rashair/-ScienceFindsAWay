using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScienceFindsAWay.Models;

namespace ScienceFindsAWay.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IConfiguration Configuration { get; }

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IEnumerable<User> DbQuery(string sql)
        {
            var userList = new List<User>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetString(reader.GetOrdinal("Name"));
                            var surname = reader.GetString(reader.GetOrdinal("Surname"));
                            var university = reader.GetString(reader.GetOrdinal("University"));
                            var faculty = reader.GetString(reader.GetOrdinal("Faculty"));
                            var mail = reader.GetString(reader.GetOrdinal("Mail"));
                            var username = reader.GetString(reader.GetOrdinal("Username"));
                            var password = reader.GetString(reader.GetOrdinal("Password"));
                            var passwordSalt = reader.GetString(reader.GetOrdinal("PasswordSalt"));
                            var id = reader.GetInt32(reader.GetOrdinal("UserID"));


                            userList.Add(new User(name, surname, university, faculty, mail, id, null, username, password, passwordSalt));
                        }
                    }
                }
            }
            return userList;

        }

        public class Credentials {
            public string username {get; set;}
            public string password {get; set;}
        }

        [HttpPost("[action]")]
        public IActionResult LogIn([FromBody]Credentials credentials)
        {
            string username = credentials.username;
            string password = credentials.password;

            string sql = $"SELECT * FROM Users WHERE Username={username}";

            var user = DbQuery(sql).FirstOrDefault();
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                    password: password,
                                                    salt: Convert.FromBase64String(user.PasswordSalt),
                                                    prf: KeyDerivationPrf.HMACSHA1,
                                                    iterationCount: 10000,
                                                    numBytesRequested: 256 / 8));

            return user.CheckPassword(hashed) ? Json(user) : Json("test response");
        }

        [HttpGet("[action]")]
        public IEnumerable<User> GetAllUsers()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM Users ");
            string sql = sb.ToString();

            return DbQuery(sql);
        }

        [HttpGet("[action]")]
        public User GetUsersWithID(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append($"FROM Users where UserID={id} ");
            string sql = sb.ToString();

            return DbQuery(sql)?.FirstOrDefault();
        }

        [HttpGet("[action]")]
        public IEnumerable<User> GetUsersByMeetingId(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT u.* ");
            sb.Append($"FROM Users u ");
            sb.Append($"JOIN MeetingUserMerge m on m.UserID = u.UserID and m.MeetingID = {id} ");
            string sql = sb.ToString();

            return DbQuery(sql);
        }
    }
}