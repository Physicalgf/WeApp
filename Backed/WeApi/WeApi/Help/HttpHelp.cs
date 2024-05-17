using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeApi.Help
{
    public static class HttpHelp
    {
        #region 后台发送POST请求

        /// &lt;summary>
        /// 后台发送POST请求
        /// &lt;/summary>
        /// &lt;param name="url">服务器地址&lt;/param>
        /// &lt;param name="data">发送的数据&lt;/param>
        /// &lt;returns>&lt;/returns>
        public static string HttpPost(string url, string data, List<string> strList = null)
        {
            string ip = string.Empty;
            try
            {
                //创建post请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                //request.UserAgent = "Mozilla/5.0 (Linux; Android 10; Redmi K30 Pro Build/QKQ1.191117.002; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.62 XWEB/2693 MMWEBSDK/200901 Mobile Safari/537.36 MMWEBID/5869 MicroMessenger/7.0.19.1760(0x27001353) Process/appbrand2 WeChat/arm64 NetType/WIFI Language/zh_CN ABI/arm64";
                byte[] payload = Encoding.UTF8.GetBytes(data);
                request.ContentLength = payload.Length;
                if (strList != null && strList.Count > 0)
                {
                    // 随机抽取ip
                    Random rd = new Random();
                    int count = rd.Next(0, strList.Count);
                    ip = strList[count];
                    System.Net.WebProxy proxy = new WebProxy(ip);
                    //request.Proxy = proxy;
                }

                //发送post的请求
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();

                //接受返回来的数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string value = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();

                return value;
            }
            catch (Exception ex)
            {
                return ex.Message + ip;
            }
        }

        #endregion

        #region 调用post方法，返回JSon数据

        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>
        /// <returns></returns>
        public static string Post(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";//"application/x-www-form-urlencoded";

            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            { }

            return null;
        }

        #endregion

        #region 发起POST异步请求

        /// <summary>
        /// 发起POST异步请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="keyValues">请求参数</param>
        /// <param name="headers">请求头</param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, object keyValues, Dictionary<string, string> headers = null)
        {
            try
            {
                string result = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.Timeout = 10000;//设置请求超时时间，单位为毫秒
                req.ContentType = "application/json;charset=UTF-8";
                if (headers != null)
                {
                    foreach (var header in headers)
                        req.Headers.Add(header.Key, header.Value);
                }
                if (keyValues != null)
                {
                    byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keyValues));
                    req.ContentLength = data.Length;
                    using (Stream reqStream = req.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);

                        reqStream.Close();
                    }
                }

                HttpWebResponse resp = (HttpWebResponse)await (req.GetResponseAsync());
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion

        #region 发起GET同步请求

        /// <summary>
        /// 发起GET同步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                HttpResponseMessage response = client.GetAsync(url).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }
        #endregion

        #region 发起GET异步请求

        /// <summary>
        /// 发起GET异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                HttpResponseMessage response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion



    }
}
