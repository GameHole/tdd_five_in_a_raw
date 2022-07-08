using System;
using UnityEngine;

namespace Five
{
    public class ChessSelectorView:IUpdate
    {
        private IChessSelectActor actor;
        private ChessSelectorActor runActor;
        private IChessSelectActor stopActor;

        public GameObject ChessPreview { get => runActor.chessPreview; }
        public Vector2Int SelectedPosition { get => runActor.remotePos; }

        public Action<Vector2Int> onPlace;

        public bool IsRun { get => actor == runActor; }

        public ChessSelectorView(IRay ray, BoardRaycastor castor)
        {
            runActor = new ChessSelectorActor(this, ray, castor);
            stopActor = new NoneSelectActor();
            Stop();
        }
        public void Update(float dt=0)
        {
            actor.Update();
        }

        public virtual void Place()
        {
            actor.Place();
        }
        public void Stop()
        {
            ChessPreview.SetActive(false);
            actor = stopActor;
        }

        public void Start()
        {
            actor = runActor;
        }
    }
}
