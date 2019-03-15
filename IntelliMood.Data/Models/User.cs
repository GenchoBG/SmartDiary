using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IntelliMood.Web.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public string DiaryName { get; set; }
    }
}
