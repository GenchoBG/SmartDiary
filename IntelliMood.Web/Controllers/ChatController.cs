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
		private readonly IMapper mapper;		private readonly IEmotionGetter emotionGetter;
        public ChatController(IChatService chatService, UserManager<User> userManager, IEmotionGetter emotionGetter, IMapper mapper)        {
            this.chatService = chatService;
            this.userManager = userManager;
			this.emotionGetter = emotionGetter;			this.mapper = mapper;        }

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

            return this.Json(new
            {
                myMessage = message,
                response = this.chatService.AddMessage(mood, currentUserId, true)
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
