using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Web.Models.UserViewModels;

namespace IntelliMood.Web.Models.RecommendationViewModels
{
    public class RecommendationTableViewModel
    {
        public IEnumerable<RecommendationListViewModel> Recommendations { get; set; }

        public IEnumerable<UserListViewModel> Users { get; set; }
    }
}
