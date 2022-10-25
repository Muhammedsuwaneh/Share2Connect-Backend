using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share2Connect.Api.Models
{
    public class AnnouncementData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Date { get; set; }
        public string Clock { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string? Place_name { get; set; }
        public string? Place_gps { get; set; }
        public List<Participant>? Participants { get; set; } 
    }
}
