using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static project.Customer.Dtos.UserDto;

namespace project.Customer.Dtos
{
    public class UserDto
    {
        public class RegisterDto
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

        }
        public class LoginDto
        {
            [MaxLength(50), Required]
            public string? UserName { get; set; }
            [PasswordPropertyText, Required]
            public string? Password { get; set; }

        }
        public class GetUserDto
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? UserName { get; set; }
            public string Role { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
        }
    }
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public GetUserDto User { get; set; } = null!;
    }

}
