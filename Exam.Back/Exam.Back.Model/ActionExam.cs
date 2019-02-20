using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
    public class ActionExam
    {
        /// <summary>
        /// ///题库Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// //问题
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public int QuestionType { get; set; }
        /// <summary>
        /// 选项A
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// 选项B
        /// </summary>
        public string B { get; set; }
        /// <summary>
        /// 选项C
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// 选项D
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// 选择提答案
        /// </summary>
        public string ChoiceAnswer { get; set; }
        /// <summary>
        /// 判断题答案
        /// </summary>
        public string JudgeAnswer { get; set; }
        /// <summary>
        /// 单元外键
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 历史试卷外键
        /// </summary>
        public int TestId { get; set; }
    }
}
