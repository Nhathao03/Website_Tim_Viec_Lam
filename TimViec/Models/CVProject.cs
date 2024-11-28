namespace TimViec.Models
{
    public class CVProject
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? Project_name { get; set; }
        public string? Customer { get; set; }
        public int? Team_size { get; set; }
        public string? Position { get; set; }
        public string? Technologies_description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation property
        public virtual CVDetails CV { get; set; }
    }
}
