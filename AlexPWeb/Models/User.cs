using System.ComponentModel.DataAnnotations;

namespace AlexPWeb.Models
{
    public class User : SignedInUser
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
    }
}
