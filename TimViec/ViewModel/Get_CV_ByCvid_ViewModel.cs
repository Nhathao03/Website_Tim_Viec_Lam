namespace TimViec.ViewModel
{
    public class Get_CV_ByCvid_ViewModel
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public string? Title { get; set; }
        public string? ContentJson { get; set; }
        public string? StyleJson { get; set; }
        public string? ImagePath { get; set; }
        public string? TypeName { get; set; }
    }

    public class Get_CV_ByCvid_ViewModelResult
    {
        public int Id { get; set; }
        public dynamic Content { get; set; }
        public dynamic? Style { get; set; }
        public string? TypeName { get; set; }
        public SectionAvatar Avatar { get; set; }
        public SectionUser Username { get; set; }
        public SectionTitle Title { get; set; }
        public SectionCareer Career { get; set; }
        public List<SectionSkill> Skills { get; set; }
        public List<SectionProfile> Profile { get; set; }
    }

    public class SectionAvatar{
        public string Url { get; set; }
    }
    public class SectionUser { 
        public string Name { get; set; }
    }
    public class SectionTitle{
        public string Job_position { get; set; }
    }
    public class SectionCareer{
        public string Career_goal { get; set; }
    }

    public class SectionProfile
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
    }
    public class SectionEducation
    {
        public string Institution { get; set; }
        public string Major { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Grade { get; set; }
    }
    public class SectionSkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SectionExperience
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
    }
    public class SectionCertification
    {
        public string Year { get; set; }
        public string Name { get; set; }

    }
    public class SectionActivities
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Activity { get; set; }
        public string Position { get; set; }

    }
}
