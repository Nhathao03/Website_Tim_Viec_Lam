using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class Rank
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string rank { get; set; }

    }
}
