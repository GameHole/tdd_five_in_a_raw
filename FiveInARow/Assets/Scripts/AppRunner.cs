using System.Collections.Generic;
using UnityEngine;
namespace Five
{
    public class AppRunner:MonoBehaviour
    {
        public App app { get; private set; }
        public TcpSocket socket { get; private set; }
      

        private void Start()
        {
            socket = new TcpSocket(new SerializerRegister());
            app = new App(socket, new Ip { address = "127.0.0.1", port = 10000 });
            app.Start();
        }
        private void Update()
        {
            app.Update(Time.deltaTime);
        }
    }
}
