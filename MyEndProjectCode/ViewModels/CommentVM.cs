using System.ComponentModel.DataAnnotations;

namespace MyEndProjectCode.ViewModels
{
    public class CommentVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
