using AutoMapper;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Breeds = request.Breeds,
                Description = request.Description
            };
            _categoryRepository.Add(category);
            await _categoryRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var categoryId = await _categoryRepository.GetByIdAsync(id);
            _categoryRepository.Remove(categoryId);
            await _categoryRepository.SaveChangeAsync();
            return true;
        }

        public async Task<PageResult<CategoryResponse>> GetAllCategories(int page, int limit)
        {
            var listCategory = await _categoryRepository.GetAllCategories(page, limit);
            var categoryResponse = new List<CategoryResponse>();
            foreach (var category in listCategory.Items)
            {
                categoryResponse.Add(new CategoryResponse
                {
                    Breeds = category.Breeds,
                    CategoryId = category.CategoryId,
                    Description = category.Description,
                });
            }
            return new PageResult<CategoryResponse>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(listCategory.TotalCount / (double)limit),
                TotalItems = listCategory.TotalCount,
                Items = categoryResponse
            };
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var categoryId = await _categoryRepository.GetByIdAsync(id);
            return new CategoryResponse
            {
                Breeds = categoryId.Breeds,
                CategoryId = categoryId.CategoryId,
                Description = categoryId.Description
            };
        }

        public async Task<bool> UpdateCategory(Guid id, CreateCategoryRequest request)
        {
            var categoryId = await _categoryRepository.GetByIdAsync(id);
            if (categoryId == null)
            {
                return false;
            }
            categoryId.Breeds = request.Breeds;
            categoryId.Description = request.Description;
            _categoryRepository.Update(categoryId);
            await _categoryRepository.SaveChangeAsync();
            return true;
        }
    }
}
