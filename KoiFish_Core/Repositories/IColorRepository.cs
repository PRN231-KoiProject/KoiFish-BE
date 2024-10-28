using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.SeedWorks;

namespace KoiFish_Core.Repositories
{
    public interface IColorRepository : IRepositoryBase<Color, Guid>
    {
        Task<int> SaveChangeAsync();
    }
}