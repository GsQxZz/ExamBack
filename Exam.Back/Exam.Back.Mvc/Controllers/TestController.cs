using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Back.Model;

using Exam.Back.IRespository.Test;
using Newtonsoft.Json;
using Exam.Back.IRespository.Stages;

namespace Exam.Back.Mvc.Controllers
{
    public class TestController : Controller
    {
        ITestscores itestcores;
        public TestController(ITestscores itestcoress)
        {
            itestcores = itestcoress;
        }
        // GET: Test
        public ActionResult ResultIndex(string testdate)
        {
            return View(); 
        }
        public string TestList(int ClassId,string time)
        {
            List<Resultsstatistical> list = itestcores.ResultList(ClassId, time);
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        public string ShowList()
        {
            List<TestClass> list = itestcores.TestList(); 
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}