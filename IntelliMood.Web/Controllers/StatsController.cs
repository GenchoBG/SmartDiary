using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IntelliMood.Web.Controllers
{
    public class StatsController : Controller
    {

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
