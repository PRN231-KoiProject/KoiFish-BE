using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KoiFish_Core;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace KoiFish_API.Controllers
{
    [Route("api/v1/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ResultModel _resultModel;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
            _resultModel = new ResultModel();
        }
        [HttpPost]
        public async Task<ActionResult<ResultModel>> Add(CreateBlogRequest request)
        {
            var add = await _blogService.AddBlog(request);
            if (add == null)
            {
                return new ResultModel
                {
                    Data = false,
                    Message = "Add fail.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            return Ok(new ResultModel
            {
                Data = true,
                Message = "Add success.",
                Status = (int)HttpStatusCode.OK,
                Success = true
            });
        }
        [HttpPut]
        [Route("blogId")]
        public async Task<ActionResult<ResultModel>> Update(UpdateBlogRequest request, Guid blogId)
        {
            var result = await _blogService.UpdateBlog(blogId, request);
            if (result == null) return new ResultModel
            {
                Data = false,
                Message = "Update fail.",
                Status = (int)HttpStatusCode.InternalServerError,
                Success = false

            };
            return new ResultModel
            {
                Data = true,
                Message = "update success.",
                Status = (int)HttpStatusCode.NoContent,
                Success = true,
            };

        }
        [HttpDelete]
        [Route("blogId")]
        public async Task<ActionResult<ResultModel>> Remove(Guid blogId)
        {
            var result = await _blogService.DeleteBlog(blogId);
            if (result == null) return new ResultModel
            {
                Data = false,
                Message = "Remove fail.",
                Status = (int)HttpStatusCode.InternalServerError,
                Success = false

            };
            return new ResultModel
            {
                Data = true,
                Message = "Remove success.",
                Status = (int)HttpStatusCode.NoContent,
                Success = true,
            };

        }
    }
}