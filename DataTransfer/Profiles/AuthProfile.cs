using AutoMapper;
using DataTransfer.DataTransferObjects;
using System.Security.Claims;

namespace DataTransfer.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Claim, ClaimDto>();
        }
    }
}
