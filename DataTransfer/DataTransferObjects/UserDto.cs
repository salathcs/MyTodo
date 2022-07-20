using DataTransfer.Base;

namespace DataTransfer.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        //from User
        public string? Name { get; set; }
        public string? Email { get; set; }

        //from Identity
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
