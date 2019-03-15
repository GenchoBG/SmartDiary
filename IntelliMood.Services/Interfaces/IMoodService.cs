﻿using System.Linq;
using IntelliMood.Data.Models;

namespace IntelliMood.Services.Interfaces
{
    public interface IMoodService
    {
        void Add(string userId, string mood);
        IQueryable<Mood> GetAllMonthly(int month);
        IQueryable<Mood> GetAllYearly(int year);
    }
}
