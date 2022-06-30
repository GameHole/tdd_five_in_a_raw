using System;
using UnityEngine;

namespace Five
{
    public class PositionConvertor
    {
        public Vector2Int ToRemotePosition(Vector3 worldPos)
        {
            return new Vector2Int((int)(worldPos.x + 0.5f), (int)(worldPos.z + 0.5f));
        }

        public Vector3 ToLocalPosition(Vector2Int remote)
        {
            return new Vector3(remote.x, 0, remote.y);
        }
    }
}
