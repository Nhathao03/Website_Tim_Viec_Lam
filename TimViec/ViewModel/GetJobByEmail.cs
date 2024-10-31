namespace TimViec.ViewModel
{
    public class GetJobByEmail
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string R1_Language { get; set;}
        public string R2_Language { get; set;}
        public string R3_Language { get; set;}

        public int? Salary { get; set;}
        public int? Rank { get; set;}
        public string Rankname { get; set; }
        public int? Type { get; set;}
        public string Typename { get; set; }
        public int? Skill { get; set;}
        public string Skillname { get; set; }
    }
}
