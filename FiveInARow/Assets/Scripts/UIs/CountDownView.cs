using System;
using UnityEngine;
using UnityEngine.UI;

namespace Five
{
    public class CountDownView:UIView
    {
        public CountDownView(float time):base()
        {
            Time = time;
            UpdateTime();
        }
        private Text _text;
        private Text text
        {
            get
            {
                if (!_text)
                    _text = View.transform.Find("CountDownTxt").GetComponent<Text>();
                return _text;
            }
        }
        public string TimeTxt { get => text.text; set => text.text = value; }
        public float addingTime { get; set; }
        public float Time { get; set; }

        protected override string viewName => "CountDownView";

        public void Update(float dt)
        {
            addingTime += dt;
            UpdateTime();
        }

        private void UpdateTime()
        {
            TimeTxt = ((int)Mathf.Max(0, Time - addingTime)).ToString();
        }

        public void Reset()
        {
            addingTime = 0;
            UpdateTime();
        }
    }
}
