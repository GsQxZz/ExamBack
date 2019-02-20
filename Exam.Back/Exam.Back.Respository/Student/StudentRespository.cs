using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.IRespository.Student;
using Exam.Back.Model;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace Exam.Back.Respository
{
    public class StudentRespository : IStudentRespository
    {
        static string strSql = ConfigurationManager.AppSettings["group"].ToString();


        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="pupil">学生实体类</param>
        /// <returns>返回受影响行数</returns>
        public int AddPupil(Pupil pupil)
        {
            int i;
            string sqlTwo = $"insert into pupil(StudentName,StudentNumber,StudentPassWord,ClassId) values(@StudentName,@StudentNumber,@StudentPassWord,@ClassId)";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = connection.Execute(sqlTwo, new { StudentName = pupil.StudentName, StudentNumber = pupil.StudentNumber, StudentPassWord = pupil.StudentPassWord, ClassId = pupil.ClassId });
            }
            return i;
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id">学生编号</param>
        /// <returns>返回受影响行数</returns>
        public int DeletePupil(int id)
        {
            int i;
            string sqlTwo = $"delete from pupil where id =@id";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = connection.Execute(sqlTwo, new { id = id });
            }
            return i;
        }

        /// <summary>
        /// 根据阶段id得到对应的班级
        /// </summary>
        /// <param name="Id">阶段id</param>
        /// <returns>返回班级list集合</returns>
        public List<Grade> getGradeList(int Id)
        {
            try
            {
                List<Grade> gradeList = new List<Grade>();
                string sqlTwo = $"select id, ClassName from grade where 1=1";
                if (Id > 0)
                {
                    sqlTwo += "  and StageId=@id";
                }
                using (IDbConnection connection = new MySqlConnection(strSql))
                {
                    gradeList = connection.Query<Grade>(sqlTwo, new { id = Id, }).ToList();
                }
                return gradeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据id得到具体的学生信息
        /// </summary>
        /// <param name="PupilId">学生id</param>
        /// <returns>返回具体的学生信息</returns>
        public List<Pupil> GetPupilById(int PupilId)
        {
            List<Pupil> pupilList = new List<Pupil>();
            string sqlTwo = $"select studentName ,studentnumber,studentpassword,Classid from pupil where 1=1";
            if (PupilId > 0)
            {
                sqlTwo += "  and id=@PupilId";
            }
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                pupilList = connection.Query<Pupil>(sqlTwo, new { PupilId = PupilId }).ToList();
            }
            return pupilList;
        }

        /// <summary>
        /// 根据班级id得到对应的学生信息
        /// </summary>
        /// <param name="ClassId">关联班级的id</param>
        /// <returns>返回得到的学生信息</returns>
        public Pageing<List<Pupil>> GetPupilList(int ClassId, int PageIndex, int PageSize, string PupilName)
        {
            Pageing<List<Pupil>> pupilList = new Pageing<List<Pupil>>();
            int Index = (PageIndex - 1) * PageSize;
            string sqlOne = $"select id, studentName ,studentnumber,studentpassword from pupil where 1=1";
            string sqlTwo = $"select id, studentName ,studentnumber,studentpassword from pupil where 1=1";
            if (!string.IsNullOrEmpty(PupilName))
            {
                if (ClassId > 0)
                {
                    sqlTwo += "  and Classid=@ClassId and StudentName like concat('%',@PupilName,'%') limit @Index,@PageSize";
                    sqlOne += " and Classid=@ClassId and StudentName like concat('%',@PupilName,'%')";
                }
                else
                {
                    sqlTwo += " and StudentName like concat('%',@PupilName,'%') limit @Index,@PageSize";
                }
            }
            else
            {
                if (ClassId > 0)
                {
                    sqlTwo += "  and Classid=@ClassId  limit @Index,@PageSize";
                    sqlOne += " and Classid=@ClassId";
                }
                else
                {
                    sqlTwo += " limit @Index,@PageSize";
                }
            }

            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                pupilList.Data = connection.Query<Pupil>(sqlTwo, new { ClassId = ClassId, PupilName= PupilName, Index = Index, PageSize = PageSize }).ToList();
            }
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                pupilList.Count = connection.Query<Pupil>(sqlOne, new { ClassId = ClassId, PupilName = PupilName }).ToList().Count();
            }
            return pupilList;
        }

        /// <summary>
        /// 根据id获取阶段信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Stage> getStageList(int Id)
        {
            List<Stage> stageList = new List<Stage>();
            string sqlTwo = $"select id,stageName from stage where CollegeId =@Id and pid!=0";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                stageList = connection.Query<Stage>(sqlTwo,new { Id=Id}).ToList();
            }
            return stageList;
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="pupil">学生实体类</param>
        /// <returns>返回受影响行数</returns>
        public int UpdatePupil(Pupil pupil)
        {
            int i;
            string sqlTwo = $"update pupil set studentpassword=@StudentPassWord,Classid=@ClassId where id=@id";
            using (IDbConnection connection = new MySqlConnection(strSql))
            {
                i = connection.Execute(sqlTwo, pupil);
            }
            return i;
        }
    }
}
