﻿using KoiFish_Core;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KoiFish_API.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private ResultModel _resultModel;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _resultModel = new ResultModel();
        }
        [HttpGet]
        public async Task<ActionResult<ResultModel>> GetAll()
        {
            var listCategory = await _categoryService.GetAllCategories();
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = listCategory,
                Message = "Categories retrieved successfully."
            });
        }
        [HttpPost]
        public async Task<ActionResult<ResultModel>> AddCategory(CreateCategoryRequest request)
        {
            var updateSuccess = await _categoryService.CreateAsync(request);

            if (!updateSuccess)
            {
                _resultModel = new ResultModel
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = "Create Category fail."
                };
                return BadRequest(_resultModel);
            }

            _resultModel = new ResultModel
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = "Create Category Successfully.",
            };

            return Ok(_resultModel);
        }

    }
}
