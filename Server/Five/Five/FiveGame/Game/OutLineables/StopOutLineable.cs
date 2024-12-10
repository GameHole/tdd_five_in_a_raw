namespace Five
{
    class StopOutLineable : IOutLineable
    {
        private Room room;
        private Player player;
        public StopOutLineable(Room room,Player player)
        {
            this.room = room;
            this.player = player;
        }
        public void OutLine()
        {
            room.Remove(player);
        }
    }
}
