using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;

namespace KoiFish_Core.Services
{
    public interface IBlogService
    {
        Task<bool> AddBlog(CreateBlogRequest request);
        Task<bool>UpdateBlog(Guid id ,UpdateBlogRequest request);
        Task<bool>DeleteBlog(Guid id);
        Task<PageResult<BlogResponse>>GetAllBlogASync(int page , int limit);
        Task<BlogResponse>GetBlogByIdAsync(Guid id);
        Task<BlogResponse>GetBlogByUserNameAsync(string name);
    }
}