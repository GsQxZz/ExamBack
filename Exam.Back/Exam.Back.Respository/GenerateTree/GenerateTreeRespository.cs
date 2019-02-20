using Dapper;
using Exam.Back.Common;
using Exam.Back.IRespository.GenerateTree;
using Exam.Back.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Respository.GenerateTree
{
    public class GenerateTreeRespository : IGenerateTreeRespository
    {
        static string strSql = ConfigurationManager.AppSettings["group"].ToString();
        /// <summary>
        /// 添加试题
        /// </summary>
        /// <param name="actionExams"></param>
        /// <returns></returns>
        public int Add(List<ActionExam> actionExams)
        {
            string str = string.Format("truncate table ActionExam");
            DapperHelper.ExecuteNonQuery(str);
            int result = 0;
            foreach (var item in actionExams)
            {
                result = DapperHelper.Insert<ActionExam>(item);
            }
            return result;
         }

        /// <summary>
        /// 获取试题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BackQuestionBank> backQuestionBanksDay(string id)
        {
            string str = string.Format("select Id id,Question question,QuestionType questionType,UnitId unitId,A a,B b,C c,D d,ChoiceAnswer choiceAnswer,JudgeAnswer judgeAnswer from backquestionbank where unitId in ({0})",id);
            List<BackQuestionBank> backQuestionBanks = DapperHelper.GetList<BackQuestionBank>(str);
            return backQuestionBanks;
        }
        /// <summary>
        /// 历史试卷添加
        /// </summary>
        /// <returns></returns>
        public int HistoryOnLineAdd(HistoryTest examOnLine)
        {
            string str = string.Format("INSERT INTO HistoryTest(ExamName,ExamTime,State,ExamType) VALUES('{0}','{1}',{2},'{3}')", examOnLine.ExamName, examOnLine.ExamTime, examOnLine.State, examOnLine.ExamType);
            return DapperHelper.ExecuteNonQuery(str);
        }

        /// <summary>
        /// 获取学院列表
        /// </summary>
        /// <returns></returns>
        public List<Ztree> Ztree()
        {
            List<Ztree> ztreeList = DapperHelper.GetList<Ztree>("select Id id,Tname name,Pid pid from ztree");
            return ztreeList;
        }

        public int SelectId(string name)
        {
            string str = string.Format("select Id from HistoryTest where ExamName = '{0}'",name);
            int id = DapperHelper.GetList<HistoryTest>(str).FirstOrDefault().Id;
            return id;
        }
    }
}
