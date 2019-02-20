using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
   public class Stage
    {
        /// <summary>
        /// 阶段编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        public string StageName { get; set; }

        /// <summary>
        /// 关于学院表的关联字段
        /// </summary>
        public int CollegeId { get; set; }

        /// <summary>
        /// 自关联的外键
        /// </summary>
        public int PId { get; set; }
    }
    public class StagesZtree
    {
        /// <summary>
        /// 阶段编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 自关联的外键
        /// </summary>
        public int pId { get; set; }
    }
}
