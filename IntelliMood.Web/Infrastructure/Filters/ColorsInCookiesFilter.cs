using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IntelliMood.Web.Infrastructure.Filters
{
    public class ColorsInCookiesFilter :  IAsyncActionFilter
    {
        private readonly UserManager<User> userManager;

        public ColorsInCookiesFilter(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("primaryColor"))
            {
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await this.userManager.FindByNameAsync(context.HttpContext.User.Identity.Name);
                    var controller = context.Controller as Controller;
                    if (user.PrimaryColor == null)
                    {
                        context.HttpContext.Response.Cookies.Append("primaryColor", "#000000");
                        context.HttpContext.Response.Cookies.Append("secondaryColor", "#00ff7f");

                    }
                    else
                    {
                        context.HttpContext.Response.Cookies.Append("primaryColor", user.PrimaryColor);
                        context.HttpContext.Response.Cookies.Append("secondaryColor", user.SecondaryColor);
                    }
                }
                else
                {
                    var controller = context.Controller as Controller;

                    context.HttpContext.Response.Cookies.Append("primaryColor", "#000000");
                    context.HttpContext.Response.Cookies.Append("secondaryColor", "#00ff7f");
                }
            }

            await next.Invoke();
        }
    }
}
