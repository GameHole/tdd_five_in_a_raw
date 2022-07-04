using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class SerializerRegister
    {
        public void Regist(MessageSerializer ser)
        {
            ser.Container.Add(MessageCode.RequestMatch, new DefaultSerializer());
            ser.Container.Add(MessageCode.GetResponseCode(MessageCode.RequestMatch), new MatchResponseSerializer());
            ser.Container.Add(MessageCode.RequestCancelMatch, new DefaultSerializer());
            ser.Container.Add(MessageCode.GetResponseCode(MessageCode.RequestCancelMatch), new ResponseSerializer());
            ser.Container.Add(MessageCode.RequestPlay, new PlayMessageSerializer());
            ser.Container.Add(MessageCode.GetResponseCode(MessageCode.RequestPlay), new ResponseSerializer());
            ser.Container.Add(MessageCode.StartNotify, new StartNotifySerializer());
            ser.Container.Add(MessageCode.PlayedNotify, new PlayNotifySerializer());
            ser.Container.Add(MessageCode.FinishNotify, new FinishNotifySerializer());
        }
    }
}
