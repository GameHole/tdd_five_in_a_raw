using Five;

namespace FivesUnitTest
{
    public class TIdGenrator : IdGenrator
    {
        internal int id;
        internal int inviled;
        public override int InvailedId => inviled;
        public override int Genrate()
        {
            return id;
        }
    }
}