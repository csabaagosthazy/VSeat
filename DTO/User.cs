using Microsoft.AspNetCore.Identity;
using System;

namespace DTO
{
    public class User
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public int CityId { get; set; }
    }
}
