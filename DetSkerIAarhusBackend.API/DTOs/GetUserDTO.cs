using System.Collections.Generic;

namespace DetSkerIAarhusBackend.API.DTOs
{
    public class GetUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<AssociatedOccurrencesDTO> AssociatedOccurrences { get; set; } = new HashSet<AssociatedOccurrencesDTO>();

    }
}
