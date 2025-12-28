using project.Models.Customer;

namespace project.Customer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> GetUserByUserName(string userName);
    }
}
