using UnityEngine.UI;

namespace Five
{
    public class FinishView : UIView
    {
        private bool _isWin;
        public bool IsWin
        {
            get => _isWin;
            set
            {
                _isWin = value;
                SetWinText(_isWin);
            }
        }

        protected override string viewName => "FinishView";

        public Text text { get; }
        public Button button { get; }

        public FinishView():base()
        {
            Close();
            text = View.transform.Find("winText").GetComponent<Text>();
            button = View.transform.Find("button").GetComponent<Button>();
        }

        void SetWinText(bool win)
        {
            text.text = $"you {(win ? "win" : "loss")}";
        }
    }
}
