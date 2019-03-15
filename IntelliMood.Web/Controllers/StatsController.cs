using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelliMood.Web.Controllers
{
    public class StatsController : Controller
    {
        private readonly IMoodService moodService;

        public StatsController(IMoodService moodService)
        {
            this.moodService = moodService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult GetMonthly(int month, int year)
        {
            var moods = this.moodService.GetAllMonthly(month, year).Select(m => m.Type).ToList();

            return this.Json(moods);
        }

        public IActionResult GetYearly(int year)
        {
            var moods = this.moodService.GetAllYearly(year).Select(m => m.Type).ToList();

            return this.Json(moods);
        }
    }
}
