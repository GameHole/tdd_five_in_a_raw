using System;
using UnityEngine;

namespace Five
{
    [Serializable]
    public class PositionPlacedChessException:Exception
    {
        public PositionPlacedChessException(Vector2Int pos) : base($"You can not place chess at poisition ({pos.x},{pos.y}) ,because position allready placeing a chess.") { }
    }
}
