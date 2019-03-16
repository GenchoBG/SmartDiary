using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntelliMood.Data.Models.Recommendations
{
    public class UserRecommendation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int RecommendationId { get; set; }

        [Required]
        public string Mood { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
