using System.Threading.Tasks;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Data.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PixelArtWars.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<IntelliMoodDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var db = serviceScope.ServiceProvider.GetService<IntelliMoodDbContext>();

                Task
                    .Run(async () =>
                    {
                        var izlez = new Recommendation()
                        {
                            Content = "Izlez navun zagubenqk",
                            Type = RecommendationTypes.Activity
                        };

                        var kniga = new Recommendation()
                        {
                            Content = "4 books of amazon success",
                            Type = RecommendationTypes.Book
                        };

                        var film = new Recommendation()
                        {
                            Content = "American pie",
                            Type = RecommendationTypes.Movie
                        };

                        var pesen = new Recommendation()
                        {
                            Content = "bqla roza",
                            Type = RecommendationTypes.Music
                        };

                        var neshto = new Recommendation()
                        {
                            Content = "memes",
                            Type = RecommendationTypes.Other
                        };

                        db.Recommendations.Add(neshto);
                    })
                    .Wait();
            }

            return app;
        }
    }
}