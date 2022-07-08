using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Five
{
    public class App:IUpdate
    {
        public Container cntr { get; } = new Container();
        public ILogger loger { get; set; } = Debug.unityLogger;
        public List<IUpdate> updaters { get; private set; } = new List<IUpdate>();
        public Client client { get; }
        public bool IsStarted { get; private set; }
        public GameFlow gameFlow { get; private set; }

        private Ip ip;
        public App(ASocket socket, Ip ip)
        {
            this.ip = ip;
            var canvas = PrefabHelper.Instantiate<Transform>("UI/Canvas");
            cntr.Set(new LoadingView()).Join(canvas);
            var match = cntr.Set(new MatchView());
            match.Join(canvas);
            new GameBuilder(30, 15, Camera.main).Build(cntr);
            new GameView(cntr);
            cntr.Set(new Player());
            cntr.Set(new PlayersInfo());
            gameFlow = new GameFlow();
            new FlowRegister(cntr).Regist(gameFlow);
            client = new Client(socket, new ProcesserRegister(cntr, gameFlow));
            new MatchButtonEvents(match, socket);
            new PlayEvents(cntr.Get<ChessSelectorView>(), socket);
            new FinishButtonEvents(match, cntr.Get<FinishView>(), cntr.Get<GameView>());
            LoadUpdater();
        }
        void LoadUpdater()
        {
            foreach (var item in cntr)
            {
                if (item is IUpdate update)
                {
                    updaters.Add(update);
                }
            }
            updaters.Add(client);
        }
        public async void Start()
        {
            IsStarted = true;
            var result = await client.ConnectAsync(ip.address, ip.port);
            if (result == ResultDefine.Success)
            {
                cntr.Get<LoadingView>().Close();
                cntr.Get<MatchView>().Open();
            }
            else
            {
                loger.LogError("", "Connect Error");
            }
        }
        public void Update(float dt)
        {
            foreach (var item in updaters)
            {
                item.Update(dt);
            } 
        }
    }
}
