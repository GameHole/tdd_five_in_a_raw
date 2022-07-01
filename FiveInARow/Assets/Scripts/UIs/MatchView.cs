using System;
using UnityEngine;
using UnityEngine.UI;

namespace Five
{
    public class MatchView: UIView
    {
        public GameObject MatchingView { get; private set; }
        public GameObject CancelView { get;private set; }

        protected override string viewName => "MatchView";

        public MatchView():base()
        {
            MatchingView = View.transform.Find("MatchingView").gameObject;
            CancelView = View.transform.Find("CancelView").gameObject;
            View.SetActive(false);
        }

        public override void Open()
        {
            base.Open();
            ActiveMatching(true);
        }

        public void Matched()
        {
            ActiveMatching(false);
        }
        void ActiveMatching(bool active)
        {
            MatchingView.SetActive(active);
            CancelView.SetActive(!active);
        }

        public void Canceled()
        {
            ActiveMatching(true);
        }
    }
}
