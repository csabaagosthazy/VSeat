
namespace DTO
{
    public class Courier:AspNetUser
    {
        public int CourierId { get; set; }
        public string LoginId { get; set; }
        public int WorkingCityId { get; set; }

    }
}
