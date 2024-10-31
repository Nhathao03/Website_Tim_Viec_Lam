using TimViec.Models;

namespace TimViec.ViewModel
{
    public class RegisterCompany
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Company> Company { get; set;}
    }
}
