using System.Linq;
using IntelliMood.Data.Models;

namespace IntelliMood.Services.Interfaces
{
    public interface IMoodService
    {
        void Add(string userId, string mood);
        IQueryable<Mood> GetAllDaily(int day, int month, int year);
        IQueryable<Mood> GetAllMonthly(int month, int year);
        IQueryable<Mood> GetAllYearly(int year);
    }
}
