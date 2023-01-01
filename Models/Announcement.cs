using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share2Connect.Api.Models
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int post_id { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public AnnouncementData data { get; set; }
    }
}
