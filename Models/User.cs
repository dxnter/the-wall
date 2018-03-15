using System.ComponentModel.DataAnnotations;

namespace the_wall.Models {
    public class Register {
        [Required]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters!")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First name can contain letters only")]
        public string firstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters!")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last name can contain letters only")]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        public string confirmPassword { get; set; }
    }

    public class Login {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}