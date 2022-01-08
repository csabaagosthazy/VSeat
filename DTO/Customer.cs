namespace DTO
{
    public class Customer : AspNetUser
    {
        public string CustomerId { get; set; }
        public string LoginId { get; set; }
    }
}
