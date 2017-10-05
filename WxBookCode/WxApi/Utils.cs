using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Xml.Linq;
using WxApi.MsgEntity;
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

        public class FileStreamInfo : MemoryStream
        {
            public string FileName { get; set; }
        }

        /// <summary>
        /// 用于下载FileStreamInfo类型的文件流
        /// </summary>
        /// <param name="stream"></param>
        public static void DownLoadStream(FileStreamInfo stream)
        {
            var bytes = stream.ToArray();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlDecode(stream.FileName));
            HttpContext.Current.Response.AddHeader("Content-Length", bytes.Length.ToString());
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 根据文件中的物理路径或网络路径下载文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="fileName"></param>
        public static void DownLoadFile(string fileUrl, string fileName)
        {
            using (var client = new WebClient())
            {
                var bytes = client.DownloadData(fileUrl);
                using (var fsi = new FileStreamInfo())
                {
                    fsi.Write(bytes, 0, bytes.Length);
                    fsi.FileName = fileName;
                    DownLoadStream(fsi);
                }
            }
        }
        
        public static void DownLoadByPost(string url, string data, Stream stream)
        {
            using (var webclient = new WebClient())
            {
                var retdata = webclient.UploadData(url, "POST", Encoding.UTF8.GetBytes(data));
                stream.Write(retdata, 0, retdata.Length);
            }
        }

        /// <summary>
        /// 将微信POST过来的XML数据包转换成对应的实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static T ConvertObj<T>(string xmlstr)
        {
            try
            {
                XElement xdoc = XElement.Parse(xmlstr);
                // 获取转换的数据类型
                var type = typeof(T);
                // 创建实例
                var t = Activator.CreateInstance<T>();
                #region 基础属性赋值
                var ToUserName = type.GetProperty("ToUserName");
                ToUserName.SetValue(t, Convert.ChangeType(xdoc.Element("ToUserName").Value, ToUserName.PropertyType), null);
                xdoc.Element("ToUserName").Remove();

                var FromUserName = type.GetProperty("FromUserName");
                FromUserName.SetValue(t, Convert.ChangeType(xdoc.Element("FromUserName").Value, FromUserName.PropertyType), null);
                xdoc.Element("FromUserName").Remove();

                var CreateTime = type.GetProperty("CreateTime");
                CreateTime.SetValue(t, Convert.ChangeType(xdoc.Element("CreateTime").Value, CreateTime.PropertyType), null);
                xdoc.Element("CreateTime").Remove();

                var MsgType = type.GetProperty("MsgType");
                string msgtype = xdoc.Element("MsgType").Value.ToUpper();
                MsgType.SetValue(t, (MsgType)Enum.Parse(typeof(MsgType), msgtype), null);
                xdoc.Element("MsgType").Remove();

                // 判断消息类型是否是事件
                if(msgtype == "EVENT")
                {
                    // 获取事件类型
                    var EventType = type.GetProperty("Event");
                    string eventtype = xdoc.Element("Event").Value.ToUpper();
                    EventType.SetValue(t, (EventType)Enum.Parse(typeof(EventType), eventtype), null);
                    xdoc.Element("Event").Remove();
                }
                #endregion

                // 遍历XML节点
                foreach(XElement element in xdoc.Elements())
                {
                    // 根据XML节点的名称，获取实体的属性
                    var pr = type.GetProperty(element.Name.ToString());
                    // 给属性赋值
                    pr.SetValue(t, Convert.ChangeType(element.Value, pr.PropertyType), null);
                }
                return t;
            }
            catch(Exception)
            {
                return default(T);
            }
        }


        public static int ConvertDateTimeInt(System.DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static void OutPrint(string txt)
        {

            System.Diagnostics.Debug.WriteLine("----------------------Receive the message Start----------------------");
            System.Diagnostics.Debug.WriteLine(txt);
            System.Diagnostics.Debug.WriteLine("---------------------- Receive the message End ----------------------");   
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
