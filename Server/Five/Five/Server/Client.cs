using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Client
    {
        public ASocket socket { get; }
        public Matcher matcher { get; }
        public MessageProcesser processer { get; }
        public Client(ASocket socket, Matching matching)
        {
            this.socket = socket;
            matcher = new Matcher(matching);
            matcher.Player.notifier = new NetNotifier(socket, matcher.Player);
            processer = new MessageProcesser(socket, new OpCodeErrorResponseProcesser(socket));
            socket.onClose += () => matcher.Player.OutLine();
        }
    }
}
