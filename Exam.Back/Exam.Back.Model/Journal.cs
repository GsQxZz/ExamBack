using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
    public class Journal
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Operate { get; set; }
        /// <summary>
        /// 操作结果
        /// </summary>
        public int OpreateResult { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }




        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }
    }
}
