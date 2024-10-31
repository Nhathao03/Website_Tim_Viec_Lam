using Microsoft.AspNetCore.Identity;

namespace TimViec.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname { get; set; }

        public string? imgCV { get; set; }

        public string? avatar { get; set; }
        public DateTime Birth { get; set; }

   
    }
}
