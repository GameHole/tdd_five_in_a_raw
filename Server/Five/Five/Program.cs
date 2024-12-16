using System;
using System.Net;

namespace Five
{
    class Program
    {
        static void Main(string[] args)
        {
            var factroy = new NetFactroy(new SerializerRegister(),new SocketFactroy());
            var processFactroy = new ProcesserFactroy(new Domain(new GameFactroy(), new IdGenrator()));
            var server = factroy.NewServer("127.0.0.1", 11000, processFactroy);
            server.Start();
        }
    }
}
