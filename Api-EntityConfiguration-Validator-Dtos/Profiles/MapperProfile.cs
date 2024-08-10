using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.UserDto;
using Api_EntityConfiguration_Validator_Dtos.Models;
using AutoMapper;

namespace Api_EntityConfiguration_Validator_Dtos.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, UserGetDto>();

        }
    }
}
