﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class AspNetUserClaim
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
