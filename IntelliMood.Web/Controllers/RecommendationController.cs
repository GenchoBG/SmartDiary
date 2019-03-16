using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using IntelliMood.Data.Models;
using IntelliMood.Services.Interfaces;
using IntelliMood.Web.Models.RecommendationViewModels;
using IntelliMood.Web.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntelliMood.Web.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly IRecommendationService recommendationService;
        private readonly UserManager<User> userManager;

        public RecommendationController(IRecommendationService recommendationService, UserManager<User> userManager)
        {
            this.recommendationService = recommendationService;
            this.userManager = userManager;
        }

        public IActionResult Stats()
        {
            var recommendations = this.recommendationService.GetAll().ProjectTo<RecommendationListViewModel>().ToList();
            var users = this.userManager.Users.ProjectTo<UserListViewModel>().ToList();
            
            var model = new RecommendationTableViewModel()
            {
                Recommendations = recommendations,
                Users = users
            };

            return this.View(model);
        }
    }
}
