using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace DataTransfer.Profiles
{
    public class MyDtoProfile : Profile
    {
        public MyDtoProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(entity => entity.UserIdentity.UserName))
                .ReverseMap()
                .ForPath(entity => entity.UserIdentity.UserName, opt => opt.MapFrom(dto => dto.UserName))
                .ForPath(entity => entity.UserIdentity.Password, opt => opt.MapFrom(dto => dto.Password));

            CreateMap<Todo, TodoDto>()
                .ReverseMap();
        }
    }
}
