using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IntelliMood.Web.Infrastructure.Filters
{
    public class ColorsInCookiesFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly UserManager<User> userManager;

        public async Task OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("primaryColor"))
            {
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await this.userManager.FindByNameAsync(context.HttpContext.User.Identity.Name);

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
                    context.HttpContext.Response.Cookies.Append("primaryColor", "#000000");
                    context.HttpContext.Response.Cookies.Append("secondaryColor", "#00ff7f");
                }
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
        }
    }
}
