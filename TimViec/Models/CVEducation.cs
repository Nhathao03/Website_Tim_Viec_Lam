namespace TimViec.Models
{
    public class CVEducation
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? School_name { get; set; }
        public string? Description { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? GPA { get; set; }

        // Navigation property
        public virtual CVDetails CV { get; set; }
    }
}
