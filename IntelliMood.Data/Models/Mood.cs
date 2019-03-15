using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntelliMood.Data.Models
{
    public class Mood
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime DateTime { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
