using project.Customer.Dtos;
using project.Models.Customer;

namespace project.Customer.Interfaces
{
    public interface IUserService
    {
        // מחזיר את פרטי המשתמש שנוצר
        Task<UserDto.GetUserDto> CreateUser(UserDto.RegisterDto userDto);

        Task<IEnumerable<UserDto.GetUserDto>> GetAllUsers();

        // מחזיר טוקן ופרטי כניסה
        Task<LoginResponseDto> LoginUser(UserDto.LoginDto userDto);
    }
}
