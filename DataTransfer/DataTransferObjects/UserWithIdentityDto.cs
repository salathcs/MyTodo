namespace DataTransfer.DataTransferObjects
{
    public class UserWithIdentityDto : UserDto
    {
        //from Identity
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
