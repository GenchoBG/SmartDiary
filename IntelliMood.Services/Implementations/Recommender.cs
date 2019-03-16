using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Services.Interfaces;

namespace IntelliMood.Services.Implementations
{
    public class Recommender : IRecommender
    {
        private readonly IntelliMoodDbContext db;

        public Recommender(IntelliMoodDbContext db)
        {
            this.db = db;
        }

        public Recommendation RecommendMusic(string userId, string mood)
        {
            return this.db.Recommendations.First();
        }
    }
}
