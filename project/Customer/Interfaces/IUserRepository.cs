using project.Customer.Dtos;
using project.Models.Customer;

namespace project.Customer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> CreateUser(UserModel user);
        Task<IEnumerable<UserDto.GetUserDto>> GetAllUsers();
        Task<UserModel> GetUserByUserName(string userName);
    }
}
