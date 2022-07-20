using DataTransfer.Base;

namespace DataTransfer.DataTransferObjects
{
    public class TodoDto : BaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Expiration { get; set; }
        public long UserId { get; set; }
    }
}
