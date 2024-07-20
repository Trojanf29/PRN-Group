using Microsoft.AspNetCore.Identity;

namespace Bloggie_Web.Repository.Contract
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUser();
        Task<bool> AddUser(IdentityUser user, string password, ICollection<string> roles);
        Task DeleteUser(Guid userId);
    }
}
