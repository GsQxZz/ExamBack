using Exam.Back.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.IRespository.GenerateTree
{
    public interface IGenerateTreeRespository
    {
        /// <summary>
        /// 获取学院
        /// </summary>
        /// <returns></returns>
        List<Ztree> Ztree();

        /// <summary>
        /// 日考题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<BackQuestionBank> backQuestionBanksDay(string id);

        /// <summary>
        /// 添加日考题
        /// </summary>
        /// <returns></returns>
        int Add(List<ActionExam> actionExams);

        /// <summary>
        /// 历史试卷添加
        /// </summary>
        /// <returns></returns>
        int HistoryOnLineAdd(HistoryTest historyOnLine);


        /// <summary>
        /// 查询历史试卷Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int SelectId(string name);
    }
}
