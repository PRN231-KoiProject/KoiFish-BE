using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Repositories;
using KoiFish_Core.Services;

namespace KoiFish_Data.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<bool> CreateColor(CreateColorRequest color)
        {
            var model = new Color
            {

                ColorName = color.ColorName,
                Description = color.Description
            };
            _colorRepository.Add(model);
            await _colorRepository.SaveChangeAsync();
            return true;
        }
    }
}