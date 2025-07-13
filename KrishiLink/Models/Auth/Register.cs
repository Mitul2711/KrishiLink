using System.ComponentModel.DataAnnotations;

namespace KrishiLink.Models.Auth
{
    public class Register
    {
        [Required]
        public string Mobile { get; set; }
        public string Business_Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string ZipCode { get; set; }
    }
}
