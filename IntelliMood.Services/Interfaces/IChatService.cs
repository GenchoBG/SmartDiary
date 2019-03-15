using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliMood.Data.Models;

namespace IntelliMood.Services.Interfaces
{
    public interface IChatService
    {
        void AddMessage(string content, string userId, bool isResponse);
        IQueryable<Message> GetMessagesForUser(string userId, DateTime date);
    }
}
