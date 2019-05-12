using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScienceFindsAWay.Models;

namespace ScienceFindsAWay.Controllers
{
    public class CategoryController : Controller
    {
        private IConfiguration Configuration { get; }

        public CategoryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IEnumerable<Category> DbQuery(string sql)
        {
            var categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SFAWHackBase")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                            var name = reader.GetString(reader.GetOrdinal("Name"));
                            var level = reader.GetInt32(reader.GetOrdinal("CategoryLevel"));

                            categories.Add(new Category(id, name, level));
                        }
                    }
                }
            }
            return categories;
        }

        [HttpGet("[action]")]
        public IActionResult GetCategoryName(int id)
        {
            return Json(DbQuery($"SELECT * FROM Categories WHERE CategoryId={id}").FirstOrDefault().Name);
        }

        [HttpGet("[action]")]
        public IActionResult GetCategoriesByMeetingId(int id)
        {
            string sql = "SELECT Categories.* "+
                "FROM Categories " +
                "JOIN MeetingCategoryMerge ON Categories.CategoryID=MeetingCategoryMerge.CategoryID " +
                $"WHERE Categories.CategoryId={id} ";

            return Json(DbQuery(sql));
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCategories()
        {
            return Json(DbQuery("SELECT * FROM Categories"));
        }
    }
}
