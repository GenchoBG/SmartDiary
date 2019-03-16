using System;
using System.Collections.Generic;
using System.Text;
using IntelliMood.Data.Models;

namespace IntelliMood.Services.Interfaces
{
    public interface IRecommender
    {
        string RecommendMusic(string userId,string mood);
    }
}
