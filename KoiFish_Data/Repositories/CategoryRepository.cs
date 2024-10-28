using KoiFish_Core.Domain.Content;
using KoiFish_Core.Repositories;
using KoiFish_Data.SeedWorks;
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

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
