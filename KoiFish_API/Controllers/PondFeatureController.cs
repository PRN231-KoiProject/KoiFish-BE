using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KoiFish_Core;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace KoiFish_API.Controllers
{
    [Route("api/v1/Ponds")]
    [ApiController]
    public class PondFeatureController : ControllerBase
    {
        private readonly IPondFeatureService _pondFeatureService;
        private readonly ResultModel _resultModel;
        public PondFeatureController(IPondFeatureService pondFeatureService)
        {
            _pondFeatureService = pondFeatureService;
            _resultModel = new ResultModel();
        }
        [HttpPost]
        public async Task<ActionResult<ResultModel>> Create(CreatePondFeatureRequest request)
        {
            var pond = await _pondFeatureService.CreatePondFeature(request);
            if (pond == null)
            {
                return new ResultModel
                {
                    Data = false,
                    Message = "Create PondFeature fail.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            return new ResultModel
            {
                Data = true,
                Message = "Create PondFeature success.",
                Status = (int)HttpStatusCode.OK,
                Success = true
            };
        }
        [HttpGet]
        public async Task<ActionResult<ResultModel>> GetAll(int page = 1, int limit = 10)
        {
            var listCategory = await _pondFeatureService.GetAllPondFeatureAsync(page, limit);
            return Ok(new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = listCategory,
                Message = "Ponds retrieved successfully."
            });
        }
        [HttpGet]
        [Route("{pondFeatureId}")]
        public async Task<ActionResult<ResultModel>> GetById(Guid pondFeatureId)
        {
            var pond = await _pondFeatureService.GetById(pondFeatureId);
            if (pond == null)
            {
                return new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Pond not found. "
                };
            }
            return Ok(new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = pond,
                Message = "Pond retrieved successfully."
            });
        }
        [HttpPut]
        [Route("{pondFeatureId}")]
        public async Task<ActionResult<ResultModel>> Update(Guid pondFeatureId, UpdatePondFeatureRequest request)
        {
            var pond = await _pondFeatureService.GetById(pondFeatureId);

            if (pond == null)
            {
                return new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "Pond not found. "
                };
            }
            var update = await _pondFeatureService.UpdateAsync(request, pondFeatureId);

            if (update == null)
            {
                return new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "update fail.",
                    Data = false
                };
            }
            return Ok(new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = true,
                Message = "Update success."
            });
        }
    }
}