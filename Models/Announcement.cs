using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share2Connect.Api.Models
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Post_id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int User_id { get; set; }
        [Required]
        public AnnouncementData Data { get; set; }
    }
}
