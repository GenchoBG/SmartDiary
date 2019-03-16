using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntelliMood.Data.Models;
using IntelliMood.Services;
using IntelliMood.Services.Interfaces;
using IntelliMood.Web.Models.ChatViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntelliMood.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;
		private readonly IMapper mapper;
        private readonly IEmotionGetter emotionGetter;
        private readonly IMoodService moodService;
        private readonly IRecommendationService recommendationService;
        private readonly IRecommender recommender;

        public ChatController(IChatService chatService, UserManager<User> userManager, IMapper mapper, IEmotionGetter emotionGetter, IMoodService moodService, IRecommender recommender, IRecommendationService recommendationService)
        {
            this.chatService = chatService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.emotionGetter = emotionGetter;
            this.moodService = moodService;
            this.recommender = recommender;
            this.recommendationService = recommendationService;
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            var model = this.mapper.Map<ChatIndexViewModel>(user);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult CreateMessage(MessageCreateViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var currentUserId = this.userManager.GetUserId(this.User);
            var message = this.chatService.AddMessage(data.Message, currentUserId, false);

            var mood = this.emotionGetter.GetEmotionFromText(data.Message);
            var moodMessage = $"I think you are feeling {mood}";
            if (mood == "Sadness")
            {
                var recommendation = this.recommender.RecommendMusic(currentUserId, mood);
                

                moodMessage += Environment.NewLine;
                moodMessage += $"I think {recommendation.Content} will make you feel better!";
                moodMessage += Environment.NewLine;

                var response = this.chatService.AddMessage(moodMessage, currentUserId, true);

                return this.Json(new
                {
                    myMessage = this.mapper.Map<MessageListViewModel>(message),
                    response = this.mapper.Map<MessageListViewModel>(response),
                    hasRecommendation = mood == "Sadness",
                    recommendationId = recommendation.Id
                });

            }
            
            var responseMessage = this.chatService.AddMessage(moodMessage, currentUserId, true);

            this.moodService.Add(currentUserId, mood);


            return this.Json(new
            {
                myMessage = this.mapper.Map<MessageListViewModel>(message),
                response = this.mapper.Map<MessageListViewModel>(responseMessage),
                hasRecommendation = mood == "Sadness"
            }); 
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            return this.Json(this.chatService.GetMessagesForUser(currentUserId, DateTime.Now).ProjectTo<MessageListViewModel>().ToList());
        }

        [HttpGet]
        public IActionResult GetMessagesForDay(int day, int month, int year)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            var messages = this.chatService.GetMessagesForUser(currentUserId, new DateTime(year, month, day)).ToList();

            return this.Json(messages.AsQueryable().ProjectTo<MessageListViewModel>().ToList());
        }

        [HttpPost]
        public IActionResult AddRating(int recommendationId, int rating)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            this.recommendationService.AddRating(currentUserId, recommendationId, rating);

            return this.Ok();
        }

        [HttpPost]
        public IActionResult AddRecommendationWithRating(string recommendation, int rating)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            this.recommendationService.AddRecommendationWithRating(currentUserId, recommendation, rating);

            return this.Ok();
        }
    }
}
