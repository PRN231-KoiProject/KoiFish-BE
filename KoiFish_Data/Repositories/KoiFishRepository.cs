using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace KoiFish_Data.Repositories
{
    public class KoiFishRepository : RepositoryBase<KoiFish, Guid>, IKoiFishRepository
    {
        public KoiFishRepository(KoiFishDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<KoiFish>> GetAllKoiFistAsync(int page, int limit)
        {
            IQueryable<KoiFish> query = _context.KoiFishes
           .Include(k => k.Category)
           .Include(k => k.FishColors).ThenInclude(fc => fc.Color)
           .Include(i => i.Images).Include(u => u.User).AsQueryable();
            int totalItems = await query.CountAsync();
            if (page > 0 && limit > 0)
            {
                query = query.Skip((page - 1) * limit).Take(limit);
            }

            var products = await query.ToListAsync();
            return new PaginatedResult<KoiFish>
            {
                Items = products,
                TotalCount = totalItems,
            };

        }
        public async Task<KoiFish> GetKoiFishById(Guid id)
        {
            var query = await _context.KoiFishes
                .Where(k => k.KoiFishId == id)
                .Include(k => k.Category)
                .Include(k => k.FishColors).ThenInclude(fc => fc.Color)
                .Include(i => i.Images)
                .Include(u => u.User)
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task<IEnumerable<KoiFish>> GetKoiFishesByUserElementAsync(string element)
        {
            var userElement = await _context.Users
            .Where(u => u.Elements.ElementName == element)
            .Select(e => e.Elements.ElementName).FirstOrDefaultAsync();
            
            string relatedElement = userElement switch
            {
                "Metal" => "Water",
                "Water" => "Wood",
                "Wood" => "Fire",
                "Fire" => "Earth",
                "Earth" => "Metal",
                _ => null
            };
            if (relatedElement == null)
            {
                return new List<KoiFish>();
            }
            var KoiFishes = await _context.KoiFishes.Include(c => c.Category)
            .Include(u => u.User).Include(img => img.Images)
           .Include(k => k.FishColors).ThenInclude(fc => fc.Color)
            .Where(kf => kf.FishElement == relatedElement).ToListAsync();
            return KoiFishes;
        }

        public async Task<int> SaveChangeASync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}