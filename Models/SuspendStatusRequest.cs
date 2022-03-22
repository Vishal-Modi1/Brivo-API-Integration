namespace Brivo_API.Models
{
    public class SuspendStatusRequest
    {
        public bool suspended { get; set; }
    }

    public class SuspendStatusResponse
    {
        public int id { get; set; }
        public bool suspended { get; set; }
    }
}
