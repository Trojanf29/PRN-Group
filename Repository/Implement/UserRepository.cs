using Bloggie_Web.Data;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie_Web.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(AuthDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddUser(IdentityUser user, string password, ICollection<string> roles)
        {
            var identityResult = await _userManager.CreateAsync(user, password);
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(user, roles);
                if (identityResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteUser(Guid userId)
        {
            var deletedUser = await _userManager.FindByIdAsync(userId.ToString());
            if (deletedUser != null)
            {
                await _userManager.DeleteAsync(deletedUser);
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUser()
        {
            var users = _userManager.Users.ToList();

            var superAdmins = await _userManager.GetUsersInRoleAsync("SuperAdmin");

            foreach (var superAdmin in superAdmins)
            {
                users.Remove(superAdmin);  
            }
            return users;
        }
    }
}
