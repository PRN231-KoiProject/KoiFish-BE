using AutoMapper;
using KoiFish_Core.Domain.Identity;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Core.Services;
using KoiFish_Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepsitory _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

  public UserService(UserManager<AppUser> userManager, IUserRepsitory repositoryManager, IMapper mapper)
        {
            _userManager = userManager;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public  async Task<UserResponse> AddUser(CreateUserRequest User)
        {
     var newUserRequest = new AppUser()
            {
                Id = Guid.NewGuid(),
                FullName = User.FullName,
                UserName = User.Email,
                Status = false,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                LockoutEnabled = false,
                EmailConfirmed = true,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };

            var checkemail = await _userManager.FindByEmailAsync(User.Email);
            if (checkemail != null)
            {
                throw new Exception($"Trùng Email");

            }
            var result = await _userManager.CreateAsync(newUserRequest, "123As@");
            newUserRequest = await _userManager.FindByEmailAsync(User.Email);


            if (!result.Succeeded)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Tạo người dùng thất bại: {errorMessages}");
         //   return null;
            }
            await _userManager.AddToRoleAsync(newUserRequest, User.Role);
            var UserResponse = _mapper.Map<UserResponse>(newUserRequest);
            return UserResponse;        }

        public async  Task<bool> ChangePasswordbyId(Guid id, string currentPassword, string newPassword)
        {
var user = await _userManager.FindByIdAsync(id.ToString());
            if (currentPassword == newPassword)
            {
                throw new Exception("Current password is equal with new password");
            }
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!isPasswordValid)
            {
                throw new Exception("Current password is incorrect.");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Fail to change password");
            }
            return true;
        }

        public async Task<bool> ChangeStatusbyId(Guid id)
        {
 var getUser = await _repositoryManager.GetUserById(id);
            if (getUser == null)
            {
                throw new Exception("User not found.");
            }

            if (getUser.Status)
            {
                getUser.Status = false;
            }
            else
            {
                getUser.Status = true;
            }
            var result = await _userManager.UpdateAsync(getUser);
            return result.Succeeded;        }

        public async Task<bool> CheckPhoneNumerAsync(string phoneNumber)
        {
var user = await _repositoryManager.GetUserByPhoneAsync(phoneNumber);
            return user != null;
        }        
        public async Task<UserResponse> GetUserById(Guid id)
        {
            var getUser = await _repositoryManager.GetUserById(id);
            if (getUser == null)
            {
                throw new Exception("User not found.");

            }
        var userResponse = _mapper.Map<UserResponse>(getUser);
            var roles = await _userManager.GetRolesAsync(getUser);
            userResponse.Role = roles.FirstOrDefault();
            return userResponse;
        }

        public async Task<PageResult<UserResponse>> GetUsersAsync(int page, int limit, string search, bool? status)
        {
           var allUsers = await _repositoryManager.GetAllAsync();
    var pagedUsers = await _repositoryManager.GetUsersAsync(page, limit, search, status);

    // Tính tổng số người dùng
    var totalItems = allUsers.Count();

    // Ánh xạ danh sách người dùng sang kiểu UserResponse
    var userResponses = _mapper.Map<IEnumerable<UserResponse>>(pagedUsers).ToList();

    // Lấy vai trò cho từng người dùng và thêm vào danh sách trả về
    var roleTasks = pagedUsers.Select(async user =>
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault();
    }).ToArray();

    var rolesResults = await Task.WhenAll(roleTasks);

    for (int i = 0; i < userResponses.Count; i++)
    {
        userResponses[i].Role = rolesResults[i];
    }

    return new PageResult<UserResponse>
    {
        CurrentPage = page,
        TotalPages = limit > 0 ? (int)Math.Ceiling(totalItems / (double)limit) : 1,
        TotalItems = totalItems,
        Items = userResponses
    };}
        public async Task<bool> RemoveUser(Guid id)
        {
  if (id == null) throw new Exception("User not found.");
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) throw new Exception("User not found.");

           // user.LockoutEnabled = true;

            user.Status = false;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;        }

        public async Task<UserResponse> UpdateUser(Guid id, CreateUpdateUserRequest User)
        {

            var UserToEdit = await _userManager.FindByIdAsync(id.ToString());
            if (UserToEdit == null) throw new Exception("User not found.");
            var roles = await _userManager.GetRolesAsync(UserToEdit);

            // Neu Roles thay doi
            if (roles.FirstOrDefault() != User.Role)
            {
                await _repositoryManager.RemoveUserFromRoleAsync(UserToEdit.Id, roles.ToArray());
                var addedResult = await _userManager.AddToRoleAsync(UserToEdit, User.Role);
                if (addedResult.Succeeded)
                {
                    Console.WriteLine("Alice đã được thêm vào role 'Manager' thành công.");
                }
                else
                {
                    // Nếu có lỗi, hiển thị các lỗi
                    foreach (var error in addedResult.Errors)
                    {
                        Console.WriteLine($"Lỗi: {error.Description}");
                    }
                }
            }

            if (UserToEdit.Email != User.Email)
                UserToEdit.Email = User.Email;

            if (UserToEdit.FullName != User.FullName)
            {
                UserToEdit.FullName = User.FullName;
            }

            if (UserToEdit.PhoneNumber != User.PhoneNumber)
                UserToEdit.PhoneNumber = User.PhoneNumber;

            var result = await _userManager.UpdateAsync(UserToEdit);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    throw new Exception($"{error.Description}");
            }
            UserToEdit.UpdatedAt = DateTime.Now;
            var UserReponse = _mapper.Map<UserResponse>(UserToEdit);

            return UserReponse;        }
    }
}
