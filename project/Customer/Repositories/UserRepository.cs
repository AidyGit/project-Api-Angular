using project.Customer.Interfaces;
using project.Data;
using project.Models.Customer;

namespace project.Customer.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            _context.UserModel.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }
        public async Task<UserModel?> GetUserByUserName(string userName)
        {
            UserModel? user = await _context.UserModel.FindAsync(userName);
            return user;
        }
    }
}
