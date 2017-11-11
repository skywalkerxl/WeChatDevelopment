using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MongoDB
{
    /// <summary>
    /// 学生类（测试）
    /// </summary>
    public class Student : EntityBase
    {
        /// <summary>
        /// 获取 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 获取 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 获取 状态
        /// </summary>
        public State State { get; set; }
    }
}
