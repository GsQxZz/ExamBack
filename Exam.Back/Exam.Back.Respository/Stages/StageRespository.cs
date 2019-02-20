using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.IRespository.Stages;
using Exam.Back.Model;
using Exam.Back.Common;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace Exam.Back.Respository.Stages
{
    public class StageRespository : IStageRespository
    {
        static string strSql = ConfigurationManager.AppSettings["group"].ToString();

        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int AddClass(Grade grade)
        {
            int j;
            string sqlTwo = $"insert into grade(classname,stageid) values(@ClassName,@StageId)";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                j = Convert.ToInt32(connection.Execute(sqlTwo, new { ClassName = grade.ClassName, StageId = grade.StageId }));
            }
            return j;
        }

        /// <summary>
        /// 删除班级数据
        /// </summary>
        /// <param name="Id">条件id</param>
        /// <returns>返回受影响行数</returns>
        public int DeleteClass(int Id)
        {
            int j;
            string sqlTwo = $"delete from grade where id =@Id";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                j = Convert.ToInt32(connection.Execute(sqlTwo, new { Id=Id }));
            }
            return j;
        }

        /// <summary>
        /// 得到对应的班级信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pageing<List<Grade>> getGradeList(int stageId, int PageIndex, int PageSize)
        {
            Pageing<List<Grade>> gradePageing = new Pageing<List<Grade>>();
            List<Grade> gradeList = new List<Grade>();
            int Index = (PageIndex - 1) * PageSize;
            string sql = $"SELECT Id,ClassName from grade where stageid=@stageId limit @Index,@PageSize";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                 gradeList= connection.Query<Grade>(sql,new { stageId=stageId, Index=Index,PageSize=PageSize }).ToList();
            }
            gradePageing.Data = gradeList;
            gradePageing.Count = gradeList.Count;
            return gradePageing;
        }

        /// <summary>
        /// 得到所有的阶段信息
        /// </summary>
        /// <returns></returns>
        public List<StagesZtree> getStageList(int collegeId)
        {

            string sql = "SELECT id,stageName name, Pid pId from stage where CollegeId =@CollegeId";
            List<StagesZtree> stageList;
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
              stageList = connection.Query<StagesZtree>(sql, new { CollegeId = collegeId }).ToList();
            }

          
            return stageList;
        }

        /// <summary>
        /// 对应班级升阶段
        /// </summary>
        /// <param name="StageId"></param>
        /// <returns>返回</returns>
        public int UpGradeOne(int gradeId)
        {
            int i;
            string sqlOne = $"select stageid from grade where id=@gradeId";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
              i= Convert.ToInt32( connection.ExecuteScalar(sqlOne, new { gradeId = gradeId }));
            }
            i--;
            int j;
            string sqlTwo = $"update grade set stageid=@stageid where id =@id";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                j = Convert.ToInt32(connection.Execute(sqlTwo, new { stageid = i, id = gradeId }));
            }
            return j;
        }

        /// <summary>
        /// 删除阶段
        /// </summary>
        /// <param name="StageId"></param>
        /// <returns></returns>
        public int DeleteStage(int StageId)
        {
            int i;
            string sqlOne = $"delete from stage where id=@StageId";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = Convert.ToInt32(connection.Execute(sqlOne, new { StageId = StageId }));
            }
            return i;
        }

        /// <summary>
        /// 添加阶段
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public int AddStage(Stage stage)
        {
            int i;
            string sqlOne = $"insert into  stage(stagename,collegeid,pid) values(@StageName,@CollegeId,@PId)";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = Convert.ToInt32(connection.Execute(sqlOne,stage));
            }
            return i;
        }
    }
}
