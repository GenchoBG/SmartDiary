using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntelliMood.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsResponse { get; set; }

        public DateTime Time { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }
    }
}
