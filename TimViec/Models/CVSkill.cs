namespace TimViec.Models
{
    public class CVSkill
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string? Skill_name { get; set; }
        public string? Skill_description { get; set; }

        // Navigation property
        public virtual CVDetails CV { get; set; }
    }
}
