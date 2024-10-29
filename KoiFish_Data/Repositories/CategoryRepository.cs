using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Data.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(KoiFishDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Category>> GetAllCategories(int page, int limit)
        {
            IQueryable<Category> query = _context.Categories.AsQueryable();
            int totalItems = await query.CountAsync();
            if (page > 0 && limit > 0)
            {
                query = query.Skip((page - 1) * limit).Take(limit);
            }

            var categories = await query.ToListAsync();
            return new PaginatedResult<Category>
            {
                Items = categories,
                TotalCount = totalItems,
            };

        }

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
