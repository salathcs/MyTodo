using Entities;
using Entities.Models;
using MyAuth_lib.Auth_Server.Models;
using MyAuth_lib.Interfaces;
using MyLogger.Interfaces;

namespace MyAuth.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IMyLogger logger;
        private readonly MyTodoContext context;

        public IdentityRepository(IMyLogger logger, MyTodoContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public User? TryGetUser(AuthRequest authRequest)
        {
            //only one identity should exist with the specified username
            var identity = context.Identites.FirstOrDefault(x => x.UserName.Equals(authRequest.UserName));

            if (identity is null)
            {
                logger.Info($"Identity not found for username: {authRequest.UserName}!");
                return null;
            }

            logger.Debug($"Identity found for username: {authRequest.UserName}!");

            if (!identity.Password.Equals(authRequest.Password))
            {
                logger.Info("Password check failed!");
                return null;
            }

            logger.Debug("Identity passed password check, User will returned!");
            return identity.IdentityUser;
        }
    }
}
