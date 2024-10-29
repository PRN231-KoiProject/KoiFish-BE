using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;

namespace KoiFish_Core.Services
{
    public interface IPondFeatureService
    {
        Task<bool>CreatePondFeature(CreatePondFeatureRequest request);
        Task<PageResult<PondFeatureResponse>> GetAllPondFeatureAsync(int page, int limit);
        Task<PondFeatureResponse>GetById(Guid id);
    }
}