using Exam.Back.IRespository.Question;
using Exam.Back.Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Http;
using NPOI.HSSF.UserModel;
using System.Data;
using Exam.Back.IRespository.Log;

namespace Exam.Back.Mvc.Controllers
{
    public class QuestionBankController : Controller
    {
        IQuestionRespository question;
        ILogRespository logs;
        public QuestionBankController(IQuestionRespository questionRespository,ILogRespository log)
        {
            question = questionRespository;
            logs = log;
        }
        // GET: QuestionBank
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Show()
        {
            return View();
        }
        //获取学院、阶段、年级、单元信息
        public string GetZtree()
        {
            List<Ztree> list = question.GetTree();
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        /// <summary>
        /// 根据单元获取题库信息
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public string GetQuestion(int unitid, string tp, string qn)
        {
            List<BackQuestionBank> list = question.GetQuestionById(unitid, tp, qn);
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            var result = question.Delete(id);
            Users us = (Users)Session["User"];
            if (result > 0)
            {
                logs.Add(us.ID, "删除试题", 1);
            }
            else
            {
                logs.Add(us.ID, "删除试题", 0);
            }
            return result;
        }
        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="ques"></param>
        /// <returns></returns>
        public int Update(BackQuestionBank ques)
        {
            var result = question.Update(ques);
            Users us = (Users)Session["User"];
            if (result > 0)
            {
                logs.Add(us.ID, "修改试题", 1);
            }
            else
            {
                logs.Add(us.ID, "修改试题", 0);
            }
                return result;
        }
        [HttpPost]
        /// <summary>
        /// 添加试题信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Add()
        {
            BackQuestionBank b = new BackQuestionBank();
            b.Question = Request["Question"];
            b.QuestionType = Convert.ToInt32(Request["QuestionType"]);
            b.UnitId = Convert.ToInt32(Request["UnitId"]);
            b.A = Request["A"];
            b.B = Request["B"];
            b.C = Request["C"];
            b.D = Request["D"];
            b.ChoiceAnswer = Request["ChoiceAnswer"];
            b.JudgeAnswer = Request["JudgeAnswer"];
            var result = question.Add(b);
            Users us = (Users)Session["User"];
            if (result > 0)
            {
                logs.Add(us.ID, "添加试题", 1);
            }
            else
            {
                logs.Add(us.ID, "添加试题", 0);
            }
            return result;
        }
        /// <summary>
        /// 导出试题信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public int QuestionExport(int UnitId)
        {
            List<BackQuestionBank> list = question.GetListByUnitId(UnitId);
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("QuestionBank");
            var type = typeof(BackQuestionBank);
            IRow row = sheet.CreateRow(0);
            int column = 0;
            int rows = 1;
            foreach (var item in type.GetProperties())
            {
                row.CreateCell(column).SetCellValue(item.Name);
                ++column;
            }
            foreach (var item in list)
            {
                row = sheet.CreateRow(rows);
                row.CreateCell(0).SetCellValue(rows);
                row.CreateCell(1).SetCellValue(item.Question);
                row.CreateCell(2).SetCellValue(item.QuestionType);
                row.CreateCell(3).SetCellValue(item.UnitId);
                row.CreateCell(4).SetCellValue(item.A);
                row.CreateCell(5).SetCellValue(item.B);
                row.CreateCell(6).SetCellValue(item.C);
                row.CreateCell(7).SetCellValue(item.D);
                row.CreateCell(8).SetCellValue(item.ChoiceAnswer);
                row.CreateCell(9).SetCellValue(item.JudgeAnswer);
                ++rows;
            }
            string path = Server.MapPath("/QuestionBankExprot/");
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";
            using (var fs = System.IO.File.OpenWrite(path + filename))
            {
                workbook.Write(fs);
            }
            Users us = (Users)Session["User"];
            if (list.Count == (rows - 1))
            {
                logs.Add(us.ID, "导出试题", 1);
            }
            else
            {
                logs.Add(us.ID, "导出试题", 0);
            }
            return rows;
        }
        /// <summary>
        /// 导入试题
        /// </summary>
        /// <returns></returns>
        public string Improt()
        {
            var file = Request.Files["files"];
            string path = Server.MapPath("/QuestionImport/");
            file.SaveAs(path + file.FileName);

            DataTable dt = new DataTable();
            IWorkbook workbook;
            string xls = Path.GetExtension(path + file.FileName);

            if (xls == ".xlsx")
            {
                workbook = new XSSFWorkbook(file.InputStream);
            }
            else
            {
                workbook = new HSSFWorkbook(file.InputStream);
            }
            if (workbook == null)
            {
                return "";
            }
            ISheet sheet = workbook.GetSheetAt(0);
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            for (int i = 0; i < header.LastCellNum; i++)
            {
                DataColumn col = new DataColumn(header.GetCell(i).ToString());
                dt.Columns.Add(col);
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dr = dt.NewRow();
                for (int j = header.FirstCellNum; j < header.LastCellNum; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        dr[j] = row.GetCell(j).ToString();
                    }
                    else
                    {
                        dr[j] = "";
                    }
                }
                dt.Rows.Add(dr);
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            List<BackQuestionBank> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BackQuestionBank>>(json);
            foreach (var item in list)
            {
                item.UnitId = Convert.ToInt32(Request["UnitId"]);
            }
            int succ = question.Import(list);
            int lose = list.Count - succ;
            List<int> count = new List<int>() {
                succ,lose
            };
            Users us = (Users)Session["User"];
            if (succ > 0)
            {
                logs.Add(us.ID, "导入试题", 1);
            }
            else
            {
                logs.Add(us.ID, "导入试题", 0);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(count);
        }
    }
}