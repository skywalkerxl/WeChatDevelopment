using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;
using WxApi.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace WxTest.Handler.Test
{
    /// <summary>
    /// MongoDBTest 的摘要说明
    /// </summary>
    public class MongoDBTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DeleteTest();
        }

        /// <summary>
        ///  插入数据测试
        /// </summary>
        static void InsertTest()
        {
            var random = new Random();
            for (var i = 1; i <= 10; i++)
            {
                var item = new Student()
                {
                    Name = "我的名字",
                    Age = random.Next(25, 30),
                    State = i % 2 == 0 ? State.Normal : State.Unused
                };
                MongoDbHelper.Insert(DbConfigParams.ConnectionString, DbConfigParams.DbName, CollectionNames.Student, item);
            }
        }

        /// <summary>
        /// 查询测试
        /// </summary>
        static void QueryTest()
        {
            var queryBuilder = new QueryBuilder<Student>();
            var query = queryBuilder.GTE(x => x.Age, 27);
            var ltModel = MongoDbHelper.GetManyByCondition<Student>(DbConfigParams.ConnectionString, DbConfigParams.DbName, CollectionNames.Student, query);
            if(ltModel != null && ltModel.Count > 0)
            {
                foreach(var item in ltModel)
                {
                    System.Diagnostics.Debug.WriteLine("姓名：{0}，年龄：{1}，状态：{2}", item.Name, item.Age, GetStateDesc(item.State));
                }
            }
        }

        /// <summary>
        /// 更新测试
        /// </summary>
        static void UpdateTest()
        {
            var queryBuilder = new QueryBuilder<Student>();
            var query = queryBuilder.GTE(x => x.Age, 27);
            var dictUpdate = new Dictionary<string, BsonValue>();
            dictUpdate["State"] = State.Unused;
            MongoDbHelper.Update(DbConfigParams.ConnectionString, DbConfigParams.DbName, CollectionNames.Student, query, dictUpdate);
        }

        /// <summary>
        /// 删除测试
        /// </summary>
        static void DeleteTest()
        {
            var queryBuilder = new QueryBuilder<Student>();
            var query = queryBuilder.GTE(x => x.Age, 28);
            MongoDbHelper.DeleteByCondition(DbConfigParams.ConnectionString, DbConfigParams.DbName, CollectionNames.Student, query);
        }

        /// <summary>
        /// 获取状态描述
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        static string GetStateDesc(State state)
        {
            string result = string.Empty;
            switch(state)
            {
                case State.All:
                    result = "全部";
                    break;
                case State.Normal:
                    result = "正常";
                    break;
                case State.Unused:
                    result = "未使用";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}