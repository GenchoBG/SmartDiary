using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data;
using IntelliMood.Data.Models;
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
    }
}
