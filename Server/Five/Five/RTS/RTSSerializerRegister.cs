using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSSerializerRegister : ISerializerRegister
    {
        public void Regist(MessageSerializer ser)
        {
            ser.Add(MessageCode.RequestMatch, new DefaultSerializer());
            ser.Add(MessageCode.GetResponseCode(MessageCode.RequestMatch), new ResponseSerializer());
        }
    }
}
