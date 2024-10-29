using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace KoiFish_Data.Repositories
{
    public class BlogRepository : RepositoryBase<Blog, Guid>, IBlogRepository
    {
        public BlogRepository(KoiFishDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Blog>> GetAllBlogAsync(int page, int limit)
        {
            IQueryable<Blog> query = _context.Blogs.Include(u => u.User).AsQueryable();
            int totalItems = await query.CountAsync();
            if (page > 0 && limit > 0)
            {
                query = query.Skip((page - 1) * limit).Take(limit);
            }

            var blogs = await query.ToListAsync();
            return new PaginatedResult<Blog>
            {
                Items = blogs,
                TotalCount = totalItems,
            };

        }

        public async Task<Blog> GetBlogByIdAsync(Guid id)
        {
            var blog = await _context.Blogs.
            Include(u => u.User).
            FirstOrDefaultAsync(b => b.BlogId == id);
            return blog;
        }

        public async Task<Blog> GetBlogByUserNameAsync(string name)
        {
            var blog = await _context.Blogs.
            Include(u => u.User).
            Where(u => u.User.UserName == name).FirstOrDefaultAsync();
            return blog;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}