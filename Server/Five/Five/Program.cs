using System;

namespace Five
{
    class Program
    {
        static void Main(string[] args)
        {
            var factroy = new ProcesserFactroy(new App());
            var server = new Server("127.0.0.1", 10000, factroy);
            server.Start();
        }
    }
}
