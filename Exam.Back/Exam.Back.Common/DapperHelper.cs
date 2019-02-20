// <copyright file="DapperHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Exam.Back.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using MySql.Data.MySqlClient;

    // Web.config配置链接：<add name="testDB" connectionString="server=10.10.10.10; database=testDB;uid=test;pwd=test;charset=utf8;port=3306" />
    public class DapperHelper
    {
        /// <summary>
        /// 连接MySQL数据库
        /// </summary>
        /// <returns>返回连接</returns>
        public static MySqlConnection MySqlCon()
        {
            // string mysqlConnectionStr = ConfigurationManager.ConnectionStrings["group5"].ToString();
            string mysqlConnectionStr = ConfigurationManager.AppSettings["group"].ToString();
            var connection = new MySqlConnection(mysqlConnectionStr);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 执行增、删、改
        /// </summary>
        /// <param name="sqlStr">字符串</param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string sqlStr)
        {
            using (IDbConnection conn = DapperHelper.MySqlCon())
            {
                return conn.Execute(sqlStr);
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql">执行的字符串</param>
        /// <returns>返回首行首列</returns>
        public static int Executescalar(string sql)
        {
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    return Convert.ToInt32(conn.ExecuteScalar(sql));
                }
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 单个添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">具体的model类</param>
        /// <returns>返回受影响行数</returns>
        public static int Insert<T>(T t)
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "insert into " + type.Name + " (";
                    string sql1 = " values (";
                    int flag = 0;
                    foreach (var item in type.GetProperties())
                    {
                        if (flag == 0)
                        {
                            flag = 1;
                            continue;
                        }
                        else
                        {
                            sql += item.Name + ",";
                            sql1 += "@" + item.Name + ",";
                        }
                    }

                    sql = sql.TrimEnd(',') + ") ";
                    sql1 = sql1.TrimEnd(',') + ")";
                    string sqls = sql + sql1;
                    return conn.Execute(sqls, t);
                }
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="t">具体类</param>
        /// <returns>返回受影响行数</returns>
        public static int Insert<T>(T[] t)
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "insert into " + type.Name + " values (";
                    int flag = 0;
                    foreach (var item in type.GetProperties())
                    {
                        if (flag == 0)
                        {
                            flag = 1;
                            continue;
                        }
                        else
                        {
                            sql += "@" + item.Name + ",";
                        }
                    }

                    sql = sql.TrimEnd(',') + ")";
                    return conn.Execute(sql, t);
                }
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 单删
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="id">id参数</param>
        /// <returns>返回受影响行数</returns>
        public static int Delete<T>(int id)
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "delete from " + type.Name + " where ";
                    foreach (var item in type.GetProperties())
                    {
                        sql += item.Name + "=" + id;
                        break;
                    }

                    return conn.Execute(sql);
                }
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        public static int Delete<T>(string id)
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "delete from " + type.Name + " where ";
                    foreach (var item in type.GetProperties())
                    {
                        sql += item.Name + " in (" + id;
                        sql = sql.TrimEnd(',') + ")";
                        break;
                    }

                    return conn.Execute(sql);
                }
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        public static int Update<T>(T t)
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "update " + type.Name + " set ";
                    string sql1 = " where ";
                    int flag = 0;
                    foreach (var item in type.GetProperties())
                    {
                        if (flag == 0)
                        {
                            sql1 += item.Name + "=@" + item.Name;
                            flag = 1;
                            continue;
                        }
                        else
                        {
                            sql += item.Name + "=@" + item.Name + ",";
                        }
                    }

                    sql = sql.TrimEnd(',');
                    sql1 = sql1.TrimEnd(',');
                    string sqls = sql + sql1;
                    return conn.Execute(sqls, t);
                }
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        public static List<T> GetList<T>(string sqlStr)
        {
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    List<T> list = conn.Query<T>(sqlStr).ToList();
                    return list;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="id">id参数</param>
        /// <returns>返回查询结果</returns>
        public static T GetListById<T>(int id)
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "select *from " + type.Name + " where ";
                    foreach (var item in type.GetProperties())
                    {
                        sql += item.Name + "=" + id;
                        break;
                    }
                    return conn.Query<T>(sql).ToList().FirstOrDefault();
                }
            }
            catch
            {
                return default(T);
            }
        }

         /// <summary>
         /// 分页
         /// </summary>
         /// <typeparam name="T">泛型参数</typeparam>
         /// <param name="pageIndex">当前页码</param>
         /// <param name="pageSize">每页显示的页数</param>
         /// <param name="strWhere">暂时不知道</param>
         /// <returns>返回查询结果</returns>
        public static List<T> GetPager<T>(int pageIndex, int pageSize, string strWhere = "")
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "select *from " + type.Name + " limit " + ((pageIndex - 1) * pageSize) + "," + pageSize + " ";
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sql = sql + strWhere;
                    }

                    return conn.Query<T>(sql).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetRowCount<T>()
        {
            Type type = typeof(T);
            try
            {
                using (IDbConnection conn = DapperHelper.MySqlCon())
                {
                    string sql = "select 1 from " + type.Name;
                    return conn.Query<T>(sql).ToList().Count;
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
