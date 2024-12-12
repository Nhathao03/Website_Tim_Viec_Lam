using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    [Table("TypeSection")]
    public class TypeSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sections> Sections { get; set; } = new List<Sections>();
    }
}
