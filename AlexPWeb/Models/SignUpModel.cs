using System.ComponentModel.DataAnnotations;

namespace AlexPWeb.Models
{
    public class SignUpModel : LoginModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserDisplayName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserConfirmPassword { get; set; }
    }
}
