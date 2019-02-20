using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Dapper;
using System.Web.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Exam.Back.Mvc.Filter
{
    public class HandleErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
               
                var conn = ConfigurationManager.AppSettings["group"].ToString();
                string msg = filterContext.Exception.Message.ToString();
                using (IDbConnection con = new MySqlConnection(conn))
                {
                    string sqlStr = string.Format("insert into ErrorLog (`Name`,CreateTime) values ('{0}','{1}')",msg,DateTime.Now);
                    int result = con.Execute(sqlStr);
                }
                filterContext.HttpContext.Response.Write(msg);
            }
        }
    }
}