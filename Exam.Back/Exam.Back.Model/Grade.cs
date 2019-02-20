using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
    /// <summary>
    /// 班级
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// 主建
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 阶段名称     
        /// </summary>
        public int StageId { get; set; }
    }
}
