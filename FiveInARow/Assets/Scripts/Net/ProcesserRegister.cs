namespace Five
{
    public class ProcesserRegister:IProcesserRegister
    {
        Container container;
        public GameFlow flow;
        public ProcesserRegister(Container container, GameFlow flow)
        {
            this.container = container;
            this.flow = flow;
        }

        public virtual void Regist(MessageProcesser processer)
        {
            AddResponses(processer);
            AddFlows(processer);
        }

        void AddFlows(MessageProcesser processer)
        {
            var playersInfo = container.Get<PlayersInfo>();
            var player = container.Get<Player>();
            var flows = new IFlowProcesser[]
            {
                 new FinishedProcesser(),
                 new TurnProcesser(),
                 new PlayedProcesser(),
                 new StartedProcesser(player)
            };
            foreach (var item in flows)
            {
                item.SetInfos(flow, playersInfo);
            }
            AddProcessers(processer, flows);
        }

        private void AddResponses(MessageProcesser processer)
        {
            var matchView = container.Get<MatchView>();
            var processers = new IProcesser[]
            {
                new ResponseDecorater(new MatchProcesser(matchView)),
                new ResponseDecorater(new CancelMatchProcesser(matchView)),
                new ResponseDecorater(new NoneResponseProcesser(MessageCode.RequestPlay))
            };
            AddProcessers(processer, processers);
        }

        private void AddProcessers(MessageProcesser processer, IProcesser[] processers)
        {
            foreach (var item in processers)
            {
                processer.Processers.Add(item.OpCode, item);
            }
        }
    }
}
