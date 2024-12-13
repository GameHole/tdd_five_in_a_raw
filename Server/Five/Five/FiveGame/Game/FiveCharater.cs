namespace Five
{
    class FiveCharater : Charater
    {
        public int Chess { get; }
        public FiveCharater(int id, int chess) : base(id)
        {
            Chess = chess;
        }
    }
}
