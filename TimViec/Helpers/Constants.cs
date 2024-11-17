using System.ComponentModel;

namespace TimViec.Helpers
{
	public class Constants
	{
		public enum StatusJob
		{
			[Description("Waiting")]
			Inprogress = 1,
			[Description("Accepted")]
			Completed = 2,
            [Description("Decline")]
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
