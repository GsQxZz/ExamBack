using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
   public class Pupil
    {
        /// <summary>
        /// 学生编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 学生学号
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string StudentPassWord { get; set; }

        /// <summary>
        /// 对应班级关联字段
        /// </summary>
        public int ClassId { get; set; }
    }
}
