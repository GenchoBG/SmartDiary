using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliMood.Web.Controllers {
    public class CalendarController : Controller{
        public IActionResult Index() {
            return View();
        }
    }
}
