using UnityEngine;

namespace Five
{
    public class SelfChessView : AView
    {
        protected override GameObject GenrateView()
        {
            return PrefabHelper.Instantiate<GameObject>("GameObjects/SelfChessView");
        }
    }
}
