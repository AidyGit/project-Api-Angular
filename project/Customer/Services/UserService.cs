using project.Customer.Dtos;
using project.Customer.Interfaces;
using project.Models.Customer;

namespace project.Customer.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto.RegisterDto> CreateUser(UserDto.RegisterDto register)
        {
            var user = new UserModel
            {
                Name = register.Name,
                Email = register.Email,
                Password = register.Password,
                Phone = register.Phone,
                UserName = register.UserName
            };
            var createdUser = await _userRepository.CreateUser(user);
            return MapToUserDto(createdUser);

        }


        public async Task<UserDto.LoginDto> LoginUser(UserDto.LoginDto login)
        {
            if (login.UserName == null)
            {
                throw new ArgumentNullException(nameof(login.UserName));
            }

            var user = await _userRepository.GetUserByUserName(login.UserName);

            if (user == null || user.Password != login.Password)
            {
                throw new ArgumentException("Invalid username or password");
            }
            return new UserDto.LoginDto
            {
                UserName = user.UserName,
                Password = user.Password
            };
            //להוסיף בשביל האבטחה טוקן JWT
            //return new UserDto.LoginDto
            //{
            //    Token = token,
            //    TokenType = "Bearer",
            //    ExpiresIn = expiryMinutes * 60, // Convert to seconds
            //    User = MapToResponseDto(user)
            //};
        }
        private static UserDto.RegisterDto MapToUserDto(UserModel user)
        {
            return new UserDto.RegisterDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt
            };
        }

    }
}
