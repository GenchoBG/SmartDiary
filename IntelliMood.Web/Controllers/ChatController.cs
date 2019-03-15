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

        public ChatController(IChatService chatService, UserManager<User> userManager, IMapper mapper, IEmotionGetter emotionGetter, IMoodService moodService)
        {
            this.chatService = chatService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.emotionGetter = emotionGetter;
            this.moodService = moodService;
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
            var moodMessage = $"You are feeling {mood}";
            var responseMessage = this.chatService.AddMessage(moodMessage, currentUserId, true);

            this.moodService.Add(currentUserId, mood);


            return this.Json(new
            {
                myMessage = this.mapper.Map<MessageListViewModel>(message),
                response = this.mapper.Map<MessageListViewModel>(responseMessage),
            }); //recommendation json response
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            return this.Json(this.chatService.GetMessagesForUser(currentUserId, DateTime.Now).ProjectTo<MessageListViewModel>().ToList());
        }
    }
}
