using Exam.Back.IRespository.Setting;
using Exam.Back.Model;
using Exam.Back.Mvc.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Back.Mvc.Controllers
{
    public class ExamBackController : Controller
    {

        IUserRespository user;
        public ExamBackController(IUserRespository userRespository)
        {
            user = userRespository;
        }
        //[AuthFilter()]
        // GET: ExamBack
        public ActionResult Index(string accountname)
        {
            Users s = Session["user"] as Users;
            List<Permissions> list = user.ManyPermission(accountname);
            ViewBag.list0 = list.Where(m => m.PID == 0).ToList();
            ViewBag.list1 = list;
            return View(list);
        }

        public ActionResult Welcome()
        {
            return View();
        }




    }
}