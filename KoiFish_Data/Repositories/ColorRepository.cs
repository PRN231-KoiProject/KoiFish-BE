using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Repositories;
using KoiFish_Data.SeedWorks;

namespace KoiFish_Data.Repositories
{
    public class ColorRepository : RepositoryBase<Color, Guid>, IColorRepository
    {
        public ColorRepository(KoiFishDbContext context) : base(context)
        {
        }

        public async Task<int> SaveChangeAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}