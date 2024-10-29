using KoiFish_Core;
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
        public async Task<ActionResult<ResultModel>> GetAll(int page = 1, int limit = 10)
        {
            var listCategory = await _categoryService.GetAllCategories(page, limit);
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = listCategory,
                Message = "Categories retrieved successfully."
            });
        }
        [HttpGet]
        [Route("{categoryId}")]
        public async Task<ActionResult<ResultModel>> GetById(Guid categoryId)
        {
            var listCategory = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (categoryId == null)
            {
                return Ok(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Category not found."
                });
            }
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = listCategory,
                Message = "Categories retrieved successfully."
            });
        }
        [HttpPut]
        [Route("{categoryId}")]
        public async Task<ActionResult<ResultModel>> Update(Guid categoryId, CreateCategoryRequest request)
        {
            var listCategory = await _categoryService.GetCategoryByIdAsync(categoryId);
            var update = _categoryService.UpdateCategory(categoryId, request);
            if (listCategory == null)
            {
                return Ok(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Category not found."
                });
            }
            if (update == null)
            {
                return Ok(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Data = false,
                    Message = "Categories update fail."
                });
            }
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = true,
                Message = "Categories Update successfully."
            });
        }
        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<ActionResult<ResultModel>> Delete(Guid id)
        {
            var listCategory = await _categoryService.GetCategoryByIdAsync(id);
            var update = _categoryService.DeleteCategory(id);
            if (listCategory == null)
            {
                return Ok(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Category not found."
                });
            }
            if (update == null)
            {
                return Ok(_resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Data = false,
                    Message = "Category delete fail."
                });
            }
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = true,
                Message = "Categories delete successfully."
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
