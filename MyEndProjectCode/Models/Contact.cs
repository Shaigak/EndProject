﻿using System.ComponentModel.DataAnnotations;

namespace MyEndProjectCode.Models
{
    public class Contact : BaseEntity
    {



      
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; }
    }
}
