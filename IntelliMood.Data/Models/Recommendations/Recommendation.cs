using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using IntelliMood.Data.Models.Enums;

namespace IntelliMood.Data.Models
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public RecommendationTypes Type { get; set; }
    }
}
