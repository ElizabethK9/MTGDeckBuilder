using System.ComponentModel.DataAnnotations;

namespace MTGDeckBuilder.Models
{
    public class UserAccounts
    {
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Email must be 10 - 50 characters")]
        string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password must be 5 - 30 characters")]
        string Password {  get; set; }
    }
}
