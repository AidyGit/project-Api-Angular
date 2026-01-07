using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using project.Customer.Dtos;
using project.Customer.Interfaces;
using project.Models.Customer;

namespace project.Customer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository, IConfiguration configuration, ILogger<UserService> logger, TokenService tokenService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
            _tokenService = tokenService;

        }

        public async Task<UserDto.RegisterDto> CreateUser(UserDto.RegisterDto register)
        {
            var user = new UserModel
            {
                Name = register.Name,
                Email = register.Email,
                Password = HashPassword(register.Password),
                Phone = register.Phone,
                UserName = register.UserName
            };
            var createdUser = await _userRepository.CreateUser(user);
            return MapToUserDto(createdUser);

        }


        public async Task<LoginResponseDto> LoginUser(UserDto.LoginDto login) // שינוי סוג ההחזרה
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                throw new ArgumentException("UserName and Password are required");
            }

            // 1. קודם כל שליפת המשתמש
            var user = await _userRepository.GetUserByUserName(login.UserName);

            // 2. בדיקה אם המשתמש קיים ואם הסיסמה תואמת (לפני הכל!)
            var hashedPassword = HashPassword(login.Password);

            if (user == null || user.Password != hashedPassword)
            {
                throw new ArgumentException("Invalid username or password");
            }

            // 3. רק אם הכל תקין - מייצרים טוקן
            var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name, user.Role, user.UserName);
           
            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes", 60);

            return new LoginResponseDto
            {
                Token = token,
                TokenType = "Bearer",
                ExpiresIn = expiryMinutes * 60,
                User = MapToUserDto(user)
            };
        }

        public async Task<IEnumerable<UserDto.GetUserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        private static UserDto.RegisterDto MapToUserDto(UserModel user)
        {
            return new UserDto.RegisterDto
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt
            };
        }


        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
