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
    [Route("api/v1/KoiFishes")]
    [ApiController]
    public class KoiFishController : ControllerBase
    {
        private readonly IKoiFishService _koiFishService;
        private ResultModel _resultModel;
        public KoiFishController(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
            _resultModel = new ResultModel();
        }
        [HttpGet]
        public async Task<ActionResult<ResultModel>> GetAll(int page = 1, int limit = 10)
        {
            var listCategory = await _koiFishService.GetAllKoiFishAsync(page, limit);
            return Ok(_resultModel = new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Data = listCategory,
                Message = "KoiFishes retrieved successfully."
            });
        }
        [HttpPost]
        public async Task<ActionResult<ResultModel>> AddKoiFish(CreateKoiFishRequest request)
        {
            var updateSuccess = await _koiFishService.AddKoiFishAsync(request);

            if (!updateSuccess)
            {
                _resultModel = new ResultModel
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = "Create KoiFish fail.",
                    Data = false
                };
                return BadRequest(_resultModel);
            }

            _resultModel = new ResultModel
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = "Create KoiFish Successfully.",
                Data = updateSuccess
            };

            return Ok(_resultModel);
        }
        [HttpPut("{koiFishId}")]
        public async Task<ActionResult<ResultModel>> UpdateKoiFish(Guid koiFishId, UpdateKoiFishRequest request)
        {
            try
            {

                var updateResult = await _koiFishService.UpdateKoiFishAsync(request, koiFishId);


                if (!updateResult)
                {
                    _resultModel = new ResultModel
                    {
                        Success = false,
                        Status = (int)HttpStatusCode.NotFound,
                        Message = "Failed to update koi fish. The koi fish with the provided ID was not found."
                    };
                    return NotFound(_resultModel);
                }


                _resultModel = new ResultModel
                {
                    Success = true,
                    Status = (int)HttpStatusCode.OK,
                    Message = "Koi fish updated successfully."
                };
                return Ok(_resultModel);
            }
            catch (Exception ex)
            {

                _resultModel = new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while updating the koi fish: {ex.Message}"
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, _resultModel);
            }
        }
        [HttpGet]
        [Route("{KoiFishId}")]
        public async Task<ActionResult<ResultModel>> GetKoiFish(Guid KoiFishId)
        {

            var koiFish = await _koiFishService.GetKoiFishByIdAsync(KoiFishId);
            if (koiFish == null)
            {
                return NotFound(new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "KoiFish not found."
                });
            }
            return Ok(new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.OK,
                Message = "KoiFish with id retrieved successfully.",
                Data = koiFish
            });
        }
        [HttpDelete]
        [Route("{koiFishId}")]
        public async Task<ActionResult<ResultModel>> DeleteKoiFish(Guid koiFishId)
        {
            var koiFish = await _koiFishService.GetKoiFishByIdAsync(koiFishId);
            if (koiFish == null)
            {

                return NotFound(new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.NotFound,
                    Message = "KoiFish not found."
                });
            }
            var remove = await _koiFishService.DeleteKoiFish(koiFishId);
            if (remove == null)
            {

                return new ResultModel
                {
                    Success = false,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "Remove fail.",
                    Data = false
                };

            }
            return Ok(new ResultModel
            {
                Success = true,
                Status = (int)HttpStatusCode.NoContent,
                Message = "Remove KoiFish Success.",
                Data = true
            });


        }
    }
}