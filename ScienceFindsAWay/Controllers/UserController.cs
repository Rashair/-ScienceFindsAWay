using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
                            //var skillsJson = reader

                            userList.Add(new User(name, surname, university, faculty, mail, id, new Skill[] { }, username, password, passwordSalt));
                        }
                    }
                }
            }
            return userList;

        }

        public class Credentials
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpPost("[action]")]
        public IActionResult LogIn([FromBody]Credentials cred)
        {
            string sql = $"SELECT * FROM Users WHERE Username='{cred.username}'";
            var user = DbQuery(sql).FirstOrDefault();

            return user != null && user.CheckPassword(cred.password) ? Json(user) : null;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllUsers()
        {
            return Json(DbQuery("SELECT * FROM Users"));
        }

        [HttpGet("[action]")]
        public IActionResult GetUsersWithID(int id)
        {
            return Json(DbQuery($"SELECT * FROM Users where UserID={id}").FirstOrDefault());
        }

        [HttpGet("[action]")]
        public IActionResult GetUsersByMeetingId(int id)
        {
            return Json(DbQuery($"SELECT u.* FROM Users u JOIN MeetingUserMerge m on m.UserID=u.UserID and m.MeetingID={id}"));
        }

        [HttpGet("[action]")]
        public IActionResult AddUser(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            user.PasswordSalt = Convert.ToBase64String(salt);
            string sql = "Insert into Users (Name,Surname,University,Faculty,Mail,Password,Username,PasswordSalt)" +
                $" values ('{user.Name}','{user.Surname}','{user.University}','{user.Faculty}','{user.Mail}'"+
                $",'{user.HashPassword(user._password)}','{user.Username}','{user.PasswordSalt}')";
                try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}