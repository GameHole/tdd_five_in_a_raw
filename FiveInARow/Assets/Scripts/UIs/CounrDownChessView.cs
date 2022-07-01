using UnityEngine;
using UnityEngine.UI;

namespace Five
{
    public class CounrDownChessView : AChessView
    {
        public Image bg;
        public Text text;

        protected override void TypeSetted()
        {
            bg.color = TypeToColor(bg.color.a);
            text.color = OneMinus(bg.color);
        }
        private Color OneMinus(Color color)
        {
            return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
        }
    }
}
