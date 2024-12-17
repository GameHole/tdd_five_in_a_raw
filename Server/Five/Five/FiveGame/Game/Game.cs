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
        private List<FiveCharater> charaters;
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
            charaters = new List<FiveCharater>();
            var chess = 1;
            foreach (var item in room.Players)
            {
                charaters.Add(new FiveCharater(item.Id, chess++));
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
                infos[i++] = new PlayerInfo(GetPlayerChess(item), item.Id);
            }
            foreach (var item in room.Players)
            {
                item.notifier.Send(new StartNotify { playerId = item.Id, infos = infos });
            }
        }
        private void NotifyTurnPlayer()
        {
            var charater = getCurrentCharater();
            room.NotifyAllPlayer(new PlayerIdNotify(MessageCode.TurnNotify, charater.Id));
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
            var charater = getCurrentCharater();
            if (player.Id != charater.Id)
                return ResultDefine.NotCurrentTurnPlayer;
            PlayMessage play = message as PlayMessage;
            int x = play.x;
            int y = play.y;
            if (!chessboard.AddValue(x, y, charater.Chess))
            {
                return ResultDefine.AllReadyHasChess;
            }
            room.NotifyAllPlayer(new PlayedNotify 
            { 
                x = x, y = y,
                id = player.Id 
            });
            if (chessboard.isFiveInRow(charater.Chess))
            {
                Finish(charater.Id);
            }
            else
            {
                NextPlayer();
            }
            return ResultDefine.Success;
        }

        private FiveCharater getCurrentCharater()
        {
            return charaters[turn.index];
        }

        public int GetPlayerChess(Player player)
        {
            return charaters.Find((v) => v.Id == player.Id).Chess;
        }
    }
}
