namespace Five
{
    public interface IFlowProcesser:IProcesser
    {
        void SetInfos(GameFlow flow, PlayersInfo players);
    }
    public abstract class AFlowProcesser<T> : AProcesser<T>, IFlowProcesser where T : Message
    {
        protected PlayersInfo players;
        protected GameFlow flow;
        public void SetInfos(GameFlow flow, PlayersInfo players)
        {
            this.flow = flow;
            this.players = players;
        }
    }
}
