using Microsoft.EntityFrameworkCore;
using project.Customer.Dtos;
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
        public async Task<UserModel> GetUserByUserName(string userName)
        {
            //NULL מביא את הראשון שהוא מוצא או      
            var user = await _context.UserModel.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
        }

        public async Task<IEnumerable<UserDto.GetUserDto>> GetAllUsers()
        {
            return await _context.UserModel.Select(u => new UserDto.GetUserDto
            {
                Name = u.Name,
                Email = u.Email,
                UserName = u.UserName,
                Phone = u.Phone
            })
            .ToListAsync();
        }
    }
}
