using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuthServer.Models
{
    public class AssociatedEvents
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int EventId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }


    }
}
