using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.DTOs
{
    public class AssociatedOccurrencesDTO
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int OccurrenceId { get; set; }
        public string Type { get; set; }
    }
}
