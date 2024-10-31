
using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class favourite_job
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? R1_Language { get; set; }
        public string? R2_Language { get; set;}
        public string? R3_Language { get; set;}
        public string image {get; set; }
        [Required]
        public int favourite {  get; set;}
        [Required]
        public int IDJob { get; set;}
        public string email { get; set; }
    }
}
