using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RK.Model
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(64)]
        public string WeChatOpenID { get; set; }
        [Required]
        [StringLength(64)]
        public string Account { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(32)]
        public string Password { get; set; }
        [StringLength(300)]
        public string AvatarUrl { get; set; }
        [StringLength(128)]
        public string Email { get; set; }
        [StringLength(32)]
        public string Phone { get; set; }
        public int Sex { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedTime { get; set; }
    }
}
