using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    [Table("Section")]
    public class Sections
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name {  get; set; }
        public string? ContentJson { get; set; }
        public string? StyleJson { get; set; }
        public int cvId { get; set; }
        public virtual CV cv { get; set; }
        public int TypeSectionId { get; set; }
        public virtual TypeSection TypeSection { get; set; }
    }
}
