using System.Collections.Generic;
using UnityEngine;

namespace Five
{
    public class ChessboardView
    {
        private Dictionary<Vector2Int, ChessView> chess;
        private ChessView chessPrefab;
        private PositionConvertor convertor;
        public ChessboardView(PositionConvertor convertor)
        {
            chess = new Dictionary<Vector2Int, ChessView>();
            chessPrefab = PrefabHelper.Find<ChessView>("GameObjects/Chess");
            this.convertor = convertor;
        }

        public int Count { get=> chess.Count; }

        public ChessView GetChess(int x, int y)
        {
            chess.TryGetValue(new Vector2Int(x, y), out var transform);
            return transform;
        }

        public void SetChess(int x, int y,int chessType)
        {
            var pos = new Vector2Int(x, y);
            if (chess.ContainsKey(pos))
            {
                throw new PositionPlacedChessException(pos);
            }
            var clone = Object.Instantiate(chessPrefab);
            clone.transform.position = convertor.ToLocalPosition(pos);
            clone.ChessType = chessType;
            chess[pos] = clone;
        }
       
        public void Clear()
        {
            foreach (var item in chess.Values)
            {
                Object.Destroy(item.gameObject);
            }
            chess.Clear();
        }
    }
}