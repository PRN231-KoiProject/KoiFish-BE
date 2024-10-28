using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;

namespace KoiFish_Core.Services
{
    public interface IColorService
    {
        Task<bool>CreateColor(CreateColorRequest color);
    }
}