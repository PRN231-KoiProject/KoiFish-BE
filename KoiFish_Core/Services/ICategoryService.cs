using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Services
{
    public interface ICategoryService
    {
        Task<PageResult<CategoryResponse>> GetAllCategories(int page, int limit);
        Task<bool> CreateAsync(CreateCategoryRequest request);
        Task<CategoryResponse>GetCategoryByIdAsync(Guid id);
        Task<bool>UpdateCategory(Guid id , CreateCategoryRequest request);
    }
}
