namespace TimViec.Models
{
    public class CVCertifications
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? Certification_name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation property
        public virtual CVDetails CV { get; set; }
    }
}
