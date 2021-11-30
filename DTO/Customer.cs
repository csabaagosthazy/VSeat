namespace DTO
{
    public class Customer : AspNetUser
    {
        public int CustomerId { get; set; }
        public string LoginId { get; set; }
    }
}
