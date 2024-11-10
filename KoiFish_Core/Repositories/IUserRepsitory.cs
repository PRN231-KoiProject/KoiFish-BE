using KoiFish_Core.Domain.Identity;
using KoiFish_Core.SeedWorks;

namespace KoiFish_Core.Repositories
{
    public interface IUserRepsitory : IRepositoryBase<AppUser, Guid>
    {
         Task<IEnumerable<AppUser>> GetUsersAsync(int page, int limit, string search, bool? status);
        Task<AppUser> GetUserById(Guid id);
        public Task RemoveUserFromRoleAsync(Guid userId, string[] roles);
        Task<int> GetTotalUserCountAsync(string search);
        Task<AppUser>GetUserByEmail(string email);
                Task<AppUser> GetUserByPhoneAsync(string phoneNumber);

    }
}