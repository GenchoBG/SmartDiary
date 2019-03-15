using IntelliMood.Data.Models;
using IntelliMood.Web.Infrastructure.Mapper;
using System;
using AutoMapper;

namespace IntelliMood.Web.Models.ChatViewModels
{
    public class MessageListViewModel : ICustomMapping
    {
        public string Content { get; set; }

        public bool IsResponse { get; set; }

        public string Time { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Message, MessageListViewModel>()
                .ForMember(m => m.Time, opts => opts.MapFrom(m => m.Time.ToString("t")));
        }
    }
}
