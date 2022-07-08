using UnityEngine;

namespace Five
{
    public interface IPlayerPlay: IFlow
    {
        void Play(int chess, Vector2Int pos);
    }
}