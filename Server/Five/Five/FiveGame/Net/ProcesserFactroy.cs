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
        private readonly MatchServce app;

        public ProcesserFactroy(MatchServce mgr)
        {
            this.app = mgr;
        }
        public virtual MessageProcesser Factroy()
        {
            MessageProcesser processer = new MessageProcesser(new OpCodeErrorResponseProcesser());
            var connect = new ConnectProcesser();
            connect.Init(app);
            processer.connect = connect;
            var stop = new ServerStopProcesser();
            stop.Init(app);
            processer.serverStop = stop;
            var array = NewProcessers();
            foreach (var item in array)
            {
                processer.Processers.Add(item.OpCode, item.processer);
            }
            foreach (var item in array)
            {
                item.processer.Init(app);
            }
            return processer;
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
