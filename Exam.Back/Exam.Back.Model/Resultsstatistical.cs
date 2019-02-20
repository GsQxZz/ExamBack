using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
   public class Resultsstatistical
    {
        /// <summary>
        /// 主键
        /// </summary>   
        public int ResultsId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string ResultsName { get; set; }

        /// <summary>
        /// 学生分数
        /// </summary>
        public string ResultsScore { get; set; }

        /// <summary>
        /// 班级外键ID
        /// </summary>
        public int Cid { get; set; }

        /// <summary>
        /// 考试日期
        /// </summary>
        public string TestDate { get; set; }

        /// <summary>
        /// 考试类别
        /// </summary>
        public string Testcategories { get; set; }

    }
}
