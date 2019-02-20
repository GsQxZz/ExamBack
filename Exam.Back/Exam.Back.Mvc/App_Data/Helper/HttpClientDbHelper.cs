using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data;

public static class HttpClientDbHelper
{
    /// <summary>
    /// 返回类型
    /// </summary>
    /// <typeparam name="T">参数类</typeparam>
    /// <typeparam name="U">返回类</typeparam>
    /// <param name="URi">地址</param>
    /// <param name="Model">参数实例</param>
    /// <returns></returns>
    public static U PostClient<T, U>(string URi, T Model)
    {
        var json = JsonConvert.SerializeObject(Model);  //序列成json字符串对象
        HttpContent context = new StringContent(json); //把字符放到上下文对象中
        context.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");//设定传递格式是json对象
        HttpClient Client = new HttpClient(); //操作api对象
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        var mess = Client.PostAsync(URi, context).Result;
        if (mess.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<U>(mess.Content.ReadAsStringAsync().Result);

        }
        else
        {
            return default(U);
        }
    }

    /// <summary>
    /// 查询操作
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="Uri"></param>
    /// <returns></returns>
    public static U GetSearchList<U>(string Uri)
    {
        Uri ur = new Uri(Uri); //完整的地址
        HttpClient Client = new HttpClient();//基于操作http的对象
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//设定返回数据的格式
        var mess = Client.GetAsync(ur).Result; //返回异步执行的结果（）                                 
        var json = "";
        //判断执行是否成功，成功后StatusCode  返回ok
        if (mess.StatusCode == System.Net.HttpStatusCode.OK)
        {
            json = mess.Content.ReadAsStringAsync().Result;//读取返回的内容
            return JsonConvert.DeserializeObject<U>(json);//转换成List集合
        }
        else
        {
            return default(U);
        }
    }


    /// <summary>
    /// 执行删除操作
    /// </summary>
    /// <param name="Uri"></param>
    /// <returns></returns>
    public static int Delete(string Uri)
    {
        Uri ur = new Uri(Uri); //完整的地址
        HttpClient Client = new HttpClient();//基于操作http的对象
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//设定返回数据的格式
        var mess = Client.GetAsync(ur).Result; ;//返回异步执行的结果（）                                 
        var i = 0;
        //判断执行是否成功，成功后StatusCode  返回ok
        if (mess.StatusCode == System.Net.HttpStatusCode.OK)
        {
            i = Convert.ToInt32(mess.Content.ReadAsStringAsync().Result);//读取返回的内容
            return i;
        }
        else
        {
            return 0;
        }
    }


    /// <summary>
    /// 用于查询和显示的作用或者删除数据
    /// </summary>
    /// <param name="Uri"></param>
    /// <returns></returns>
    public static DataTable GetSearchList(string Uri)
    {
        Uri ur = new Uri(Uri); //完整的地址
        HttpClient Client = new HttpClient();//基于操作http的对象  
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//设定返回数据的格式
        var mess = Client.GetAsync(ur).Result; ;//返回异步执行的结果（）                                 
        var json = "";
        //判断执行是否成功，成功后StatusCode  返回ok
        if (mess.StatusCode == System.Net.HttpStatusCode.OK)
        {
            json = mess.Content.ReadAsStringAsync().Result;//读取返回的内容
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);//转换成List集合
            return dt;

        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 绑定下拉框
    /// </summary>
    /// <param name="Uri"></param>
    /// <param name="ShowText"></param>
    /// <param name="Value"></param>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static DataTable GetComBindList(string Uri, string ShowText, string Value, string Text)
    {
        Uri ur = new Uri(Uri); //完整的地址
        HttpClient Client = new HttpClient();//基于操作http的对象
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//设定返回数据的格式
        var mess = Client.GetAsync(ur).Result; ;//返回异步执行的结果（）                                 
        var json = "";
        //判断执行是否成功，成功后StatusCode  返回ok
        if (mess.StatusCode == System.Net.HttpStatusCode.OK)
        {
            json = mess.Content.ReadAsStringAsync().Result;//读取返回的内容
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);//转换成List集合
            DataRow dr = dt.NewRow();
            dr[Value] = "0";
            dr[Text] = ShowText;
            dt.Rows.InsertAt(dr, 0);
            return dt;

        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 用于添加、修改操作
    ///  T 代表类型
    ///  URi 代表地址
    ///  Model 传递的参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="URi"></param>
    /// <param name="Model"></param>
    /// <returns></returns>
    public static int sPostClient<T>(string URi, T Model)
    {
        var json = JsonConvert.SerializeObject(Model);  //序列成json字符串对象
        HttpContent context = new StringContent(json); //把字符放到上下文对象中
        context.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");//设定传递格式是json对象
        HttpClient Client = new HttpClient(); //操作api对象
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        var mess = Client.PostAsync(URi, context).Result;
        int i = 0;
        if (mess.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return i = Convert.ToInt32(mess.Content.ReadAsStringAsync().Result);
        }
        else
        {
            return 0;
        }
    }


  
}

