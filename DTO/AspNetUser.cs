using Microsoft.AspNetCore.Identity;
using System;

namespace DTO
{
    public class AspNetUser : IdentityUser
    {
        //public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Email { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        //public string NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; } = false;
        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }
        //public string ConcurrencyStamp { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; } = false;
        //public bool TwoFactorEnabled { get; set; }
        //public DateTimeOffset? LockoutEnd { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public int AccessFailedCount { get; set; }
        public DateTime CreationDate { get; set; }
        public int CityId { get; set; }
    }
}
