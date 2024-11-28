namespace TimViec.Models
{
    public class CVHonors_Awards
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? Award_name { get; set; }
        public DateTime? Time { get; set; }
  

        // Navigation property
        public virtual CVDetails CV { get; set; }
    }
}
