namespace TimViec.ViewModel
{
    public class Get_CV_ByCvid_ViewModel
	{
		public int Id { get; set; }
		public string? UserID { get; set; }
		public string? Title { get; set; }
		public string? ContentJson { get; set; }
		public string? StyleJson {  get; set; }
		public string? ImagePath { get; set; }
		public string? TypeName { get; set; }
	}

    public class Get_CV_ByCvid_ViewModelResult
    {
        public int Id { get; set; }
        public dynamic Content { get; set; } 
        public dynamic? Style { get; set; }
        public string? TypeName { get; set; }
    }
}
