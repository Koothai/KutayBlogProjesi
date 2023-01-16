using System.ComponentModel.DataAnnotations;
using KutayBlogProjesi.Models.Entities.Concrete;

namespace KutayBlogProjesi.Models.DTOs
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "Fill the Username section")]
        [Display(Name = "User Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fill the Password section")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Fill the Email section")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid Email address.")]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }

        public UserUpdateDTO(){}
        public UserUpdateDTO(User user)
        {
            Username = user.Username;
            Email = user.Email;
        }
    }
}
