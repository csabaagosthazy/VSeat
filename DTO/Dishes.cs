﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Dishes
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
        public int  SpiceLevel { get; set; }
        public int RestaurantId { get; set; }

    }
}
