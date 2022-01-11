using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class Cart
    {
        [Key]
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public decimal Discount { get; set; } = 0;
        public decimal TotalPrice { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
