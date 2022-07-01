using UnityEngine;

namespace Five
{
    public abstract class UIView
    {
        public GameObject View { get; private set; }
        public UIView()
        {
            View = PrefabHelper.Instantiate<GameObject>($"UI/{viewName}");
        }
        public virtual void Join(Transform parent)
        {
            View.transform.SetParent(parent);
        }
        protected abstract string viewName { get; }
        public virtual void Open()
        {
            View.SetActive(true);
        }
        public virtual void Close()
        {
            View.SetActive(false);
        }
    }
}
