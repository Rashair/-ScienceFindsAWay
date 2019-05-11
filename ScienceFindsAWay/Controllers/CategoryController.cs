using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        public string GetCategoryName(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM Categories ");
            sb.Append("WHERE CategoryId=id ");
            string sql = sb.ToString();

            return DbQuery(sql).FirstOrDefault().Name;
        }

        public IEnumerable<Category> GetCategoriesByMeetingId(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Categories.* ");
            sb.Append("FROM Categories ");
            sb.Append("JOIN MeetingCategoryMerge ON Categories.CategoryID=MeetingCategoryMerge.CategoryID ");
            sb.Append($"WHERE Categories.CategoryId={id} ");
            string sql = sb.ToString();

            return DbQuery(sql);
        }
    }
}
