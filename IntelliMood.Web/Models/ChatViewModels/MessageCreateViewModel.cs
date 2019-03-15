using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliMood.Web.Models.ChatViewModels
{
    public class MessageCreateViewModel
    {
        [Required]
        public string Message { get; set; }
    }
}
