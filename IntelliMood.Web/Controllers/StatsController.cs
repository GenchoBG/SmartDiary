using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
using IntelliMood.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntelliMood.Web.Controllers
{
    public class StatsController : Controller
    {
        private readonly IMoodService moodService;
        private readonly UserManager<User> userManager;

        public StatsController(IMoodService moodService, UserManager<User> userManager)
        {
            this.moodService = moodService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult GetDaily(int day, int month, int year)
        {
            var moods = this.moodService.GetAllDaily(day, month, year).Where(m => m.UserId == this.userManager.GetUserId(this.User)).Select(m => m.Type).ToList();

            return this.Json(moods);
        }

        public IActionResult GetMonthly(int month, int year)
        {
            var moods = this.moodService.GetAllMonthly(month, year).Where(m => m.UserId == this.userManager.GetUserId(this.User)).Select(m => m.Type).ToList();

            return this.Json(moods);
        }

        public IActionResult GetYearly(int year)
        {
            var moods = this.moodService.GetAllYearly(year).Where(m => m.UserId == this.userManager.GetUserId(this.User)).Select(m => m.Type).ToList();

            return this.Json(moods);
        }
    }
}
