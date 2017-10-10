using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WxApi.MsgEntity
{
    public class TemplateNotice
    {
        private static Dictionary<string, Dictionary<string, int>> _industryList;
        public static Dictionary<string, Dictionary<string, int>> IndustryList
        {
            get
            {
                if (_industryList == null)
                    _industryList = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(Code.IndustryCode);
                return _industryList;
            }
        }
    }
}
