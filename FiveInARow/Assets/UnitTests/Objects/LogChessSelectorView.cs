using Five;

namespace UnitTests
{
    internal class LogChessSelectorView:ChessSelectorView
    {
        internal bool isPlaced;

        public LogChessSelectorView(IRay ray, BoardRaycastor castor) : base(ray, castor)
        {
        }

        public override void Place()
        {
            base.Place();
            isPlaced = true;
        }
    }
}