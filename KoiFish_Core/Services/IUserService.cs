using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Identity;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;

namespace KoiFish_Core.Services
{
    public interface IUserService
    {  
        
        Task<PageResult<UserResponse>> GetUsersAsync(int page, int limit, string search, bool? status);
        Task<UserResponse> UpdateUser(Guid id, CreateUserRequest user);
        Task<bool> RemoveUser(Guid id);
        Task<UserResponse> AddUser(CreateUserRequest User);

        Task<UserResponse> GetUserById(Guid id);
                Task<bool> ChangeStatusbyId(Guid id);
        Task<bool> CheckPhoneNumerAsync(string phoneNumber);

        Task<bool> ChangePasswordbyId(Guid id, string currentPassword, string newPassword);


    }
}
