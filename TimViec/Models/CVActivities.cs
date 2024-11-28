namespace TimViec.Models
{
    public class CVActivities
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? Organization_name { get; set; }
        public string? Position { get; set; }
        public string? Activity_description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation property
        public virtual CVDetails CV { get; set; }
    }
}
