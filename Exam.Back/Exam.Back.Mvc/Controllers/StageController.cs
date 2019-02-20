using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Back.IRespository.Log;
using Exam.Back.IRespository.Stages;
using Exam.Back.Model;
using Newtonsoft.Json;

namespace Exam.Back.Mvc.Controllers
{
    public class StageController : Controller
    {
        IStageRespository _iStageRespository;
        ILogRespository logs;
        public StageController(IStageRespository iStageRespository, ILogRespository log)
        {
            _iStageRespository = iStageRespository;
            logs = log;
        }

        // GET: Stage
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 显示阶段树状图以及点击显示的班级信息
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult GetStages()
        {
            return View();
        }

        /// <summary>
        /// 返回zTree绑定需要的json字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetStageList(int CollegeId)
        {
            List<StagesZtree> stagesZtreeList = _iStageRespository.getStageList(CollegeId);
            string str = JsonConvert.SerializeObject(stagesZtreeList);
            return str;
        }

        /// <summary>
        /// 根据树的点击事件返回对应的班级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetGradeList(int stageId,int PageIndex,int PageSize)
        {
           Pageing<List<Grade>> gradeList = _iStageRespository.getGradeList(stageId,PageIndex,PageSize);
            string gradeStr = JsonConvert.SerializeObject(gradeList);
            return gradeStr;
        }

        /// <summary>
        /// 修改班级阶段
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpGradeOne(int gradeId)
        {
            int i = _iStageRespository.UpGradeOne(gradeId);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "修改班级信息", 1);
            }
            else
            {
                logs.Add(us.ID, "修改班级信息", 0);
            }
            return i;
        }

        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="grade">班级实体类</param>
        /// <returns>返回受影响行数</returns>
        [HttpPost]
        public int AddClass(Grade grade)
        {
            int i = _iStageRespository.AddClass(grade);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加班级", 1);
            }
            else
            {
                logs.Add(us.ID, "添加班级", 0);
            }
            return i;
        }

        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public int DeleteClass(int Id)
        {
            int i = _iStageRespository.DeleteClass(Id);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "删除班级", 1);
            }
            else
            {
                logs.Add(us.ID, "删除班级", 0);
            }
            return i;
        }

        /// <summary>
        /// 删除阶段
        /// </summary>
        /// <param name="StageId"></param>
        /// <returns></returns>
        [HttpPost]
        public int DeleteStage(int StageId)
        {
            int i = _iStageRespository.DeleteStage(StageId);
            return i;
        }

        /// <summary>
        /// 添加节点操作
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public int AddStage(Stage stage)
        {
            int i = _iStageRespository.AddStage(stage);
            return i;
        }
    }
}