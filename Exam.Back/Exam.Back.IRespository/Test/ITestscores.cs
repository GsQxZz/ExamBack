using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Back.Model;
namespace Exam.Back.IRespository.Test
{
   public interface ITestscores
    {
        /// <summary>
        /// 获取每名学生的成绩
        /// </summary>
        /// <returns></returns>
        List<Resultsstatistical> ResultList(int Cid,string testdate);

        /// <summary>
        /// 阶段班级
        /// </summary>
        /// <returns></returns>
        List<TestClass> TestList();
    }
}
