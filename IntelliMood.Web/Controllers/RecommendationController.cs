using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using IntelliMood.Data.Models;
using IntelliMood.Services.Interfaces;
using IntelliMood.Web.Models.RecommendationViewModels;
using IntelliMood.Web.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntelliMood.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RecommendationController : Controller
    {
        private readonly IRecommendationService recommendationService;
        private readonly IRecommender recommender;
        private readonly UserManager<User> userManager;

        public RecommendationController(IRecommendationService recommendationService, UserManager<User> userManager, IRecommender recommender)
        {
            this.recommendationService = recommendationService;
            this.userManager = userManager;
            this.recommender = recommender;
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

        public IActionResult GetUnpopulatedArray()
        {
            return this.Json(this.recommender.GetUnpopulatedArray());
        }

        public IActionResult GetPopulatedArray()
        {
            return this.Json(this.recommender.GetPopulatedArray());
        }
    }
}
