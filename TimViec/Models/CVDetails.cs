using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class CVDetails
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        [Required]
        public string FullName { get; set; }
        public string? Image_user { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Website { get; set; }
        public string? Job_opening { get; set; }
        public string? Location { get; set; }
        public string? Interests { get; set; }
        public string? References { get; set; }
        public string? Additional_information { get; set; }
        public string? Carrer_objective { get; set; }
        public DateTime CreatedAt { get; set; }


        // Navigation properties
        public virtual ICollection<CVEducation> Educations { get; set; } = new List<CVEducation>();
        public virtual ICollection<CVExperience> Experiences { get; set; } = new List<CVExperience>();
        public virtual ICollection<CVSkill> Skills { get; set; } = new List<CVSkill>();
        public virtual ICollection<CVHonors_Awards> Honors_Awards { get; set; } = new List<CVHonors_Awards>();
        public virtual ICollection<CVProject> Projects { get; set; } = new List<CVProject>();
        public virtual ICollection<CVCertifications> Certifications { get; set; } = new List<CVCertifications>();
        public virtual ICollection<CVActivities> Activities { get; set; } = new List<CVActivities>();
    }
}
