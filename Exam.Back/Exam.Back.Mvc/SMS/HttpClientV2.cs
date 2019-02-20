using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Exam.Back.Mvc
{
	public class HttpClientV2 {
		/// <summary>
		/// POST请求与获取结果  
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string HttpPost(string Url, SortedDictionary<string, string> parameters) {
			return HttpPost(Url, toParaData(parameters));
		}



		//调用http接口,接口编码为utf-8
		private static string toParaData(SortedDictionary<string, string> parameters) {

			//设置参数，并进行URL编码
			StringBuilder codedString = new StringBuilder();
			foreach(string key in parameters.Keys) {
				// codedString.Append(HttpUtility.UrlEncode(key));
				codedString.Append(key);
				codedString.Append("=");
				codedString.Append(HttpUtility.UrlEncode(parameters[key], System.Text.Encoding.UTF8));
				codedString.Append("&");
			}
			string paraUrlCoded = codedString.Length == 0 ? string.Empty : codedString.ToString().Substring(0, codedString.Length - 1);


			return paraUrlCoded;
		}


		/// <summary>  
		/// POST请求与获取结果  
		/// </summary>  
		public static string HttpPost(string Url, string postDataStr) {

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

			//request.ContentLength = postDataStr.Length;
			//StreamWriter writer = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8);
			// writer.Write(postDataStr);
			// writer.Flush();


			//将URL编码后的字符串转化为字节
			byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
			request.ContentLength = payload.Length;
			Stream writer = request.GetRequestStream();
			writer.Write(payload, 0, payload.Length);
			writer.Close();

			//获得响应流
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			string encoding = response.ContentEncoding;
			if(encoding == null || encoding.Length < 1) {
				encoding = "UTF-8"; //默认编码  
			}
			StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));

			string retString = reader.ReadToEnd();
			return retString;
		}



	}

}