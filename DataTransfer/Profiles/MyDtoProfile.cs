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
                .ReverseMap();

            CreateMap<User, UserWithIdentityDto>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(entity => entity.UserIdentity.UserName))
                .ReverseMap()
                .ForPath(entity => entity.UserIdentity.UserName, opt => opt.MapFrom(dto => dto.UserName))
                .ForPath(entity => entity.UserIdentity.Password, opt => opt.MapFrom(dto => dto.Password));

            CreateMap<Todo, TodoDto>()
                .ReverseMap()
                .ForPath(entity => entity.UserId, opt => opt.Ignore());


            CreateMap<Todo, TodoWithEmailDto>()
                .ForMember(dto => dto.Email, opt => opt.MapFrom(entity => entity.TodoUser.Email));
        }
    }
}
