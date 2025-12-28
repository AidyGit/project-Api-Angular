using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project.Customer.Dtos
{
    public class UserDto
    {
        public class RegisterDto()
        {
            [MaxLength (50),Required]
            public string? Name { get; set; }
            [EmailAddress, Required]
            public string? Email { get; set; }
            [PasswordPropertyText, Required]
            public string? Password { get; set; }
            [MaxLength(10)]
            public string? Phone { get; set; }
            [MaxLength(50), Required]
            public string? UserName { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

        }
        public class LoginDto() 
        {
            [MaxLength(50), Required]
            public string? UserName { get; set; }
            [PasswordPropertyText, Required]
            public string? Password { get; set; }

        }
        public class GetUserDto()
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? UserName { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

        }
    }
}
