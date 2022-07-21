using Entities.Models;

namespace MyAuth.Interfaces
{
    public interface IRegistrationRepository
    {
        void CreateUser(User user);
    }
}
