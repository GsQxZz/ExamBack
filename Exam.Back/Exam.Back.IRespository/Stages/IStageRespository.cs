using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exam.Back.Model;

namespace Exam.Back.IRespository.Stages
{
    public interface IStageRespository
    {
        /// <summary>
        /// 得到阶段的所有信息
        /// </summary>
        /// <returns>返回所有的阶段集合</returns>
        List<StagesZtree> getStageList(int collegeId);


        /// <summary>
        /// 返回对应的班级信息
        /// </summary>
        /// <param name="stageId">对应阶段的id</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">每页显示的条数</param>
        /// <returns>返回查询到的班级集合</returns>
        Pageing<List<Grade>> getGradeList(int stageId,int PageIndex,int PageSize);

        /// <summary>
        /// 将对应班级升阶段
        /// </summary>
        /// <param name="StageId">阶段的id值</param>
        /// <returns>返回受影响行数</returns>
        int UpGradeOne(int StageId);

        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="grade">班级实体类</param>
        /// <returns>返回受影响行数</returns>
        int AddClass(Grade grade);

        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="Id">传递的id主键值</param>
        /// <returns>返回受影响行数</returns>
        int DeleteClass(int Id);

        /// <summary>
        /// 删除阶段
        /// </summary>
        /// <param name="StageId"></param>
        /// <returns></returns>
        int DeleteStage(int StageId);

        /// <summary>
        /// 添加阶段
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        int AddStage(Stage stage);
    }
}
