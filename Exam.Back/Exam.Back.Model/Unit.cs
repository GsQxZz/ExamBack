using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
    /// <summary>
    /// 单元表
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// 单元Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 单元名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 课程外键
        /// </summary>
        public int SourceId { get; set; }
    }
}
