namespace Five
{
    public class FlowRegister
    {
        private Container container;

        public FlowRegister(Container container)
        {
            this.container = container;
        }

        public void Regist(GameFlow flow)
        {
            var player = container.Get<Player>();
            var countView = container.Get<CountDownView>();
            var selector = container.Get<ChessSelectorView>();
            flow.AddFlow(new StartViewController { game = container.Get<GameView>(), main = container.Get<MatchView>() });
            flow.AddFlow(new SelfChessSetter { self = container.Get<SelfChessView>() });
            flow.AddFlow(new ChessboardViewController { chessView = container.Get<ChessboardView>() });
            flow.AddFlow(new FinishViewController { finishView = container.Get<FinishView>(), player = player });
            flow.AddFlow(new CountDownViewController { countView = countView });
            flow.AddFlow(new ChessSelectorController { selector = selector, player = player });
            flow.AddFlow(new TurnChessTypeSetter().Add(countView.View).Add(selector.ChessPreview));
        }
    }
}
