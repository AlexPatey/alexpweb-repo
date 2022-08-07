using System.ComponentModel.DataAnnotations;

namespace AlexPWeb.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}
