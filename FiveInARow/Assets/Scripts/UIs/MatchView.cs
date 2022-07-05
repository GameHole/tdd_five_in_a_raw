using System;
using UnityEngine;
using UnityEngine.UI;

namespace Five
{
    public class MatchView: UIView
    {
        protected override string viewName => "MatchView";

        public GameObject MatchingView { get; private set; }
        public GameObject CancelView { get;private set; }

        public Button matchBtn { get; }
        public Button cancelBtn { get; }

        public MatchView():base()
        {
            MatchingView = View.transform.Find("MatchingView").gameObject;
            CancelView = View.transform.Find("CancelView").gameObject;
            matchBtn = MatchingView.transform.Find("matchBtn").GetComponent<Button>();
            cancelBtn = CancelView.transform.Find("cancelBtn").GetComponent<Button>();
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
