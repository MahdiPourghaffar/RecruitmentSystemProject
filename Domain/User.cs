using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace Domain
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public IEnumerable<Announcement> Announcements { get; set; }
    }
}
