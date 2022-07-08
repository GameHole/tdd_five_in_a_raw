using UnityEngine;

namespace Five
{
    public abstract class UIView: AView
    {
        protected abstract string viewName { get; }
        protected override GameObject GenrateView()
        {
            return PrefabHelper.Instantiate<GameObject>($"UI/{viewName}");
        }
        public virtual void Join(Transform parent)
        {
            View.transform.SetParent(parent,false);
        }
    }
}
