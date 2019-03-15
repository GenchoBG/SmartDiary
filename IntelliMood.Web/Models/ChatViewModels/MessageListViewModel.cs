using IntelliMood.Data.Models;
using IntelliMood.Web.Infrastructure.Mapper;
using System;

namespace IntelliMood.Web.Models.ChatViewModels
{
    public class MessageListViewModel : IMapFrom<Message>
    {
        public string Content { get; set; }

        public bool IsResponse { get; set; }

        public DateTime Time { get; set; }
    }
}
