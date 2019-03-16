
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliMood.Web.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Diary name")]
        public string DiaryName { get; set; }

        [Display(Name = "Push notifications")]
        public bool Notifications { get; set; }

        [Display(Name = "Primary color")]
        public string PrimaryColor { get; set; }

        [Display(Name = "Secondary color")]
        public string SecondaryColor { get; set; }

        public string StatusMessage { get; set; }
    }
}
