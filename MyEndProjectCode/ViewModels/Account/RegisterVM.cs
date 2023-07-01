using System.ComponentModel.DataAnnotations;

namespace MyEndProjectCode.ViewModels.Account
{
    public class RegisterVM
    {

        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
