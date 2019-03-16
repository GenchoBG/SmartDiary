using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliMood.Data.Models;
using IntelliMood.Web.Infrastructure.Mapper;

namespace IntelliMood.Web.Models.RecommendationViewModels
{
    public class RecommendationListViewModel : IMapFrom<Recommendation>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
