namespace Five
{
    public class Binder
    {
        public int OpCode { get; }
        public AServceProcesser processer { get; }

        public Binder(int code, AServceProcesser processer)
        {
            this.OpCode = code;
            this.processer = processer;
        }
    }
    public class ProcesserFactroy: IProcesserFactroy
    {
        private readonly Domain domain;

        public ProcesserFactroy(Domain domain)
        {
            this.domain = domain;
        }
        public virtual ServerProcesser Factroy()
        {
            ServerProcesser app = new ServerProcesser(new OpCodeErrorResponseProcesser());
            var connect = new ConnectProcesser();
            connect.Init(domain);
            app.SetConnectProcesser(connect);
            var stop = new ServerStopProcesser();
            stop.Init(domain);
            app.serverStop = stop;
            var array = NewProcessers();
            foreach (var item in array)
            {
                app.Add(item.OpCode, item.processer);
            }
            foreach (var item in array)
            {
                item.processer.Init(domain);
            }
            return app;
        }
      
        protected virtual Binder[] NewProcessers()
        {
            return new Binder[]
            {
                new Binder(MessageCode.RequestMatch, new MatchRequestProcesser()),
                new Binder(MessageCode.RequestCancelMatch, new CancelRequestProcesser()),
                new Binder(MessageCode.RequestPlay, new PlayRequestProcesser())
            };
        }
    }
}
