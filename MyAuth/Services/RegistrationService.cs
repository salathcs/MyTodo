using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyAuth.Interfaces;

namespace MyAuth.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationRepository;
        private readonly IMapper mapper;

        public RegistrationService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
        }

        public void Register(UserWithIdentityDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            user.Created = DateTime.UtcNow;
            user.Updated = DateTime.UtcNow;
            user.CreatedBy = "Registration";
            user.UpdatedBy = "Registration";
            registrationRepository.CreateUser(user);
        }
    }
}
