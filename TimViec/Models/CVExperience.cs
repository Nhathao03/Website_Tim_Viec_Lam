namespace TimViec.Models
{
    public class CVExperience
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? Position { get; set; } 
        public string? Company_name { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Experience_description { get; set; }

        // Navigation property
        public virtual CVDetails CV { get; set; } 
    }
}
