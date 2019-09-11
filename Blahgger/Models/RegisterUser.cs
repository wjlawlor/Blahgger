using System.ComponentModel.DataAnnotations;

namespace Blahgger.Models
{
    public class RegisterUser
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName{ get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
        public string HashedPassword { get; set; }
    }
}
