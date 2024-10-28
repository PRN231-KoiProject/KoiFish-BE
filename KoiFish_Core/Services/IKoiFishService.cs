using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;

namespace KoiFish_Core.Services
{
    public interface IKoiFishService
    {
        Task<bool> AddKoiFishAsync(CreateKoiFishRequest request);
        Task<PageResult<KoiFishResponse>> GetAllKoiFishAsync(int page, int limit);
        Task<bool>UpdateKoiFishAsync(UpdateKoiFishRequest request , Guid id);
        Task<KoiFishResponse>GetKoiFishByIdAsync(Guid id);
    }
}