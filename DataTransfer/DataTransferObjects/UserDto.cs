using DataTransfer.Base;

namespace DataTransfer.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
