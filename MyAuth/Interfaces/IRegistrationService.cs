using DataTransfer.DataTransferObjects;

namespace MyAuth.Interfaces
{
    public interface IRegistrationService
    {
        void Register(UserDto userDto);
    }
}
