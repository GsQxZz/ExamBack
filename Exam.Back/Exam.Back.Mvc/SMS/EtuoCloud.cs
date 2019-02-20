using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Exam.Back.Mvc
{
    public class EtuoCloud
    {
        /*
		 * http://www.etuocloud.com/doc/api_doc_overview.html
		 */

        /// ========================================程序配置参数区开始
        //接口生产地址(应用上线后正式环境必须使用该地址)
        //private  static String url = "http://www.etuocloud.com/gateway.action";
        //接口测试地址（未上线前测试环境使用）
        private static string url = "http://www.etuocloud.com/gatetest.action";

        //应用 app_key
        private static string APP_KEY = "NnGrb1pNrub4oGbQQJKM7nWWXNBDdJUv";
        //应用 app_secret
        private static string APP_SECRET = "geFcEPM6yUehy61d5Ck3e2wYpup0WWWEZ2Fga6Ld7w4E2Lw6sqGx21prnNKMtVlq";

        //接口响应格式 json或xml
        private static string FORMAT = "json";

        /// ========================================程序配置参数区结束
        /// 

        /// <summary>
        /// 发生短信验证码
        /// </summary>
        /// <param name="to">手机号</param>
        /// <param name="template">短信模板ID</param>
        /// <param name="smscode">验证码</param>
        /// <returns></returns>
        public static string SendSmsCode(string to, int template, string smscode)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("view", FORMAT);
            parameters.Add("method", "cn.etuo.cloud.api.sms.simple");
            parameters.Add("out_trade_no", "");//商户订单号，可空
            parameters.Add("to", to);
            parameters.Add("template", template.ToString());
            parameters.Add("smscode", smscode);
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);

        }

        /// <summary>
        /// 发生模板短信
        /// </summary>
        /// <param name="to">手机号</param>
        /// <param name="template">短信模板ID</param>
        /// <param name="param">模板中的参数，以英文逗号分隔</param>
        /// <returns></returns>
        public static string SendSmsTpl(string to, int template, string param)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("view", FORMAT);
            parameters.Add("method", "cn.etuo.cloud.api.sms.template");
            parameters.Add("out_trade_no", "");//商户订单号，可空
            parameters.Add("to", to);
            parameters.Add("template", template.ToString());
            parameters.Add("params", param);
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);
        }

        /// <summary>
        /// 发送自定义短信
        /// </summary>
        /// <param name="to">手机号</param>
        /// <param name="content">短信内容</param>
        /// <param name="out_trade_no">商户订单号，可空</param>
        /// <returns></returns>
        public static string SendText(string to, string content, string out_trade_no)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("view", FORMAT);
            parameters.Add("method", "cn.etuo.cloud.api.sms.advance");
            parameters.Add("out_trade_no", out_trade_no);
            parameters.Add("to", to);
            parameters.Add("content", content);
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);
        }

        /// <summary>
        /// 发送语音验证码
        /// </summary>
        /// <param name="to">手机号</param>
        /// <param name="template">语音模板ID</param>
        /// <param name="verifyCode">验证码</param>
        /// <returns></returns>
        public static string SendVoiceCode(string to, int template, string verifyCode)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("method", "cn.etuo.cloud.api.voice.simple");
            parameters.Add("out_trade_no", "");//商户订单号，可空
            parameters.Add("to", to);
            parameters.Add("template", template.ToString());
            parameters.Add("verifyCode", verifyCode);
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);
        }

        /// <summary>
        /// 发送语音通知
        /// </summary>
        /// <param name="to">接收语音通知的手机号</param>
        /// <param name="template">语音通知模板ID</param>
        /// <returns></returns>
        public static string SendVoiceNotice(string to, int template)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("method", "cn.etuo.cloud.api.voice.message");
            parameters.Add("out_trade_no", "");//商户订单号，可空
            parameters.Add("to", to);
            parameters.Add("template", template.ToString());
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);
        }

        /// <summary>
        /// 获取流量产品列表
        /// </summary>
        /// <returns></returns>
        public static string GetFlowProductList()
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("method", "cn.etuo.cloud.api.flow.list");
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);
        }

        /// <summary>
        /// 充值流量
        /// </summary>
        /// <param name="to">手机号</param>
        /// <param name="flowcode">流量产品编码</param>
        /// <param name="out_trade_no">商户订单号，可空</param>
        /// <returns></returns>
        private static string RechargeFlow(string to, string flowcode, string out_trade_no)
        {
            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("method", "cn.etuo.cloud.api.flow.charge");
            parameters.Add("out_trade_no", out_trade_no);
            parameters.Add("mobile", to);
            parameters.Add("flowcode", flowcode);
            parameters.Add("sign", GetSign(parameters));
            return HttpClientV2.HttpPost(url, parameters);
        }

        /// <summary>
        /// 获取param签名
        /// </summary>
        /// <param name="sParams"></param>
        /// <returns></returns>
        private static string GetSign(SortedDictionary<string, string> parameters)
        {
            string sign = string.Empty;
            StringBuilder codedString = new StringBuilder();

            foreach (KeyValuePair<string, string> temp in parameters)
            {
                if (temp.Value == "" || temp.Value == null || temp.Key.ToLower() == "sign")
                {
                    continue;
                }

                if (codedString.Length > 0)
                {
                    codedString.Append("&");
                }

                codedString.Append(temp.Key.Trim());
                codedString.Append("=");
                codedString.Append(temp.Value.Trim());
            }

            // 应用key
            codedString.Append(APP_SECRET);
            string signkey = codedString.ToString();
            sign = GetMD5(signkey, "utf-8");

            return sign;
        }

        /// <summary>
        /// MD5验证签名
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        private static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用XXX编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.UTF8.GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();

            //  return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5").ToLower(); ;

            return retStr;
        }
    }
}