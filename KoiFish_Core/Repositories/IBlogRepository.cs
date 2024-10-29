using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.SeedWorks;

namespace KoiFish_Core.Repositories
{
    public interface IBlogRepository : IRepositoryBase<Blog, Guid>
    {
        Task<int> SaveChangeAsync();
        Task<PaginatedResult<Blog>>GetAllBlogAsync(int page , int limit);
        Task<Blog>GetBlogByIdAsync(Guid id);
        Task<Blog>GetBlogByUserNameAsync(string name);
    }
}