using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Skills { get; set; }
    }
}
