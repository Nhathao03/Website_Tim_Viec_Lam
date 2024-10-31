using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    public class StatusJob
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        public int Status { get; set; }
        [Required]
        public string Fullname { get; set; }
        public string? Note { get; set; }
        [Required]
        public string imgCV { get; set; }

		[ForeignKey("Job")]
        public int JobID { get; set; }
        public Job? Job { get; set; }

        public int Read { get; set; }  
	}
}
