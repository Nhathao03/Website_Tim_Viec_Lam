﻿using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class Type_work
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; }
    }
}
