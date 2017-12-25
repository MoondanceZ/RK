using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RK.Model
{
    public class RecordType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IconName { get; set; }
        [Required]
        public string IconCss { get; set; }
        public int? UserId { get; set; }
    }
}
