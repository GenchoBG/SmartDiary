using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
using IntelliMood.Web.Infrastructure.Mapper;

namespace IntelliMood.Web.Models.ChatViewModels
{
    public class ChatIndexViewModel : IMapFrom<User>
    {
        public string DiaryName { get; set; }
    }
}
