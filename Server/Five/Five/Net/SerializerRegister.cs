using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class SerializerRegister
    {
        public void Regist(MessageSerializer ser)
        {
            ser.Add(MessageCode.RequestMatch, new DefaultSerializer());
            ser.Add(MessageCode.GetResponseCode(MessageCode.RequestMatch), new ResponseSerializer());
            ser.Add(MessageCode.RequestCancelMatch, new DefaultSerializer());
            ser.Add(MessageCode.GetResponseCode(MessageCode.RequestCancelMatch), new ResponseSerializer());
            ser.Add(MessageCode.RequestPlay, new PlayMessageSerializer());
            ser.Add(MessageCode.GetResponseCode(MessageCode.RequestPlay), new ResponseSerializer());
            ser.Add(MessageCode.StartNotify, new StartNotifySerializer());
            ser.Add(MessageCode.PlayedNotify, new PlayNotifySerializer());
            ser.Add(MessageCode.FinishNotify, new FinishNotifySerializer());
        }
    }
}
