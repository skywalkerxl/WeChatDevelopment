using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.JSApi
{
    public class JsApiTicket : ErrorEntity
    {

        public string ticket { get; set; }

        public int _expires_in;

        public int expires_in
        {
            get { return _expires_in; }
            set
            {
                expires_time = DateTime.Now.AddSeconds(value);
                _expires_in = value;
            }
        }

        public DateTime expires_time { get; set; }
    }
}
