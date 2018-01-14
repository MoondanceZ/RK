﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RK.Model
{
    public class AccountRecord
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 类型:1,收入; 2,支出
        /// </summary>
        [Required]
        public int Type { get; set; }
        /// <summary>
        /// 记录外键ID
        /// </summary>
        public int AccountRecordTypeId { get; set; }
        /// <summary>
        /// 记录
        /// </summary>
        public virtual AccountType RecordType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }
        /// <summary>
        /// 状态: -1,删除;1,正常;0,禁用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeletedTime { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int UserId { get; set; }

    }
}