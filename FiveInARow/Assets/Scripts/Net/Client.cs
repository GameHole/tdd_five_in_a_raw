using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace Five
{
    public class Client:IUpdate
    {
        private ASocket socket;
        private ConcurrentQueue<Message> posts = new ConcurrentQueue<Message>();

        public Client(ASocket socket, IProcesserRegister processerRegister)
        {
            this.socket = socket;
            Procsesser = new MessageProcesser(socket,new OpCodeNotFoundProcesser());
            processerRegister.Regist(Procsesser);
            socket.onRecv -= Procsesser.Process;
            socket.onRecv += (Message msg) =>
            {
                posts.Enqueue(msg);
            };
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

        public void Update(float dt = 0)
        {
            while (posts.TryDequeue(out var message))
            {
                Procsesser.Process(message);
            }
        }
    }
}
