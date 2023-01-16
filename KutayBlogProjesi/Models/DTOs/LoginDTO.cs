using System.ComponentModel.DataAnnotations;

namespace KutayBlogProjesi.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Fill the Email section")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid Email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fill the Password section")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
