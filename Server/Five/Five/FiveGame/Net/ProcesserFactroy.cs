namespace Five
{
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
                processer.Processers.Add(item.OpCode, item);
            }
            foreach (var item in array)
            {
                item.Init(app);
            }
            return processer;
        }

        protected virtual RequestProcesser[] NewProcessers()
        {
            return new RequestProcesser[]
            {
                new MatchRequestProcesser(),
                new CancelRequestProcesser(),
                new PlayRequestProcesser()
            };
        }
    }
}
