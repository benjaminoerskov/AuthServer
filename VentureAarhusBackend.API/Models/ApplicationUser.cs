using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DetSkerIAarhusBackend.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<AssociatedOccurrences> AssociatedOccurrences { get; set; } = new HashSet<AssociatedOccurrences>();
    }
}
