using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VentureAarhusBackend.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<AssociatedOccurrences> AssociatedOccurrences { get; set; } = new HashSet<AssociatedOccurrences>();
    }
}
