using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share2Connect.Api.Models
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Post_id { get; set; }
        public string Category { get; set; }
        public int User_id { get; set; }
        public AnnouncementData Data { get; set; }
    }
}
