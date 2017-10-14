﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RK.Model
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Account { get; set; }
        [StringLength(32)]
        public string Password { get; set; }
    }
}
