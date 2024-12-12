using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    [Table("Template")]
    public class Template
    {
        public int Id { get; set; }
        public int TypeID { get; set; }
        public string? Name { get; set; }
        public string ImagePath { get; set; }
        public string? HtmlTemplate { get; set; }
        public virtual TypeCV type { get; set; }
        public virtual ICollection<CV> cv { get; set; } = new List<CV>();

    }
}
