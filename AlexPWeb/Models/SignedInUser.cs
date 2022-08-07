using System.ComponentModel.DataAnnotations;

namespace AlexPWeb.Models
{
    public class SignedInUser
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
