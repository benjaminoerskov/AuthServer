using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuthServer.Models
{
    [Table("AssociatedOccurrences")]
    public class AssociatedOccurrences
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int EventId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public string Type { get; set; }


    }
}
