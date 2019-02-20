using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.IRespository.Question;
using Exam.Back.Model;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using Dapper;
using System.ComponentModel.DataAnnotations;

namespace Exam.Back.Respository.Question
{
    public class QuestionRespository : IQuestionRespository
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string mysqlConnectionStr = ConfigurationManager.ConnectionStrings["group5"].ToString();
        /// <summary>
        /// 添加试题信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Add(BackQuestionBank b)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = string.Format("insert into backquestionbank(Question,QuestionType,UnitId,A,B,C,D,ChoiceAnswer,JudgeAnswer) values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}')", b.Question, b.QuestionType, b.UnitId, b.A, b.B, b.C, b.D, b.ChoiceAnswer, b.JudgeAnswer);
                return conn.Execute(sql);
            }
        }

        /// <summary>
        /// 删除（单个删除）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(string Id)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = string.Format("delete from BackQuestionBank where Id in ({0})", Id);
                return conn.Execute(sql);
            }
        }
        /// <summary>
        /// 根据单元获取试题信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public List<BackQuestionBank> GetListByUnitId(int UnitId)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = "select * from backquestionbank where unitid=" + UnitId;
                return conn.Query<BackQuestionBank>(sql).ToList();
            }
        }
        public void AddDict(int UnitId)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                dict.Clear();
                string sql = "select * from backquestionbank where unitid=" + UnitId;
                List<BackQuestionBank> list = conn.Query<BackQuestionBank>(sql).ToList();
                foreach (var item in list)
                {
                    if (!dict.ContainsKey(item.Question))
                    {
                        dict.Add(item.Question, item.QuestionType.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 根据单元获取题库信息
        /// </summary>
        /// <param name="Unitid">单元</param>
        /// <param name="tp">题目类型</param>
        /// <param name="qn">问题</param>
        /// <returns></returns>
        public List<BackQuestionBank> GetQuestionById(int Unitid, string tp, string qn)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = "select * from BackQuestionBank where UnitId=" + Unitid;
                if (!(string.IsNullOrEmpty(tp)))
                {
                    if (Convert.ToInt32(tp) > 0)
                    {
                        var questiontype = Convert.ToInt32(tp);
                        sql += " and QuestionType=" + questiontype;
                    }
                }
                if (!(string.IsNullOrEmpty(qn)))
                {
                    sql += " and Question like '%" + qn + "%'";
                }
                return conn.Query<BackQuestionBank>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取学院阶段信息
        /// </summary>
        /// <returns></returns>
        public List<Ztree> GetTree()
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = "select Id id,Tname name,Pid pid from Ztree";
                return conn.Query<Ztree>(sql).ToList();
            }
        }
        /// <summary>
        /// 导入试题
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Import(List<BackQuestionBank> list)
        {
            AddDict(list[0].UnitId);
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                int a = 0;
                foreach (var item in list)
                {
                    if (!dict.ContainsKey(item.Question))
                    {
                        string sql = string.Format("insert into BackQuestionBank(Question,QuestionType,UnitId,A,B,C,D,ChoiceAnswer,JudgeAnswer) values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}')", item.Question, item.QuestionType, item.UnitId, item.A, item.B, item.C, item.D, item.ChoiceAnswer, item.JudgeAnswer);
                                            a = a + conn.Execute(sql);
                    }
                    
                }
                return a;
            }
        }

        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public int Update(BackQuestionBank q)
        {
            using (IDbConnection conn = new MySqlConnection(mysqlConnectionStr))
            {
                string sql = string.Format("update BackQuestionBank set Question='{0}',QuestionType={1},A='{2}',B='{3}',C='{4}',D='{5}',ChoiceAnswer='{6}',JudgeAnswer='{7}' where Id={8}", q.Question, q.QuestionType, q.A, q.B, q.C, q.D, q.ChoiceAnswer, q.JudgeAnswer, q.Id);
                return conn.Execute(sql);
            }
        }
    }
}
