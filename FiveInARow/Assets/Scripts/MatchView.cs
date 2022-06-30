using UnityEngine;

namespace Five
{
    public class MatchView
    {
        public GameObject matchView { get; set; }
        public GameObject cancelView { get; set; }
        public MatchView()
        {
            matchView = PrefabHelper.Instantiate("UI/MatchView");
        }
    }
}
