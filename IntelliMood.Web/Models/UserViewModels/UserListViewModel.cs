using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
using IntelliMood.Web.Infrastructure.Mapper;

namespace IntelliMood.Web.Models.UserViewModels
{
    public class UserListViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
