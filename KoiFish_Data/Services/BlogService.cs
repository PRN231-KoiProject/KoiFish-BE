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

        public async Task<bool> DeleteBlog(Guid id)
        {
            var blogId = await _blogRepository.GetByIdAsync(id);
            if(blogId == null) return false;
            _blogRepository.Remove(blogId);
            await _blogRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> UpdateBlog(Guid id, UpdateBlogRequest request)
        {
            var blogId = await _blogRepository.GetByIdAsync(id);
            if (blogId == null) return false;
            blogId.Content = request.Content;
            blogId.Title = request.Title;
            blogId.CreatedDate = DateTime.Now;
            blogId.UpdatedDate = DateTime.Now;
            _blogRepository.Update(blogId);
            await _blogRepository.SaveChangeAsync();
            return true;
        }
    }
}