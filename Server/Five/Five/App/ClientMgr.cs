using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientMgr
    {
        private Matching matching;
        public ConcurrentList<MssageProcesser> clients { get; private set; }
        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new ConcurrentList<MssageProcesser>();
        }
        public void onAccept(ASocket socket)
        {
            MssageProcesser client = new MssageProcesser(socket);
            new RequestRegister(socket, new Matcher(matching)).Regist(client);
            clients.Add(client);
            socket.onClose += () => clients.Remove(client);
        }
    }
}
