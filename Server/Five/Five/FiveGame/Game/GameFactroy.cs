namespace Five
{
    public class GameFactroy: IGameFactroy
    {
        public AGame Factroy()
        {
            return new Game(15, 30);
        }
    }
}
