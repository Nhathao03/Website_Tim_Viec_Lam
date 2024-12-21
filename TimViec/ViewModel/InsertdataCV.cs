namespace TimViec.ViewModel
{
    public class SectionAvatarInsert
    {
        public string? Url { get; set; }
    }
    public class SectionUserInsert
    {
        public string? Name { get; set; }

    }
    public class SectionTitleInsert
    {
        public string? Job_position { get; set; }

    }
    public class SectionCareerInsert
    {
        public string? Career_goal { get; set; }

    }
    public class SectionProfileInsert
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }

    }
    public class SectionEducationInsert
    {
        public string? Institution { get; set; }
        public string? Major { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
        public string? Grade { get; set; }
    }
    public class SectionSkillInsert
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
    public class SectionExperienceInsert
    {
        public string? Company { get; set; }
        public string? Position { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public List<string>? Responsibilities { get; set; }
    }
    public class SectionProjectInsert
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Project { get; set; }
        public string? Team_size { get; set; }
        public string? Position { get; set; }
        public List<string>? Description { get; set; }

    }
    public class SectionCertificationInsert
    {
        public string? Year { get; set; }
        public string? Name { get; set; }


    }
    public class SectionActivitiesInsert
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Activity { get; set; }
        public string? Position { get; set; }

        public List<string>? Description { get; set; }
    }
    public class SectionHonor_awardInsert
    {
        public string? Year { get; set; }
        public string? Name { get; set; }

    }
    public class SectionInterestInsert
    {
        public string? Description { get; set; }

    }
    public class SectionReferencesInsert
    {
        public string? Description { get; set; }

    }
    public class SectionInformationInsert
    {
        public string? Description { get; set; }

    }
}
