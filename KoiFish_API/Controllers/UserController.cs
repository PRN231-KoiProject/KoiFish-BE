using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using KoiFish_Core.Services;
using KoiFish_Core.Domain.Identity;
using KoiFish_Core;
using KoiFish_Core.Models.Requests;

namespace KoiFish_API.Controllers
{
    
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _serviceManager;
        private readonly UserManager<AppUser> _userManager;

        private ResultModel _resultModel;

        public UserController(IUserService service, UserManager<AppUser> userManager)
        {
            _serviceManager = service;
            _userManager = userManager;

            _resultModel = new ResultModel();
        }

        [HttpGet]
       // [Authorize]
        public async Task<ActionResult<ResultModel>> GetAll(int page = 1, int limit = 10, string search = null, [FromQuery] bool? status = null)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
          //  var user = await _userManager.FindByEmailAsync(userEmail);
            var listusers = await _serviceManager.GetUsersAsync(page, limit, search, status);
            if (listusers == null)
            {
                _resultModel = new ResultModel
                {
                    Success = false,
                    Message = "No users found.",
                    Status = (int)HttpStatusCode.NotFound
                };
            }
            _resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = listusers,
                Message = "Users retrieved successfully."
            };

            return Ok(_resultModel);
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<ResultModel>> GetUserById(Guid userId)
        {
            var user = await _serviceManager.GetUserById(userId);
            if (user == null)
            {
                _resultModel = new ResultModel
                {
                    Success = false,
                    Message = "User not found.",
                    Status = (int)HttpStatusCode.NotFound
                };
            }
            else
            {
                _resultModel = new ResultModel
                {
                    Success = true,
                    Status = (int)HttpStatusCode.OK,
                    Data = user,
                    Message = "User retrieved successfully."
                };
            }
            return Ok(_resultModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {

            if (!ModelState.IsValid)
            {
                _resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.BadRequest
                };
            }

            var checkUserExisted = await _serviceManager.CheckPhoneNumerAsync(request.PhoneNumber);
            if (checkUserExisted)
            {
                return Ok(new ResultModel
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = "Your Phone Number Existed."
                });
            }


            var result = await _serviceManager.AddUser(request);
            if (result == null)
            {
                _resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Failed to add user."
                };
                return NotFound(_resultModel);
            }

            _resultModel = new ResultModel
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = "User added successfully."
            };
            return Ok(_resultModel);
        }

        [HttpPost("admin/{userId}")]
        public async Task<ActionResult<ResultModel>> Update(Guid userId, CreateUserRequest request)
        {
            var update = await _serviceManager.UpdateUser(userId, request);
            if (update == null)
            {
                //update fail
                return NotFound(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Failed to update user."
                });
            }
            // update success
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Message = "User updated successfully."
            });
        }

    

        [HttpPut("{userId}/password")]
        public async Task<ActionResult<ResultModel>> ChangePassword(Guid userId, string currentPassword, string newPassword)
        {

            var result = await _serviceManager.ChangePasswordbyId(userId, currentPassword, newPassword);
            if (result)
            {

                return Ok(_resultModel = new ResultModel
                {
                    Success = true,
                    Status = (int)HttpStatusCode.OK,
                    Message = "Change password user successfully."
                });
            }
            else
            {
                return NotFound(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Failed to change password user."
                });
            }
        }

   
        [HttpPatch("{userId}/status")]
        public async Task<ActionResult<ResultModel>> ChangeUserStatus(Guid userId)
        {
            // Gọi dịch vụ để tìm người dùng theo ID
            var update = await _serviceManager.ChangeStatusbyId(userId);

            if (!update)
            {
                //update fail
                return NotFound(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Failed to update user."
                });
            }
            // update success
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Message = "User updated successfully."
            });

        }



    }
}
