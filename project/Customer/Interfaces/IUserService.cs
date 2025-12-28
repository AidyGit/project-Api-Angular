using project.Customer.Dtos;

namespace project.Customer.Interfaces
{
    public interface IUserService
    {
        Task<UserDto.RegisterDto> CreateUser(UserDto.RegisterDto userDto);
        Task<UserDto.LoginDto> LoginUser(UserDto.LoginDto userDto);

        Task<IEnumerable<UserDto.GetUserDto>> GetAllUsers();


    }
}
