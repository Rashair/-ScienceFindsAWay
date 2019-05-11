using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScienceFindsAWay.Controllers
{
    [Route("api/[controller]")]
    public class PlacesController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<Place> GetAllPlaces()
        {
            
        }
    }
}