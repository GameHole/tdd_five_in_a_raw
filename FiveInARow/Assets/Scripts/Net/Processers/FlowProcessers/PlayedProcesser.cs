using UnityEngine;

namespace Five
{
    public class PlayedProcesser : AFlowProcesser<PlayedNotify>
    {

        public override int OpCode => MessageCode.PlayedNotify;


        public override void ProcessContent(PlayedNotify message)
        {
            int chess = players.FindChess(message.id);
            Vector2Int pos = new Vector2Int(message.x, message.y);
            foreach (var item in flow.GetFlowList<IPlayerPlay>())
            {
                item.Play(chess, pos);
            } 
        }
       
    }
}
