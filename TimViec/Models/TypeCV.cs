using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    [Table("TypeCV")]
    public class TypeCV
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
    }
}
