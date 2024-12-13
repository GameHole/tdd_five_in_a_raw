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
        private readonly MatchServce svc;

        public ProcesserFactroy(MatchServce mgr)
        {
            this.svc = mgr;
        }
        public virtual App Factroy()
        {
            App app = new App(new OpCodeErrorResponseProcesser());
            var connect = new ConnectProcesser();
            connect.Init(svc);
            app.connect = connect;
            var stop = new ServerStopProcesser();
            stop.Init(svc);
            app.serverStop = stop;
            var array = NewProcessers();
            foreach (var item in array)
            {
                app.Add(item.OpCode, item.processer);
            }
            foreach (var item in array)
            {
                item.processer.Init(svc);
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
