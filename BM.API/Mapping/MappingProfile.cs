using AutoMapper;
using BM.DataAccess.Entities;
using BM.Domain.Models;

namespace BM.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            //// Mapping nested objects
            CreateMap<Slot, SlotModel>()
                .ForMember(dest => dest.User, act => act.MapFrom(src => src.User));
            CreateMap<SlotModel, Slot>()
                .ForMember(dest => dest.User, act => act.MapFrom(src => src.User));
        }
    }
}
