using TimViec.Models;

namespace TimViec.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Company> Companies { get; set; }
		public IEnumerable<Skill> skills { get; set; }
		public IEnumerable<Rank> ranks { get; set; }
		public IEnumerable<City> Cities { get; set; }
		public IEnumerable<Type_work> Type_Works { get; set; }
	}
}
