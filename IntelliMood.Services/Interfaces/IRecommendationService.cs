using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data.Models;
using IntelliMood.Data.Models.Recommendations;

namespace IntelliMood.Services.Interfaces
{
    public interface IRecommendationService
    {
        IQueryable<Recommendation> GetAll();
        IQueryable<UserRecommendation> GetAllUserRecommendations();
        void AddRating(string userId, int recommendationId, int rating);
        void AddRecommendationWithRating(string userId, string recommendation, int rating);
    }
}
