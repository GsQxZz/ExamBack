using Exam.Back.IRespository.Log;
using Exam.Back.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Back.Mvc.Controllers
{
    public class JournalController : Controller
    {
        ILogRespository logs;
        public JournalController(ILogRespository log)
        {
            logs = log;
        }
        public ActionResult Show()
        {
            return View();
        }
        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="pageindex">第几页</param>
        /// <param name="pagesize">每页几条</param>
        /// <returns></returns>
        public string GetList(string UserName,int pageindex,int pagesize)
        {
            Pageing<List<Journal>> pa=logs.GetList(UserName, pageindex, pagesize);
            return Newtonsoft.Json.JsonConvert.SerializeObject(pa);
        }
        
    }
}