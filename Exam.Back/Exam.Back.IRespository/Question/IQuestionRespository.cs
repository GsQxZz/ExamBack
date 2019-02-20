using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Back.Model;

namespace Exam.Back.IRespository.Question
{
    public interface IQuestionRespository
    {
        /// <summary>
        /// 获取学院阶段树状信息
        /// </summary>
        /// <returns></returns>
        List<Ztree> GetTree();
        /// <summary>
        /// 根据单元获取题库信息
        /// </summary>
        /// <param name="Unitid"></param>
        /// <returns></returns>
        List<BackQuestionBank> GetQuestionById(int Unitid,string tp,string qn);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int Delete(string Id);
        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        int Update(BackQuestionBank q);
        /// <summary>
        /// 添加试题信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        int Add(BackQuestionBank b);
        /// <summary>
        /// 根据单元获取试题信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        List<BackQuestionBank> GetListByUnitId(int UnitId);
        /// <summary>
        /// 导入试题
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        int Import(List<BackQuestionBank> list);
    }
}
