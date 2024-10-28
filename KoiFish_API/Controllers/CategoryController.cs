using KoiFish_Core;
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
                Status = (int) HttpStatusCode.OK,
                Data = listCategory,
                Message = "Categories retrieved successfully."
            });
        }
    }
}
