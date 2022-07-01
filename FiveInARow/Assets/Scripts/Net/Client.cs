using System;
using System.Threading.Tasks;

namespace Five
{
    public class Client
    {
        private ASocket socket;

        public Client(ASocket socket, IProcesserRegister processerRegister)
        {
            this.socket = socket;
            Procsesser = new MessageProcesser(socket);
            processerRegister.Regist(Procsesser);
        }

        public MessageProcesser Procsesser { get;private set; }

        public Task<Result> ConnectAsync(string ip, int port)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    socket.Connect(ip, port);
                    return ResultDefine.Success;
                }
                catch (Exception)
                {
                    return LocalResultDefine.ConnectError;
                }
            });
        }
    }
}
