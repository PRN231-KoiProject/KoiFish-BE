using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Models.Requests;

namespace KoiFish_Core.Services
{
    public interface IBlogService
    {
        Task<bool> AddBlog(CreateBlogRequest request);
        Task<bool>UpdateBlog(CreateBlogRequest request);
    }
}