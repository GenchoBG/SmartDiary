using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Data.Models.Enums;
using IntelliMood.Data.Models.Recommendations;
using IntelliMood.Services.Interfaces;

namespace IntelliMood.Services.Implementations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IntelliMoodDbContext db;

        public RecommendationService(IntelliMoodDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Recommendation> GetAll()
        {
            return this.db.Recommendations.AsQueryable();
        }

        public IQueryable<UserRecommendation> GetAllUserRecommendations()
        {
            return this.db.UserRecommendations.AsQueryable();
        }

        public void AddRating(string userId, int recommendationId, int rating)
        {
            this.db.UserRecommendations.Add(new UserRecommendation()
            {
                RecommendationId = recommendationId,
                UserId = userId,
                Rating = rating,
                Mood = "Sadness"
            });

            this.db.SaveChanges();
        }

        public void AddRecommendationWithRating(string userId, string recommendation, int rating)
        {
            var rec = this.db.Recommendations.FirstOrDefault(r => r.Content == recommendation);
            if (rec == null)
            {
                rec = new Recommendation()
                {
                    Content = recommendation,
                    Type = RecommendationTypes.Other
                };

                this.db.Recommendations.Add(rec);
            }

            this.db.UserRecommendations.Add(new UserRecommendation()
            {
                RecommendationId = rec.Id,
                UserId = userId,
                Rating = rating,
                Mood = "Sadness"
            });

            this.db.SaveChanges();
        }
    }
}
