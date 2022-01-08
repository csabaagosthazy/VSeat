using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class RestaurantVM
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
