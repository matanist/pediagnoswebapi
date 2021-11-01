using AutoMapper;
using pediagnoswebapi.Models.DB;
using pediagnoswebapi.Models.Dtos;

namespace pediagnoswebapi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pet,PetDto>();
            CreateMap<User,OwnerDto>();
        }
    }
}