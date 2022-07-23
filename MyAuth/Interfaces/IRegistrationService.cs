using DataTransfer.DataTransferObjects;

namespace MyAuth.Interfaces
{
    public interface IRegistrationService
    {
        void Register(UserWithIdentityDto userDto);
    }
}
