using Five;

namespace FivesUnitTest
{
    internal class TIdGenrator : IdGenrator
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