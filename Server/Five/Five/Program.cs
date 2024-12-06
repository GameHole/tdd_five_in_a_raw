using System;
using System.Net;

namespace Five
{
    class Program
    {
        static void Main(string[] args)
        {
            var factroy = new NetFactroy(new SerializerRegister());
            var processFactroy = new ProcesserFactroy(new MatchServce(new App(),new GameFactroy()));
            var server = factroy.NewServer("127.0.0.1", 10000, processFactroy);
            server.Start();
        }
    }
}
