using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
    /// <summary>
    /// 学院表
    /// </summary>
    public class College
    {
        /// <summary>
        /// 主建
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学院名称
        /// </summary>
        public string CollegeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime{get;set;}
        /// <summary>
        /// 简称
        /// </summary>
        public string Dean { get; set; }
    }
}
