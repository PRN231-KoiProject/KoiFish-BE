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
    public class PondFeatureRepository : RepositoryBase<PondFeature, Guid>, IPondFeatureRepository
    {
        public PondFeatureRepository(KoiFishDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<PondFeature>> GetAllPondFeatureAsync(int page, int limit)
        {
            IQueryable<PondFeature> query = _context.PondFeatures.AsQueryable();
            int totalItems = await query.CountAsync();
            if (page > 0 && limit > 0)
            {
                query = query.Skip((page - 1) * limit).Take(limit);
            }

            var pondFeatures = await query.ToListAsync();
            return new PaginatedResult<PondFeature>
            {
                Items = pondFeatures,
                TotalCount = totalItems,
            };
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}