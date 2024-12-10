using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    [Table("CVs")]
    public class CV
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }       
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDefault { get; set; }
		public virtual Template template { get; set; }
		public int templateId { get; set; }
		public virtual ICollection<Sections> section { get; set; } = new List<Sections>();

    }
}
