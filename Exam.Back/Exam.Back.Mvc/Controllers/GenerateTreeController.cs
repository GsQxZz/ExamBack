using Exam.Back.IRespository.GenerateTree;
using Exam.Back.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;
using System.Data;

namespace Exam.Back.Mvc.Controllers
{
    public class GenerateTreeController : Controller
    {
        IGenerateTreeRespository _iGenerateTreeRespository;
        public GenerateTreeController(IGenerateTreeRespository iGenerateRespository)
        {
            _iGenerateTreeRespository = iGenerateRespository;
        }

        /// <summary>
        /// 获取树状图以及点击生成试题
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateTree()
        {
            return View();
        }

        /// <summary>
        /// 获取阶段列表
        /// </summary>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetTreeList()
        {
            List<Ztree> stages = _iGenerateTreeRespository.Ztree();
            return JsonConvert.SerializeObject(stages);
        }
        /// <summary>
        /// 获取试题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public int backquestionDay(string id,string ExamType,string sessionStorage)
        {
            //返回单元列表
            List<BackQuestionBank> backQuestionBanks = _iGenerateTreeRespository.backQuestionBanksDay(id);
            Hashtable hashtable = new Hashtable();//定义哈希表
            List<BackQuestionBank> dayExamList = new List<BackQuestionBank>();//日考题列表
            List<BackQuestionBank> radioExamList = new List<BackQuestionBank>();//单选题
            List<BackQuestionBank> checkExamList = new List<BackQuestionBank>();//多选题
            List<BackQuestionBank> judgeExamList = new List<BackQuestionBank>();//判断题
            
            foreach (var item in backQuestionBanks)//三种类型题循环放入集合
            {
                if(item.QuestionType == 1)
                {
                    radioExamList.Add(item);
                }
                if(item.QuestionType == 2)
                {
                    checkExamList.Add(item);
                }
                if(item.QuestionType == 3)
                {
                    judgeExamList.Add(item);
                }
            }
            //循环获得判断题10道题
            foreach (var item in judgeExamList)
            {
                Random random = new Random();
                while (true)
                {
                    int num = random.Next(0, 10);
                    if (!hashtable.ContainsKey(num))
                    {
                        hashtable.Add(num, item);
                        break;
                    }
                }
                if (hashtable.Count >= 10)
                {
                    break;
                }
            }
            //循环获得多选题10道题
            foreach (var item in checkExamList)
            {
                Random random = new Random();
                while (true)
                {
                    int num = random.Next(10,20);
                    if (!hashtable.ContainsKey(num))
                    {
                        hashtable.Add(num,item);
                        break;
                    }
                }
                if (hashtable.Count >= 20)
                {
                    break;
                }
            }
            //循环获得单选题20道题
            foreach (var item in radioExamList)
            {
                Random random = new Random();
                while (true)
                {
                    int num = random.Next(20, 40);
                    if (!hashtable.ContainsKey(num))
                    {
                        hashtable.Add(num, item);
                        break;
                    }
                }
                if (hashtable.Count >= 40)
                {
                    break;
                }
            }

            if (hashtable.Count < 40)
            {
                return -1;
            }

            //循环从哈希表放入集合
            foreach (DictionaryEntry item in hashtable)
            {
                dayExamList.Add((BackQuestionBank)item.Value);
            }
            

            HistoryTest historyOnLine = new HistoryTest();
            historyOnLine.ExamName = sessionStorage;
            historyOnLine.ExamTime = DateTime.Now;
            historyOnLine.State = 0;
            historyOnLine.ExamType = ExamType;
            var finish = _iGenerateTreeRespository.HistoryOnLineAdd(historyOnLine);
            var testid = _iGenerateTreeRespository.SelectId(historyOnLine.ExamName);


            List<ActionExam> action = new List<ActionExam>();
            foreach (var item in dayExamList)
            {
                ActionExam actionExam = new ActionExam();
                actionExam.Question = item.Question;
                actionExam.QuestionType = item.QuestionType;
                actionExam.A = item.A;
                actionExam.B = item.B;
                actionExam.C = item.C;
                actionExam.D = item.D;
                actionExam.JudgeAnswer = item.JudgeAnswer;
                actionExam.ChoiceAnswer = item.ChoiceAnswer;
                actionExam.UnitId = item.UnitId;
                actionExam.TestId = testid;
                action.Add(actionExam);
            }
            int result = _iGenerateTreeRespository.Add(action);
            
            
            return result;
        }
    }
}
