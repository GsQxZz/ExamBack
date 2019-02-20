using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.Model;

namespace Exam.Back.IRespository.Student
{
   public interface IStudentRespository
    {
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="pupil">学生实体类</param>
        /// <returns>返回受影响行数</returns>
        int AddPupil(Pupil pupil);

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id">学生编号</param>
        /// <returns>返回手影响行数</returns>
        int DeletePupil(int id);

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="pupil">学生实体类</param>
        /// <returns>返回受影响行数</returns>
        int UpdatePupil(Pupil pupil);

        /// <summary>
        /// 根据班级id得到对应的学生信息
        /// </summary>
        /// <param name="ClassId">关联班级的id</param>
        /// <returns>返回得到的学生信息</returns>
        Pageing<List<Pupil>> GetPupilList(int ClassId,int PageIndex,int PageSize,string PupilName);

        /// <summary>
        /// 根据id得到阶段
        /// </summary>
        /// <param name="Id">学院id</param>
        /// <returns></returns>
        List<Stage> getStageList(int Id);

        /// <summary>
        /// 根据id得到班级
        /// </summary>
        /// <param name="Id">阶段id</param>
        /// <returns>返回班级集合</returns>
        List<Grade> getGradeList(int Id);

        /// <summary>
        /// 得到学生信息用于反填
        /// </summary>
        /// <param name="PupilId">学生id</param>
        /// <returns>返回具体的学生信息</returns>
       List<Pupil> GetPupilById(int PupilId);
    }
}
