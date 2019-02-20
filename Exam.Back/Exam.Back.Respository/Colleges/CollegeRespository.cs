using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.IRespository.Colleges;
using Exam.Back.Model;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using Exam.Back.Common;

namespace Exam.Back.Respository.Colleges
{
    public class CollegeRespository : ICollegeRespository
    {
        //获取连接mysql数据库的连接字符串
        static string strSql = ConfigurationManager.AppSettings["group"].ToString();

        /// <summary>
        /// 添加学院
        /// </summary>
        /// <param name="college">学院实体类</param>
        /// <returns>返回受影响行数</returns>
        public int AddCollege(College college)
        {
            int i;
            string sql = "insert into college(CollegeName,CreateTime,Dean) values(@CollegeName,@CreateTime,@Dean) ";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = connection.Execute(sql, college);
            }
            return i;
        }

        /// <summary>
        /// 获取所有的学院信息
        /// </summary>
        /// <param name="CollegeName">学院名称,用于模糊查询学院</param>
        /// <param name="PageIndex">分页当前页码</param>
        /// <param name="PageSize">分页的每页条数</param>
        /// <returns>返回得到的json字符串</returns>
        public Pageing<List<College>> GetCollegeList(string CollegeName, int PageIndex, int PageSize)
        {
            Pageing<List<College>> collegeList = new Pageing<List<College>>();
            int Index = (PageIndex - 1) * PageSize;
            string sqlTwo = $"select id,collegename ,createtime,dean from college where 1=1";
            string sqlone = $"select id,collegename ,createtime,dean from college where 1=1";
            if (!string.IsNullOrEmpty(CollegeName))
            {
                sqlTwo += "  and collegename like concat(@CollegeName,'%') limit @Index,@PageSize";
                sqlone += " and collegename like concat(@CollegeName,'%')";
            }
            else
            {
                sqlTwo += " limit @Index,@PageSize";
            }
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                collegeList.Data = connection.Query<College>(sqlTwo, new { CollegeName = CollegeName,Index=Index,PageSize=PageSize }).ToList();
            }
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                collegeList.Count = connection.Query<College>(sqlone, new { CollegeName = CollegeName}).ToList().Count();
            }
            return collegeList;
        }

        /// <summary>
        /// 删除学院
        /// </summary>
        /// <param name="CollegeId">要删除的学院id</param>
        /// <returns>返回受影响行数</returns>
        public int DeleteCollege(string CollegeId)
        {
            int i;
            string sql = $"delete from college where id in ({CollegeId})";
            //string sql = "delete from college where CHARINDEX(id, @CollegeId) > 0";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = connection.Execute(sql);
            }
            return i;
        }
    }
}
