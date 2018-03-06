using System.ComponentModel.DataAnnotations;

namespace VentureAarhusBackend.API.DTOs
{
    public class PostAssociatedOccurrencesParams
    {
        [Required]
        public string TypeOfAssociation { get; set; }
        [Required]
        public int OccurrenceId { get; set; }
    }
}