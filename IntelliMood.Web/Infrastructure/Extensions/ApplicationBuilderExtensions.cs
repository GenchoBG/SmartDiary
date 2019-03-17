using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Data.Models.Enums;
using IntelliMood.Data.Models.Recommendations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntelliMood.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
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
                        if (!db.Recommendations.Any())
                        {
                            var recommendations = new List<Recommendation>();

                            recommendations.Add(new Recommendation()
                            {
                                Content = "Take a walk outside",
                                Type = RecommendationTypes.Activity
                            });

                            recommendations.Add(new Recommendation()
                            {
                                Content = "Take a cold shower",
                                Type = RecommendationTypes.Activity
                            });

                            recommendations.Add(new Recommendation()
                            {
                                Content = "WarCross",
                                Type = RecommendationTypes.Book
                            });
                            recommendations.Add(new Recommendation()
                            {
                                Content = "Mr. Perfect",
                                Type = RecommendationTypes.Book
                            });
                            recommendations.Add(new Recommendation()
                            {
                                Content = "50 shades of grey",
                                Type = RecommendationTypes.Book
                            });

                            recommendations.Add(new Recommendation()
                            {
                                Content = "American pie",
                                Type = RecommendationTypes.Movie
                            });
                            recommendations.Add(new Recommendation()
                            {
                                Content = "Despicable Me",
                                Type = RecommendationTypes.Movie
                            });

                            recommendations.Add(new Recommendation()
                            {
                                Content = "Bach",
                                Type = RecommendationTypes.Music
                            });
                            recommendations.Add(new Recommendation()
                            {
                                Content = "Vivaldi",
                                Type = RecommendationTypes.Music
                            });
                            recommendations.Add(new Recommendation()
                            {
                                Content = "Vagner",
                                Type = RecommendationTypes.Music
                            });
                            recommendations.Add(new Recommendation()
                            {
                                Content = "Mozzart",
                                Type = RecommendationTypes.Music
                            });

                            recommendations.Add(new Recommendation()
                            {
                                Content = "Memes",
                                Type = RecommendationTypes.Other
                            });
                            
                            db.Recommendations.AddRange(recommendations);

                            await db.SaveChangesAsync();

                            var stamat = new User
                            {
                                UserName = "Stamat",
                                Email = "stamatpeshov@gmail.com",
                                DiaryName = "Artie",
                                PrimaryColor = "#000000",
                                SecondaryColor = "#00ff7f"
                            };

                            var neti = new User
                            {
                                UserName = "Neti",
                                Email = "anetastsvetkova@gmail.com",
                                DiaryName = "Artie",
                                PrimaryColor = "#000000",
                                SecondaryColor = "#00ff7f"
                            };

                            var kalin = new User
                            {
                                UserName = "Kalin",
                                Email = "kdk@gmail.com",
                                DiaryName = "Artie",
                                PrimaryColor = "#000000",
                                SecondaryColor = "#00ff7f"
                            };

                            var kosta = new User
                            {
                                UserName = "Kosta",
                                Email = "kkk@gmail.com",
                                DiaryName = "Haralampi",
                                PrimaryColor = "#000000",
                                SecondaryColor = "#00ff7f"
                            };

                            await userManager.CreateAsync(stamat, "test12");
                            await userManager.CreateAsync(neti, "test12");
                            await userManager.CreateAsync(kalin, "test12");
                            await userManager.CreateAsync(kosta, "test12");

                            var random = new Random();

                            var lovedByEverybody = new Recommendation()
                            {
                                Content = "Playing with animals",
                                Type = RecommendationTypes.Other
                            };

                            db.Recommendations.Add(lovedByEverybody);
                            db.SaveChanges();

                            foreach (var user in db.Users.ToList())
                            {
                                db.UserRecommendations.Add(new UserRecommendation()
                                {
                                    Mood = "Sad",
                                    Rating = 5,
                                    UserId = user.Id,
                                    RecommendationId = lovedByEverybody.Id
                                });
                            }

                            foreach (var recommendation in db.Recommendations.ToList())
                            {
                                foreach (var user in db.Users.ToList())
                                {
                                    var shouldAdd = random.Next(0, 10);
                                    if (shouldAdd < 7)
                                    {
                                        db.UserRecommendations.Add(new UserRecommendation()
                                        {
                                            UserId = user.Id,
                                            RecommendationId = recommendation.Id,
                                            Mood = "Bad",
                                            Rating = random.Next(5) + 1
                                        });
                                    }
                                }
                            }

                            await db.SaveChangesAsync();
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}