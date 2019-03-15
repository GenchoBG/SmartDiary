using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data;
using IntelliMood.Data.Models;
using IntelliMood.Services.Interfaces;

namespace IntelliMood.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IntelliMoodDbContext db;

        public ChatService(IntelliMoodDbContext db)
        {
            this.db = db;
        }

        public void AddMessage(string content, string userId, bool isResponse)
        {
            this.db.Messages.Add(new Message()
            {
                Content = content,
                IsResponse = isResponse,
                UserId = userId,
                Time = DateTime.Today
            });

            this.db.SaveChanges();
        }

        public IQueryable<Message> GetMessagesForUser(string userId, DateTime date)
        {
            return this.db.Messages.Where(m => m.UserId == userId && m.Time.DayOfYear == date.DayOfYear).AsQueryable();
        }
    }
}
