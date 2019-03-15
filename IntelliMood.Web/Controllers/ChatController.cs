using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
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

        public ChatController(IChatService chatService, UserManager<User> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateMessage(MessageCreateViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            this.chatService.AddMessage(data.Message, this.userManager.GetUserId(this.User), false);

            return this.Json("yess"); //recommendation json response
        }
    }
}
