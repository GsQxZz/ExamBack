using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Exam.Back.IRespository.Colleges;
using Exam.Back.IRespository.Log;
using Exam.Back.Model;
using Newtonsoft.Json;

namespace Exam.Back.Mvc.Controllers
{
    public class CollegeController : Controller
    {
        ICollegeRespository _iCollegeRespository;
        ILogRespository logs;
        public CollegeController(ICollegeRespository iCollegeRespository, ILogRespository log)
        {
            _iCollegeRespository = iCollegeRespository;
            logs = log;
        }

        // GET: College
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回学院视图
        /// </summary>
        /// <returns></returns>
        public ActionResult College()
        {
            return View();
        }

        /// <summary>
        /// 获取所有的学院信息
        /// </summary>
        /// <param name="CollegeName">学院名称,用于模糊查询学院</param>
        /// <param name="PageIndex">分页当前页码</param>
        /// <param name="PageSize">分页的每页条数</param>
        /// <returns>返回得到的json字符串</returns>
        [HttpGet]
        public string GetCollegeList(string CollegeName,int PageIndex,int PageSize)
        {
            Pageing<List<College>> collegeList = _iCollegeRespository.GetCollegeList(CollegeName,PageIndex,PageSize);
            return JsonConvert.SerializeObject(collegeList);
        }

        /// <summary>
        /// 执行增加学院操作
        /// </summary>
        /// <param name="college">学院实体类</param>
        /// <returns>返回受影响行数</returns>
        public int AddCollege(College college)
        {
            int i = _iCollegeRespository.AddCollege(college);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加学院", 1);
            }
            else
            {
                logs.Add(us.ID, "添加学院", 0);
            }
            return i;
        }

        /// <summary>
        /// 删除学院
        /// </summary>
        /// <param name="CollegeId">要删除的学院id</param>
        /// <returns>返回受影响行数</returns>
        public int DeleteCollege(string CollegeId)
        {
            int i = _iCollegeRespository.DeleteCollege(CollegeId);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "删除学院", 1);
            }
            else
            {
                logs.Add(us.ID, "删除学院", 0);
            }
            return i;
        }
    }
}