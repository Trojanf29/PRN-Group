using System.ComponentModel.DataAnnotations;

namespace Bloggie_Web.Models.ViewModels
{
    public class UserAddViewModal
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
