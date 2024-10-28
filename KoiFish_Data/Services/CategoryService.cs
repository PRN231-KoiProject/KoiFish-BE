﻿using AutoMapper;
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

        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            var listCategory = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(listCategory);
        }
    }
}