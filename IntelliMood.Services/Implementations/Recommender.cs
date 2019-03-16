using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Services.DataStructures;
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

        public Recommendation Recommend(string userId)
        {
            var users = this.db.Users.ToList();
            var recommendations = this.db.Recommendations.ToList();

            var userIndexes = new Dictionary<string, int>();
            var recommendationIndexes = new Dictionary<int, int>();

            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];

                userIndexes[user.Id] = i;
            }

            for (int i = 0; i < recommendations.Count; i++)
            {
                var recommendation = recommendations[i];

                recommendationIndexes[recommendation.Id] = i;
            }


            var values = new double[users.Count][];
            for (int i = 0; i < users.Count; i++)
            {
                values[i] = new double[recommendations.Count];
            }

            var userRecommendations = this.db.UserRecommendations.ToList();
            foreach (var userRecommendation in userRecommendations)
            {
                values[userIndexes[userRecommendation.UserId]][recommendationIndexes[userRecommendation.RecommendationId]] = userRecommendation.Rating;
            }

            var populated = this.GetPopulatedEmptySpots(values.Select(arr => arr.ToList()).ToList());

            var goodIndexes = populated[userIndexes[userId]].Select((val, index) =>
            {
                return new
                {
                    Val = val,
                    Index = index,
                };
            }).Where(o => o.Val >= 3.5).Select(o => o.Index).ToList();

            var random = new Random();
            var winner = goodIndexes[random.Next(0, goodIndexes.Count)];

            return recommendations[winner];

            //return this.db.Recommendations.First();
        }

        private List<List<double>> GetPopulatedEmptySpots(List<List<double>> data)
        {
            var predictor = new SVDPP();

            // Loading matrix of ratings from file
            predictor.LoadItemsFromMatrix(data, predictor.MatrixUI);

            predictor.Initialize(); // Initializing the ratings prediction model

            predictor.Learn();      // Training the ratings prediction model

            predictor.Predict();    // Predicting ratings for the unrated items

            return predictor.MatrixUI;
        }
    }
}
