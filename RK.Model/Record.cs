using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RK.Model
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 类型:1,收入; 2,支出
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 记录
        /// </summary>
        public virtual RecordType RecordType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1000)]
        public string Remark { get; set; }
        /// <summary>
        /// 状态: -1,删除;1,正常;0,禁用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeletedTime { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

    }
}
