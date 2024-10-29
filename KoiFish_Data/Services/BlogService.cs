using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Repositories;
using KoiFish_Core.Services;

namespace KoiFish_Data.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> AddBlog(CreateBlogRequest request)
        {
            var model = new Blog
            {
                Content = request.Content,
                Title = request.Title,
                UserId = request.UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _blogRepository.Add(model);
            await _blogRepository.SaveChangeAsync();
            return true;
        }

        public Task<bool> UpdateBlog(CreateBlogRequest request)
        {
             return null; 
        }
    }
}