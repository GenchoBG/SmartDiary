using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Services.Interfaces;

namespace IntelliMood.Services.Implementations
{
    public class MoodService : IMoodService
    {
        private readonly IntelliMoodDbContext db;

        public MoodService(IntelliMoodDbContext db)
        {
            this.db = db;
        }

        public void Add(string userId, string mood)
        {
            this.db.Moods.Add(new Mood()
            {
                DateTime = DateTime.Now,
                UserId = userId,
                Type = mood
            });

            this.db.SaveChanges();
        }

        public IQueryable<Mood> GetAllDaily(int day, int month, int year)
        {
            return this.db.Moods.Where(m => m.DateTime.Day == day && m.DateTime.Month == month && m.DateTime.Year == year).AsQueryable();
        }

        public IQueryable<Mood> GetAllMonthly(int month, int year)
        {
            return this.db.Moods.Where(m => m.DateTime.Month == month && m.DateTime.Year == year).AsQueryable();
        }

        public IQueryable<Mood> GetAllYearly(int year)
        {
            return this.db.Moods.Where(m => m.DateTime.Year == year).AsQueryable();
        }
    }
}
