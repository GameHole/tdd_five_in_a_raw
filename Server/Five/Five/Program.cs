using System;

namespace Five
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            var server = new Server("127.0.0.1", 10000, app,new ProcesserFactroy(app.mgr));
            server.Start();
        }
    }
}
