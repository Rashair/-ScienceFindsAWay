using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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



    }
}