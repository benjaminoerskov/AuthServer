﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace AuthServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<AssociatedOccurrences> AssociatedOccurrences { get; set; } = new HashSet<AssociatedOccurrences>();
    }
}
