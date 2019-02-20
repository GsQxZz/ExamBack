using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Exam.Back.IRespository.Stages;
using Exam.Back.Model;

namespace Exam.Back.Api.Controllers
{
    public class StageController : ApiController
    {
        /// <summary>
        /// 构造函数注入
        /// </summary>
        IStageRespository iStageRespository;
        public StageController(IStageRespository istageRespository)
        {
            iStageRespository = istageRespository;
        }

        /// <summary>
        /// 得到所有的阶段信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<StagesZtree> GetStageList()
        {
            List<StagesZtree> stageList = iStageRespository.getStageList();
            return stageList;
        }
    }
}
