using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScienceFindsAWay.Models;

namespace ScienceFindsAWay.Controllers
{
    [Route("api/[controller]")]
    public class PlaceController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<Place> GetAllPlaces()
        {
            return null;
        }
    }
}