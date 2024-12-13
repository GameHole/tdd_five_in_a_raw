using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Game : AGame
    {       
        public Turn turn { get; private set; }
        public LoopTimer timer { get; private set; }
        public Chessboard chessboard { get; private set; }
        private List<int> playerIDs;
        public Game(int boardSize,float turnTime)
        {
            timer = new LoopTimer(turnTime);
            chessboard = new Chessboard(boardSize, boardSize);
        }
        public override void Init(IRoom room)
        {
            base.Init(room);
            turn = new Turn(room.maxPlayer);
        }

        public override void Start()
        {
            base.Start();
            playerIDs = new List<int>();
            var chess = 1;
            foreach (var item in room.Players)
            {
                item.Start(chess++);
                playerIDs.Add(item.Id);
            }
            turn.Start();
            NotifyStart();
            NotifyTurnPlayer();
            TimerDriver.Start(timer);
            timer.onTime -= NextPlayer;
            timer.onTime += NextPlayer;
        }
        public void NotifyStart()
        {
            var infos = new PlayerInfo[room.maxPlayer];
            int i = 0;
            foreach (var item in room.Players)
            {
                infos[i++] = new PlayerInfo(item.chess, item.Id);
            }
            foreach (var item in room.Players)
            {
                item.notifier.Send(new StartNotify { playerId = item.Id, infos = infos });
            }
        }
        private void NotifyTurnPlayer()
        {
            room.NotifyAllPlayer(new PlayerIdNotify(MessageCode.TurnNotify, playerIDs[turn.index]));
        }

        public void NextPlayer()
        {
            turn.Next();
            NotifyTurnPlayer();
            timer.Reset();
        }

        public void Finish(int id)
        {
            room.NotifyAllPlayer(new PlayerIdNotify(MessageCode.FinishNotify, id));
            room.Stop();
        }

        public override void Stop()
        {
            chessboard.Clear();
            TimerDriver.Stop(timer);
        }

        public override Result Commit(Message message,Player player)
        {
            if (player.Id !=playerIDs[turn.index])
                return ResultDefine.NotCurrentTurnPlayer;
            PlayMessage play = message as PlayMessage;
            int x = play.x;
            int y = play.y;
            if (!chessboard.AddValue(x, y, player.chess))
            {
                return ResultDefine.AllReadyHasChess;
            }
            var notify = new PlayedNotify { x = x, y = y, id = player.Id };
            room.NotifyAllPlayer(notify);
            if (chessboard.isFiveInRow(player.chess))
            {
                Finish(player.Id);
            }
            else
            {
                NextPlayer();
            }
            return ResultDefine.Success;
        }
    }
}
