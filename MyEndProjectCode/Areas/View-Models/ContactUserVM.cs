using System.ComponentModel.DataAnnotations;

namespace MyEndProjectCode.Areas.View_Models
{
    public class ContactUserVM
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

