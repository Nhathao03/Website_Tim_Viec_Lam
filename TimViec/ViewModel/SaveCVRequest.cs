namespace TimViec.ViewModel
{
    public class SaveCVRequest
    {
        public int cvid {  get; set; }
        public string NameCV { get; set; }
        public SectionAvatar Avatar { get; set; }
        public SectionUser Username { get; set; }
        public SectionTitle Title { get; set; }
        public SectionCareer Career { get; set; }
        public List<SectionSkill> Skills { get; set; }
        public SectionProfile Profile { get; set; }
        public List<SectionEducation> Educations { get; set; }
        public List<SectionExperience> Experiences { get; set; }
        public List<SectionProject> Projects { get; set; }
        public List<SectionCertification> Certifications { get; set; }
        public List<SectionActivities> Activities { get; set; }
        public List<SectionHonor_award> Honor_Awards { get; set; }
        public List<SectionInterest> Interests { get; set; }
        public List<SectionReferences> References { get; set; }
        public SectionInformation information { get; set; }
    }

    public class SectionAvatar
    {
        public string? Url { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionUser
    {
        public string? Name { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionTitle
    {
        public string? Job_position { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionCareer
    {
        public string? Career_goal { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionProfile
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionEducation
    {
        public string? Institution { get; set; }
        public string? Major { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
        public string? Grade { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionSkill
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionExperience
    {
        public string? Company { get; set; }
        public string? Position { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int SectionID { get; set; }
        public List<string>? Responsibilities { get; set; }
    }
    public class SectionProject
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Project { get; set; }
        public string? Team_size { get; set; }
        public string? Position { get; set; }
        public List<string>? Description { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionCertification
    {
        public string? Year { get; set; }
        public string? Name { get; set; }
        public int SectionID { get; set; }

    }
    public class SectionActivities
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Activity { get; set; }
        public string? Position { get; set; }
        public int SectionID { get; set; }
        public List<string>? Description { get; set; }
    }
    public class SectionHonor_award
    {
        public string? Year { get; set; }
        public string? Name { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionInterest
    {
        public string? Description { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionReferences
    {
        public string? Description { get; set; }
        public int SectionID { get; set; }
    }
    public class SectionInformation
    {
        public string? Description { get; set; }
        public int SectionID { get; set; }
    }
}
