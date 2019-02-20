using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Back.IRespository;
using Exam.Back.Model;
using Exam.Back.Common;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using Dapper;
namespace Exam.Back.Respository.Test
{
    public class Testscores : IRespository.Test.ITestscores
    {
        static string strSql = ConfigurationManager.AppSettings["group"].ToString();
        public List<Resultsstatistical> ResultList(int Cid, string testdate)
        {
            using (IDbConnection conn = new MySqlConnection(strSql))
            {
                
                List<Resultsstatistical> gradeList = new List<Resultsstatistical>();
                string sqlTwo = $"select * from resultsstatistical where Cid=" + Cid;
                if (!string.IsNullOrEmpty(testdate.ToString()))
                {
                    sqlTwo += "  and TestDate between '" + Convert.ToDateTime(testdate).ToString("yyyy-MM-dd") + " 00:00:00' and '" + Convert.ToDateTime(testdate).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00'";
                }
                else
                {
                    sqlTwo += "  and TestDate between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' and '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00'";
                }
                return conn.Query<Resultsstatistical>(sqlTwo).ToList();
            }

        }
        public List<TestClass> TestList()
        {
            List<TestClass> TestList = DapperHelper.GetList<TestClass>("SELECT Id id,TextName name,Pid pid FROM TestClass");
            return TestList;
        }
    }
}
