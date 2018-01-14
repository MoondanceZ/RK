using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RK.Model
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 0: 收入, 1: 支出
        /// </summary>
        public int Type { get; set; }
        public int? UserInfoId { get; set; }
        /// <summary>
        /// 状态: -1,删除;1,正常;0,禁用
        /// </summary>
        public int Status { get; set; }
    }
}
