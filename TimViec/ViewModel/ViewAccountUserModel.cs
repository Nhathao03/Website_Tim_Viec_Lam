using Microsoft.AspNetCore.Identity;

namespace TimViec.ViewModel
{
	public class ViewAccountUserModel
	{
		public string Id { get; set; }
		public string fullname { get; set; }
		public string email { get; set; }
		public DateTime Birth {  get; set; }
		public string phonenumber { get; set; }

	}
}
