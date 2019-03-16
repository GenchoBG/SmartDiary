using System;
using System.Collections.Generic;
using System.Text;
using IntelliMood.Services.Interfaces;

namespace IntelliMood.Services.Implementations
{
    public class Recommender : IRecommender
    {
        public string RecommendMusic(string userId, string mood)
        {
            return "Manowar - Kings of Metal";
        }
    }
}
