using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class StartNotify : Message
    {
        public PlayerInfo[] infos;
        public StartNotify(PlayerInfo[] infos) : base(MessageCode.StartNotify)
        {
            this.infos = infos;
        }
        public override string ToString()
        {
            var info = "";
            foreach (var item in infos)
            {
                info += $"({item.Chess},{item.PlayerId})";
            }
            return base.ToString() + " " + info;
        }
    }
}
