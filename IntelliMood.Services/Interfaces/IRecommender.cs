using System;
using System.Collections.Generic;
using System.Text;
using IntelliMood.Data.Models;

namespace IntelliMood.Services.Interfaces
{
    public interface IRecommender
    {
        Recommendation Recommend(string userId);
        List<List<double>> GetUnpopulatedArray();
        List<List<double>> GetPopulatedArray();
    }
}
