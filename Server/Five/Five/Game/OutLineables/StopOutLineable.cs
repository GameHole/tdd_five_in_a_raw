namespace Five
{
    class StopOutLineable : IOutLineable
    {
        private Room game;
        private Player player;
        public StopOutLineable(Room game,Player player)
        {
            this.game = game;
            this.player = player;
        }
        public void OutLine()
        {
            game.Remove(player);
        }
    }
}
