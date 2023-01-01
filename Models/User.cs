using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Share2Connect.Api.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        [Required]
        public string userNameText { get; set; }
        [Required]
        public string userPassword { get; set; }
        [Required]
        public string userMail { get; set; }
        [Required]
        public string userGender { get; set; }
        [Required]
        public string userBio { get; set; }
        [Required]
        public string userPhoneNumber { get; set; }
        [AllowNull]
        public byte[]? userImage { get; set; }
        [Required]
        public string userDepartment { get; set; } 
    }
}
