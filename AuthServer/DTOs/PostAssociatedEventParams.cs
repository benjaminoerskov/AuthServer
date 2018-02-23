using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.DTOs
{
    public class PostAssociatedEventParams
    {
        [Required]
        public Models.Type TypeOfAssociation { get; set; }
        [Required]
        public int EventId { get; set; }
    }
}