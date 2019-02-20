using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
    /// <summary>
    /// 历史试卷
    /// </summary>
    public class HistoryTest
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamTime { get; set; }
        public int State { get; set; }
        public string ExamType { get; set; }
    }
}
