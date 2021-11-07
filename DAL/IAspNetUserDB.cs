using DTO;

namespace DAL
{
    interface IAspNetUserDB
    {
        AspNetUser GetUser(string email, string password);
        
    }
}