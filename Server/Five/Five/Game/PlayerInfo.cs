namespace Five
{
    public class PlayerInfo
    {
        public int PlayerId { get; private set; }
        public int Chess { get; private set; }
        public PlayerInfo(Player player)
        {
            PlayerId = player.PlayerId;
            Chess = player.chess;
        }
    }
}