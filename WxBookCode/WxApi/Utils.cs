using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WxApi
{
    
    public class Utils
    {

        /// <summary>
        /// HTTP GET方式请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns>响应信息</returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "*/*";
            string responseStr = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                
            responseStr = reader.ReadToEnd();
            return responseStr;
        }

        /// <summary>
        /// HTTP POST 方式请求数据
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="param">POST的数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((a, b, c, d) => true);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-from-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            string responseStr = "";
            using(StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
            {
                requestStream.Write(param); // 将请求的数据写入请求流
            }
            using(HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                using(StreamReader reader = new StreamReader (response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd(); // 获取响应
                }
            }
            return responseStr;
        }


        public static string GetRequestData()
        {
            using (var stream = HttpContext.Current.Request.InputStream)
            {
                using(var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T PostResult<T>(object obj, string url)
        {
            // 序列化设置
            var setting = new JsonSerializerSettings();
            // 解决枚举类型序列化，被转换成数字的问题
            setting.Converters.Add(new StringEnumConverter());
            var retdata = HttpPost(url, JsonConvert.SerializeObject(obj, setting));
            return JsonConvert.DeserializeObject<T>(retdata);
        }


        
        public static string HttpPostForm(string url, List<FormEntity> form)
        {
            // 分割字符串
            var boundary = "----" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((a, b, c, d) => true);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            MemoryStream stream = new MemoryStream();
            #region 将非文件表单写入内存流
            foreach (var entity in form.Where(f => f.IsFile == false))
            {
                var temp = new StringBuilder();
                temp.AppendFormat("\r\n--{0}", boundary);
                temp.Append("\r\nContent-Disposition: form-data;");
                temp.Append("\r\n\r\n");
                temp.Append(entity.Value);
                byte[] b = Encoding.UTF8.GetBytes(temp.ToString());
                stream.Write(b, 0, b.Length);
            }
            #endregion

            #region 将文件表单写入内存流
            foreach (var entity in form.Where(f => f.IsFile == true))
            {
                byte[] filedata = null;
                var filename = Path.GetFileName(entity.Value);
                if (entity.Value.Contains("http"))
                {
                    // 处理网络文件
                    using (var client = new WebClient())
                    {
                        filedata = client.DownloadData(entity.Value);
                    }
                }
                else
                {
                    // 处理物理路径文件
                    using (FileStream file = new FileStream(entity.Value, FileMode.Open, FileAccess.Read))
                    {
                        filedata = new byte[file.Length];
                        file.Read(filedata, 0, (int)file.Length);
                    }
                }
                var temp = string.Format("\r\n--{0}\r\nContent-Disposition:" + "form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n", boundary, entity.Name, filename);
                byte[] b = Encoding.UTF8.GetBytes(temp);
                stream.Write(b, 0, b.Length);
                stream.Write(filedata, 0, filedata.Length);
            }
            #endregion
            //结束标记
            byte[] foot_data = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            stream.Write(foot_data, 0, foot_data.Length);
            Stream reqStream = request.GetRequestStream();
            stream.Position = 0L;
            //将Form表单生成流写入请求流
            stream.CopyTo(reqStream);
            stream.Close();
            reqStream.Close();
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using(StreamReader reader = new StreamReader (response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd(); // 获取响应
                }
            }
            
        }


        /// <summary>
        /// 发起postForm请求，并获取请求的返回值
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="formEntities">数据实体</param>
        /// <param name="url">接口地址</param>
        /// <returns></returns>
        public static T PostFormResult<T>(List<FormEntity> formEntities, string url)
        {
            var retdata = HttpPostForm(url, formEntities);
            return JsonConvert.DeserializeObject<T>(retdata);
        }


        /// <summary>
        /// 发起Get请求，并获取请求返回值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <returns></returns>
        public static T GetResult<T>(string url)
        {
            var retdata = HttpGet(url);
            return JsonConvert.DeserializeObject<T>(retdata);
        }
        
    }

    public class FormEntity
    {
        /// <summary>
        /// 表单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表单值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否是文件
        /// </summary>
        public bool IsFile { get; set; }


    }
    
}
