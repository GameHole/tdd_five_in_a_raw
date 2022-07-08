using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    public class ChessPlacer : IUpdate
    {
        private IInput testInput;
        private ChessSelectorView selector;

        public ChessPlacer(IInput testInput, ChessSelectorView selector)
        {
            this.testInput = testInput;
            this.selector = selector;
        }

        public void Update(float dt=0)
        {
            if (testInput.GetDown())
            {
                selector.Place();
            }
        }
    }
}
