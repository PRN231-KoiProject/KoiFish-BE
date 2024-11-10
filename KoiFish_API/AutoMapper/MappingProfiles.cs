using AutoMapper;
using KoiFish_Core.Domain.Identity;
using KoiFish_Core.Models.Responses;

namespace KoiFish_API.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
                        CreateMap<AppUser,UserResponse>()               .ReverseMap();


        }
    }
}
