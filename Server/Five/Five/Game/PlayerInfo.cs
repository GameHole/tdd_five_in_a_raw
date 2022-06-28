namespace Five
{
    public struct PlayerInfo
    {
        public int PlayerId { get; private set; }
        public int Chess { get; private set; }
        public PlayerInfo(Player player)
        {
            PlayerId = player.PlayerId;
            Chess = player.chess;
        }
        public PlayerInfo(int chess,int id)
        {
            PlayerId = id;
            Chess = chess;
        }
    }
}