namespace Brivo_API.Models
{
    public class User
    {
        public int id { get; set; }
        public string externalId { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public bool suspended { get; set; }
        public bool bleTwoFactorExempt { get; set; }
        public List<object> customFields { get; set; }
        public List<object> emails { get; set; }
        public List<object> phoneNumbers { get; set; }
    }
}
