using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KoiFish_Core;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace KoiFish_API.Controllers
{
    [Route("api/v1/Colors")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        private  ResultModel _resultModel;

        public ColorController(IColorService colorService )
        {
            _colorService = colorService;
            _resultModel = new ResultModel();
        } 
        [HttpPost]
        public async Task<ActionResult<ResultModel>> AddColor(CreateColorRequest request)
        {
            var updateSuccess = await _colorService.CreateColor(request);

            if (!updateSuccess)
            {
                _resultModel = new ResultModel
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = "Create Color fail."
                };
                return BadRequest(_resultModel);
            }

            _resultModel = new ResultModel
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = "Create Color Successfully.",
            };

            return Ok(_resultModel);
        }


    }
}