using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Exam.Back.IRespository.Student;
using Exam.Back.Model;
using Newtonsoft.Json;
using Exam.Back.Common;
using Exam.Back.IRespository.Log;

namespace Exam.Back.Mvc.Controllers
{
    public class PupilController : Controller
    {
        IStudentRespository StudentRespository;
        ILogRespository logs;
        public PupilController(IStudentRespository studentRespository, ILogRespository log)
        {
            StudentRespository = studentRespository;
            logs = log;
        }


        // GET: Pupil
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回学生视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Pupil()
        {
            return View();
        }

        /// <summary>
        /// 返回弹框显示学生信息
        /// </summary>
        /// <returns></returns>
        public ActionResult LayerPupil()
        {
            return View();
        }

        /// <summary>
        /// 添加学生模态窗
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPupil()
        {
            return View();
        }

        /// <summary>
        /// 修改学生信息模态窗
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePupil()
        {
            return View();
        }

        /// <summary>
        /// 得到阶段
        /// </summary>
        /// <returns>返回阶段的json字符串</returns>
        [HttpGet]
        public string GetStageList(int CollegeId)
        {
            List<Stage> stageList;
          //  string key = "GetStageList";
          //  stageList= RedisHelper.Get<List<Stage>>(key);
           // if (stageList == null)
           // {
                stageList = StudentRespository.getStageList(CollegeId);
               // RedisHelper.Set<List<Stage>>(key, stageList);
            //}
            return JsonConvert.SerializeObject(stageList);
        }

        /// <summary>
        /// 得到班级list
        /// </summary>
        /// <param name="StageId">对应的阶段id</param>
        /// <returns>返回班级list转的json字符串</returns>
        [HttpGet]
        public string GetGradeList(int StageId)
        {
            List<Grade> gradeList = StudentRespository.getGradeList(StageId);
            return JsonConvert.SerializeObject(gradeList);
        }

        /// <summary>
        /// 得到对应班级所有的学生
        /// </summary>
        /// <returns></returns>
        public string GetPupilList(int GradeId, int PageIndex, int PageSize,string PupilName)
        {
           Pageing<List<Pupil>> pupilList = StudentRespository.GetPupilList(GradeId,PageIndex,PageSize,PupilName);
            return JsonConvert.SerializeObject(pupilList);
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="PupilId">学生id</param>
        /// <returns>返回受影响行数</returns>
        public int DeletePupil(int PupilId)
        {
            int i = StudentRespository.DeletePupil(PupilId);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "删除学生", 1);
            }
            else
            {
                logs.Add(us.ID, "删除学生", 0);
            }
            return i;
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="pupil">学生实体类</param>
        /// <returns>返回受影响行数</returns>
        public int AddPupilNoView(Pupil pupil)
        {
            int i = StudentRespository.AddPupil(pupil);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "添加学生", 1);
            }
            else
            {
                logs.Add(us.ID, "添加学生", 0);
            }
            return i;
        }

        /// <summary>
        /// 根据id查询具体的学生信息
        /// </summary>
        /// <param name="PupilId">学生id</param>
        /// <returns>返回具体学生json字符串信息</returns>
        [HttpGet]
        public string GetPupilById(int PupilId)
        {
            List<Pupil> pupilList = StudentRespository.GetPupilById(PupilId);
            return JsonConvert.SerializeObject(pupilList);
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="pupil">学生信息实体类</param>
        /// <returns>返回受影响行数</returns>
        [HttpPost]
        public int UpdatePupilNoView(Pupil pupil)
        {
            int i = StudentRespository.UpdatePupil(pupil);
            Users us = (Users)Session["User"];
            if (i > 0)
            {
                logs.Add(us.ID, "修改学生信息", 1);
            }
            else
            {
                logs.Add(us.ID, "修改学生信息", 0);
            }
            return i;
        }
    }
}