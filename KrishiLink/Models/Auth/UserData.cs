using System.ComponentModel.DataAnnotations;

namespace KrishiLink.Models.Auth
{
    public class UserData
    {
        [Key]
        public int UserId { get; set; }
        public string Mobile { get; set; }
        public string Business_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }

        public string? Access_Token { get; set; }

        public DateTime Created_At { get; set; } = DateTime.UtcNow;
    }
}
