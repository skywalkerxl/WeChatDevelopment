using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using WxApi.MongoDB;

namespace WxApi
{
    public static class MongoDbHelper
    {
        /// <summary>
        /// 获取MongoDB实例对象
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns>数据库实例对象</returns>
        private static MongoDatabase GetDatabase(string connectionString, string dbName)
        {
            var server = MongoServer.Create(connectionString);
            return server.GetDatabase(dbName);
        }

        #region 新增
        public static void Insert<T>(string connectionString, string dbName, string collectionName, T model) where T: EntityBase
        {
            if(model == null)
            {
                throw new ArgumentNullException("model", "带插入的数据库不能为空");
            }
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection<T>(collectionName);
            collection.Insert(model);
        }
        #endregion

        #region 更新
        public static void Update(string connectionString, string dbName, string collectionName, IMongoQuery query, Dictionary<string, BsonValue> dictUpdate)
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection(collectionName);
            var update = new UpdateBuilder();
            if (dictUpdate != null && dictUpdate.Count > 0)
            {
                foreach (var item in dictUpdate)
                {
                    update.Set(item.Key, item.Value);
                }
            }
            var d = collection.Update(query, update, UpdateFlags.Multi);
        }
        #endregion

        #region 查询
        public static T GetById<T>(string connectionString, string dbName, string collectionName, ObjectId id) where T: EntityBase
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection<T>(collectionName);
            return collection.FindOneById(id);
        }
        #endregion

        #region 根据查询条件获取一条语句
        public static T GetOneByCondition<T>(string connectionString, string dbName, string collectionName, IMongoQuery query) where T:EntityBase
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection<T>(collectionName);
            return collection.FindOne(query);
        }
        #endregion

        #region 根据查询条件获取多条语句
        public static List<T> GetManyByCondition<T>(string connectionString, string dbName, string collectionName, IMongoQuery query) where T:EntityBase
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection<T>(collectionName);
            return collection.Find(query).ToList();
        }
        #endregion


        #region 获取集合中的所有数据
        public static List<T> GetAll<T>(string connectionString, string dbName, string collectionName) where T: EntityBase
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection<T>(collectionName);
            return collection.FindAll().ToList();
        }
        #endregion
        
        #region 删除
        public static void DeleteByCondition(string connectionString, string dbName, string collectionName, IMongoQuery query)
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection(collectionName);
            collection.Remove(query);
        }

        public static void DeleteAll(string connectionString, string dbName, string collectionName)
        {
            var db = GetDatabase(connectionString, dbName);
            var collection = db.GetCollection(collectionName);
            collection.RemoveAll();
        }
        #endregion

        #region 

        #endregion
    }
}