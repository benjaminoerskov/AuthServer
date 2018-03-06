using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Models;

namespace AuthServer.DTOs
{
    public class GetUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<AssociatedOccurrencesDTO> AssociatedOccurrences { get; set; } = new HashSet<AssociatedOccurrencesDTO>();

    }
}
