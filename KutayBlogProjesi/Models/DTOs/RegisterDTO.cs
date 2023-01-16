using System.ComponentModel.DataAnnotations;

namespace KutayBlogProjesi.Models.DTOs
{
    public class RegisterDTO
    {
        //[Required(ErrorMessage = "Must to type into user name")]
       
        //public string Username { get; set; }

        [Required(ErrorMessage = "Fill the Password section")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Fill the Email section")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Please enter a valid Email address.")]
        [Display(Name ="E-Mail Address")]
        public string Email { get; set; }
    }
}
