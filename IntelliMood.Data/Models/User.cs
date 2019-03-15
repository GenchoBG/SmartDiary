using Microsoft.AspNetCore.Identity;

namespace IntelliMood.Data.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public string DiaryName { get; set; }

        public bool Notifications { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }
    }
}
