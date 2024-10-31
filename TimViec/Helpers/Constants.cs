using System.ComponentModel;

namespace TimViec.Helpers
{
	public class Constants
	{
		public enum StatusJob
		{
			[Description("Đang chờ duyệt")]
			Inprogress = 1,
			[Description("Đã duyệt")]
			Completed = 2,
            [Description("Từ chối")]
            Refuse = 3
        }

		public enum ViewStatus
		{
			NoRead = 0,
			Read = 1
		}

		public enum favouriteJob
		{
			favourite = 1,
			nofavourite = 0
		}
	}
}
