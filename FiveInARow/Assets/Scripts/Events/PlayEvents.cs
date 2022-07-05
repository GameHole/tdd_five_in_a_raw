using UnityEngine;
namespace Five
{
    public class PlayEvents
    {
        public PlayEvents(ChessSelectorView selector, ASocket socket)
        {
            selector.onPlace += (Vector2Int pos) =>
            {
                socket.Send(new PlayRequest { x = pos.x, y = pos.y });
            };
        }
    }
}
