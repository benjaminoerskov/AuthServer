using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.DTOs
{
    public class PostAssociatedOccurrencesParams
    {
        [Required]
        public string TypeOfAssociation { get; set; }
        [Required]
        public int EventId { get; set; }
    }
}