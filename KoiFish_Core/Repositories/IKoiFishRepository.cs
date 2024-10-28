using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.SeedWorks;

namespace KoiFish_Core.Repositories
{
    public interface IKoiFishRepository : IRepositoryBase<KoiFish, Guid>
    {
        Task<PaginatedResult<KoiFish>> GetAllKoiFistAsync(int page, int limit);
        Task<int> SaveChangeASync();
        Task<KoiFish>GetKoiFishById(Guid id);
    }
}