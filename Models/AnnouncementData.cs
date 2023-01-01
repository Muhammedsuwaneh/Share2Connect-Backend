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
        public string adNameText { get; set; }
        [Required]
        public string adDescText { get; set; }
        [AllowNull]
        public byte[]? adImage { get; set; }
        [AllowNull]
        public string? adClubName { get; set; }
        [AllowNull]
        public string? adDateText { get; set; }
        [AllowNull]
        public string? adTicketText { get; set; }
        [AllowNull]
        public string? adPriceText { get; set; }
        [AllowNull]
        public string? adSeatText { get; set; }
        [AllowNull]
        public string? adPlaceText { get; set; }
        [AllowNull]
        public string? adPlaceGPS { get; set; }
        [AllowNull]
        public string? adRouteStartText { get; set; }
        [AllowNull]
        public string? adRouteEndText { get; set; }
        [AllowNull]
        public string? adRouteStartGPS { get; set; }
        [AllowNull]
        public string? adRouteEndGPS { get; set; }
        [AllowNull]
        public List<Participant>? Participants { get; set; } 
    }
}
