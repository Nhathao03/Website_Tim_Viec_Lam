using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name_city { get; set; }
    }
}
