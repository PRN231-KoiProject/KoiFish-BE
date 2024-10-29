using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category, Guid>
    {
        Task<int>SaveChangeAsync();
        Task<PaginatedResult<Category>>GetAllCategories(int page , int limit);
    }
}
