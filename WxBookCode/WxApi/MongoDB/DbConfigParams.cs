using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WxApi.MongoDB
{
    /// <summary>
    /// 数据库配置参数
    /// </summary>
    public static class DbConfigParams
    {
        private static string connectionString = ConfigurationManager.AppSettings["MongoDBConn"];

        /// <summary>
        /// 获取 数据库连接串
        /// </summary>
        public static string ConnectionString
        {
            get { return connectionString; }
        }

        private static string dbName = ConfigurationManager.AppSettings["MongoDBName"];

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        public static string DbName
        {
            get { return dbName; }
        }
    }
}
