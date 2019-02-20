using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.Model;

namespace Exam.Back.IRespository.Colleges
{
   public interface ICollegeRespository
    {
        /// <summary>
        /// 获取所有的学院信息
        /// </summary>
        /// <param name="CollegeName">学院名称,用于模糊查询学院</param>
        /// <param name="PageIndex">分页当前页码</param>
        /// <param name="PageSize">分页的每页条数</param>
        /// <returns>返回得到的json字符串</returns>
        Pageing<List<College>> GetCollegeList(string CollegeName, int PageIndex, int PageSize);

        /// <summary>
        /// 执行增加学院操作
        /// </summary>
        /// <param name="college">学院实体类</param>
        /// <returns></returns>
        int AddCollege(College college);

        /// <summary>
        /// 删除学院
        /// </summary>
        /// <param name="CollegeId">要删除的学院id</param>
        /// <returns>返回受影响行数</returns>
        int DeleteCollege(string CollegeId);
    }
}
