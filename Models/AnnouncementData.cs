using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Share2Connect.Api.Models
{
    public class AnnouncementData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string DateTime { get; set; }
        [AllowNull]
        public int NumberOfTicket { get; set; }
        [AllowNull]
        public int Price { get; set; }
        [AllowNull]
        public int EmptySeats { get; set; }
        [AllowNull]
        public string? Place_name { get; set; }
        [AllowNull]
        public string? Place_gps { get; set; }
        [AllowNull]
        public ImageFile Image { get; set; }
        public List<Participant>? Participants { get; set; } 
    }
}
