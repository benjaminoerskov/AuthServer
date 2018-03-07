using System.ComponentModel.DataAnnotations.Schema;

namespace DetSkerIAarhusBackend.API.Models
{
    [Table("AssociatedOccurrences")]
    public class AssociatedOccurrences
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int OccurrenceId { get; set; }
        public string Type { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
